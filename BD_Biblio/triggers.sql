CREATE TRIGGER trg_VerifierDisponibiliteLivre
ON Emprunts
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @LivreID INT;
    SELECT @LivreID = LivreID FROM inserted;

    IF EXISTS (SELECT 1 FROM Emprunts WHERE LivreID = @LivreID AND EstRendu = 0)
    BEGIN
        RETURN
    END
    ELSE
    BEGIN
        INSERT INTO Emprunts (LivreID, UtilisateurID, DateEmprunt, DateRetour, EstRendu)
        SELECT LivreID, UtilisateurID, DateEmprunt, DateRetour, EstRendu
        FROM inserted;
    END
END;
GO


CREATE TRIGGER trg_MettreAJourRetourLivre
ON Emprunts
AFTER UPDATE
AS
BEGIN
    IF UPDATE(EstRendu) -- Vérifie si le champ EstRendu a été mis à jour
    BEGIN
        UPDATE Emprunts
        SET DateRetour = GETDATE()
        WHERE EmpruntID IN (SELECT EmpruntID FROM inserted WHERE EstRendu = 1);
    END
END;
GO
