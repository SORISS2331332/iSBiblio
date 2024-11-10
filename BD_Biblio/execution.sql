-- 1. Ajouter un nouvel utilisateur
DECLARE @Resultat BIT;

EXEC AjouterUtilisateur 
    @Nom = 'Dupont',
    @Prenom = 'Jean',
    @Adresse = '123 Rue de la Bibliothèque',
    @Email = 'jean.dupont@example.com',
    @MotDePasse = 'Password'; -- Remplacez par le mot de passe haché


    
-- 2. Authentifier un utilisateur
EXEC AuthentifierUtilisateur 
    @Email = 'jean.dupont@example.com',
    @MotDePasse = 'Password', -- Remplacez par le mot de passe à comparer
    @Resultat = @Resultat OUTPUT;

SELECT @Resultat AS AuthentificationResultat; -- 1 pour succès, 0 pour échec

-- 3. Ajouter un livre
EXEC AjouterLivre 
    @Titre = 'Le Petit Prince',
    @ISBN = '978-3-16-148410-0',
    @AnneePublication = 1943,
    @Genre = 'Fiction';

-- 4. Emprunter un livre
DECLARE @LivreID INT = 1; -- Remplacez par l'ID du livre
DECLARE @UtilisateurID INT = 1; -- Remplacez par l'ID de l'utilisateur

EXEC EmprunterLivre 
    @LivreID = @LivreID,
    @UtilisateurID = @UtilisateurID;

-- 5. Retourner un livre
DECLARE @EmpruntID INT = 1; -- Remplacez par l'ID de l'emprunt

EXEC RetournerLivre 
    @EmpruntID = @EmpruntID;
