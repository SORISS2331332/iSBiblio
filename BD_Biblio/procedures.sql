-- Procédure pour ajouter un nouvel auteur

IF OBJECT_ID('dbo.AjouterAuteur', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.AjouterAuteur;
END
GO
CREATE PROCEDURE AjouterAuteur
    @Nom NVARCHAR(100),
    @Prenom NVARCHAR(100),
    @DateNaissance DATE
AS
BEGIN
    INSERT INTO Auteurs (Nom, Prenom, DateNaissance)
    VALUES (@Nom, @Prenom, @DateNaissance);
END;
GO



-- Procédure Stockée pour l'ajout d'un nouvel utilisateur

IF OBJECT_ID('dbo.AjouterUtilisateur', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.AjouterUtilisateur;
END
GO
CREATE PROCEDURE AjouterUtilisateur
    @Nom NVARCHAR(100),
    @Prenom NVARCHAR(100),
    @Adresse NVARCHAR(255),
    @Email NVARCHAR(255),
    @MotDePasse NVARCHAR(255) -- Mot de passe en clair
AS
BEGIN
    DECLARE @Sel UNIQUEIDENTIFIER;
	SET @sel = NEWID(); -- Générer un sel aléatoire
    DECLARE @MotDePasseHache VARBINARY(255);
    
    -- Hachage du mot de passe avec le sel
    SET @MotDePasseHache = HASHBYTES('SHA2_256', @MotDePasse + CAST((@Sel) AS NVARCHAR(255)));

    -- Insertion de l'utilisateur
    INSERT INTO Utilisateurs (Nom, Prenom, Adresse, Email, MotDePasse, Sel)
    VALUES (@Nom, @Prenom, @Adresse, @Email, @MotDePasseHache, @Sel);
END;
GO



-- Procédure Stockée pour l'authentification d'un utilisateur
IF OBJECT_ID('dbo.AuthentifierUtilisateur', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.AuthentifierUtilisateur;
END
GO
CREATE PROCEDURE AuthentifierUtilisateur
    @Email NVARCHAR(255),
    @MotDePasse NVARCHAR(255), -- Mot de passe en clair
    @Resultat BIT OUTPUT
AS
BEGIN
    DECLARE @MotDePasseHache VARBINARY(255);
    DECLARE @Sel UNIQUEIDENTIFIER;

    -- Récupérer le sel et le mot de passe haché de l'utilisateur
    SELECT @Sel = Sel, @MotDePasseHache = MotDePasse
    FROM Utilisateurs
    WHERE Email = @Email;

    -- Vérifier si l'utilisateur existe
    IF @MotDePasseHache IS NOT NULL
    BEGIN
        -- Hachage du mot de passe fourni avec le sel
        IF @MotDePasseHache = HASHBYTES('SHA2_256', @MotDePasse + CAST(@Sel AS NVARCHAR(255)))
        BEGIN
            SET @Resultat = 1; -- Authentification réussie
        END
        ELSE
        BEGIN
            SET @Resultat = 0; -- Échec de l'authentification
        END
    END
    ELSE
    BEGIN
        SET @Resultat = 0; -- Utilisateur non trouvé
    END
END;
GO


-- Procédure Stockée pour ajouter un livre

IF OBJECT_ID('dbo.AjouterLivre', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.AjouterLivre;
END
GO
CREATE PROCEDURE AjouterLivre
    @Titre NVARCHAR(255),
    @LienImage NVARCHAR(255),
    @AuteurId INT,
    @AnneePublication DATE,
    @Genre NVARCHAR(100)
AS
BEGIN
    INSERT INTO Livres (Titre, LienImage,AuteurID, AnneePublication, Genre)
    VALUES (@Titre,@LienImage, @AuteurId, @AnneePublication, @Genre);
END;
GO

-- Procédure Stockée pour enregistrer un emprunt

IF OBJECT_ID('dbo.EmprunterLivre', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.EmprunterLivre;
END
GO
CREATE PROCEDURE EmprunterLivre
    @LivreID INT,
    @UtilisateurID INT
AS
BEGIN
    INSERT INTO Emprunts (LivreID, UtilisateurID, DateEmprunt)
    VALUES (@LivreID, @UtilisateurID, GETDATE());
END;
GO

-- Procédure Stockée pour retourner un livre

IF OBJECT_ID('dbo.RetournerLivre', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE dbo.RetournerLivre;
END
GO
CREATE PROCEDURE RetournerLivre
    @EmpruntID INT
AS
BEGIN
    UPDATE Emprunts
    SET EstRendu = 1, DateRetour = GETDATE()
    WHERE EmpruntID = @EmpruntID;

    DECLARE @LivreID INT;
    SELECT @LivreID = LivreID FROM Emprunts WHERE EmpruntID = @EmpruntID;

    UPDATE Livres
    SET Disponibilite = 1
    WHERE LivreID = @LivreID;
END;
GO
