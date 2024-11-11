-- Vue pour les livres disponibles

GO
CREATE VIEW LivresDisponibles AS
SELECT L.LivreID, L.Titre, L.Genre,L.AnneePublication, A.Nom AS Auteur, A.Prenom AS PrenomAuteur
FROM Livres L
JOIN Auteurs A ON L.AuteurID = A.AuteurID
WHERE Disponibilite = 1

-- Vue pour les emprunts actifs
GO
CREATE VIEW VueEmpruntsActifs AS
SELECT e.EmpruntID, l.Titre, u.Nom, u.Prenom, e.DateEmprunt, e.DateRetour
FROM Emprunts e
JOIN Livres l ON e.LivreID = l.LivreID
JOIN Utilisateurs u ON e.UtilisateurID = u.UtilisateurID
WHERE e.EstRendu = 0;


-- Vue pour les emprunts par utilisateur
GO
CREATE VIEW VueEmpruntsUsers AS
SELECT e.EmpruntID, l.Titre,l.Genre, u.Email, e.DateEmprunt, e.DateRetour
FROM Emprunts e
JOIN Livres l ON e.LivreID = l.LivreID
JOIN Utilisateurs u ON e.UtilisateurID = u.UtilisateurID
GO

