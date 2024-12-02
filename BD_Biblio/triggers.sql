
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
