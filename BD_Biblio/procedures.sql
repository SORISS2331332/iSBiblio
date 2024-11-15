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




Go
CREATE PROCEDURE [dbo].[ConnecterUtilisateur]
    @Email NVARCHAR(255),
    @Password NVARCHAR(255)
AS
BEGIN
    DECLARE @IdentifiantUtilisateur INT;   
    DECLARE @CompteVerrouille BIT;          
    DECLARE @MotDePasseHache VARBINARY(255); 
    DECLARE @Sel UNIQUEIDENTIFIER;          
    DECLARE @MotDePasseStocke VARBINARY(255); 
    DECLARE @Tentatives INT;                
    DECLARE @PremiereTentativeEchouee DATETIME;  
    DECLARE @DerniereTentativeEchouee DATETIME;   
    DECLARE @TempsFinVerrouillage DATETIME;      
    DECLARE @TempsRestant INT;               

    --Existante de L,utilisateur
    SELECT @IdentifiantUtilisateur = UtilisateurID, 
        @Sel = Sel, 
        @MotDePasseStocke = MotDePasse
    FROM Utilisateurs
    WHERE Email = @Email;
    IF @IdentifiantUtilisateur IS NULL
    BEGIN
        RAISERROR('Utilisateur non trouvé.', 16, 1);
        RETURN;
    END

    -- Vérifier les tentatives échouées dans les 3 dernières minutes
    SELECT 
        @CompteVerrouille = CASE WHEN COUNT(*) >= 5 THEN 1 ELSE 0 END,
        @PremiereTentativeEchouee = MIN(DateEssai), 
        @DerniereTentativeEchouee = MAX(DateEssai)    
    WHERE UserId = @IdentifiantUtilisateur
    AND EtatConnexion = 0
    AND DateEssai > DATEADD(MINUTE, -3, GETDATE());  
    
    -- Si l'utilisateur est verrouillé
    IF @CompteVerrouille = 1
    BEGIN
        -- Calcul de l'heure de fin du verrouillage (3 minutes après la première tentative échouée)
        SET @TempsFinVerrouillage = DATEADD(MINUTE, 3, @PremiereTentativeEchouee);

        -- Calculer le temps restant avant la fin du verrouillage
        SET @TempsRestant = DATEDIFF(MINUTE, GETDATE(), @TempsFinVerrouillage);

        -- Si le temps restant est positif, l'utilisateur doit attendre
        IF @TempsRestant > 0
        BEGIN
            -- Retourner le message avec le temps restant avant une nouvelle tentative
            RAISERROR('Compte verrouillé en raison de tentatives de connexion échouées. Il vous reste %d minute(s) avant de pouvoir réessayer.', 16, 1, @TempsRestant);
            RETURN;
        END
    END

    -- Hacher le mot de passe fourni avec le sel récupéré
    SET @MotDePasseHache = HASHBYTES('SHA2_256', @Password + CAST(@Sel AS NVARCHAR(255)));

    -- Comparer le mot de passe haché avec celui stocké
    IF @MotDePasseStocke = @MotDePasseHache
    BEGIN
        -- Si la connexion est réussie, enregistrer la tentative réussie
        INSERT INTO Essais (UserId, DateEssai, EtatConnexion)
        VALUES (@IdentifiantUtilisateur, GETDATE(), 1);

        -- Retourner un indicateur de succès
        SELECT 'Succès' AS Status;
    END
    ELSE
    BEGIN
        -- Si la connexion échoue, enregistrer une tentative échouée
        INSERT INTO Essais (UserId, DateEssai, EtatConnexion)
        VALUES (@IdentifiantUtilisateur, GETDATE(), 0);

        -- Retourner un indicateur d'échec
        RAISERROR('Mot de passe incorrect.', 16, 1);
    END
END

