-- Vue pour les livres disponibles
CREATE VIEW VueLivresDisponibles AS
SELECT L.LivreID, L.Titre, A.Nom AS Auteur, A.Prenom AS PrenomAuteur
FROM Livres L
JOIN Auteurs A ON L.AuteurID = A.AuteurID
WHERE Disponibilite = 1;
GO


CREATE VIEW VueEmpruntsActifs AS
SELECT e.EmpruntID, l.Titre, u.Nom, u.Prenom, e.DateEmprunt, e.DateRetour
FROM Emprunts e
JOIN Livres l ON e.LivreID = l.LivreID
JOIN Utilisateurs u ON e.UtilisateurID = u.UtilisateurID
WHERE e.EstRendu = 0;
GO

CREATE VIEW VueUtilisateursAvecLivres AS
SELECT 
    u.Nom,
    u.Prenom,
    u.Email,
    l.Titre,
    e.DateEmprunt,
    e.DateRetour
FROM 
    Utilisateurs u
JOIN 
    Emprunts e ON u.UtilisateurID = e.UtilisateurID
JOIN 
    Livres l ON e.LivreID = l.LivreID
WHERE 
    e.EstRendu = 0; -- Pour n'afficher que les livres non retournés
GO
