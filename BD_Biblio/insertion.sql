-- Insertion de données dans la table Utilisateurs
INSERT INTO Utilisateurs (Nom, Prenom, Adresse, Email, MotDePasse, Sel) VALUES 
('Martin', 'Alice', '45 Rue des Fleurs, Paris', 'alice.martin@example.com', HASHBYTES('SHA2_256', 'Password1' + CAST(NEWID() AS NVARCHAR(255))), NEWID()),
('Bernard', 'Luc', '78 Avenue de la République, Lyon', 'luc.bernard@example.com', HASHBYTES('SHA2_256', 'Password2' + CAST(NEWID() AS NVARCHAR(255))), NEWID()),
('Lemoine', 'Sophie', '12 Boulevard du Général, Marseille', 'sophie.lemoine@example.com', HASHBYTES('SHA2_256', 'Password3' + CAST(NEWID() AS NVARCHAR(255))), NEWID()),
('Garnier', 'Julien', '89 Chemin de la Liberté, Toulouse', 'julien.garnier@example.com', HASHBYTES('SHA2_256', 'Password4' + CAST(NEWID() AS NVARCHAR(255))), NEWID()),
('Laurent', 'Émilie', '33 Rue des Écoles, Bordeaux', 'emilie.laurent@example.com', HASHBYTES('SHA2_256', 'Password5' + CAST(NEWID() AS NVARCHAR(255))), NEWID());

-- Insertion de données dans la table Auteurs
INSERT INTO Auteurs (Nom, Prenom, DateNaissance) VALUES 
('Hugo', 'Victor', '1802-02-26'),
('Orwell', 'George', '1903-06-25'),
('Austen', 'Jane', '1775-12-16'),
('Tolkien', 'J.R.R.', '1892-01-03'),
('Rowling', 'J.K.', '1965-07-31'),
('Martin', 'George R.R.', '1948-09-20'),
('Dumas', 'Alexandre', '1802-07-24'),
('Hemingway', 'Ernest', '1899-07-21'),
('Fitzgerald', 'F. Scott', '1896-09-24'),
('Dickens', 'Charles', '1812-02-07'),
('Poe', 'Edgar Allan', '1809-01-19'),
('Bradbury', 'Ray', '1920-08-22'),
('Chandler', 'Raymond', '1888-07-23'),
('Morrison', 'Toni', '1931-02-18'),
('Gibson', 'William', '1948-01-17'),
('Asimov', 'Isaac', '1920-01-02'),
('Calvino', 'Italo', '1923-10-15'),
('Huxley', 'Aldous', '1894-07-26'),
('Dostoevsky', 'Fyodor', '1821-11-11'),
('Melville', 'Herman', '1819-08-01');

-- Insertion de données dans la table Livres
INSERT INTO Livres (Titre, AuteurID, AnneePublication, Genre, Disponibilite) VALUES
('Le Petit Prince', 1, 1943, 'Fiction', 1),
('1984', 2, 1949, 'Dystopie', 1),
('Moby Dick', 3, 1851, 'Aventure', 1),
('Les Misérables', 4, 1862, 'Roman', 1),
('Pride and Prejudice', 5, 1813, 'Roman', 1),
('To Kill a Mockingbird', 6, 1960, 'Fiction', 1),
('The Great Gatsby', 7, 1925, 'Roman', 1),
('War and Peace', 8, 1869, 'Historique', 1),
('The Catcher in the Rye', 9, 1951, 'Fiction', 1),
('Brave New World', 10, 1932, 'Dystopie', 1),
('Crime and Punishment', 11, 1866, 'Roman', 1),
('The Picture of Dorian Gray', 12, 1890, 'Roman', 1),
('The Brothers Karamazov', 13, 1880, 'Roman', 1),
('Fahrenheit 451', 14, 1953, 'Dystopie', 1),
('The Hobbit', 15, 1937, 'Fantastique', 1),
('Les Fleurs du mal', 16, 1857, 'Poésie', 1),
('The Alchemist', 17, 1988, 'Fiction', 1),
('The Odyssey', 18, -800, 'Épopée', 1),
('The Grapes of Wrath', 19, 1939, 'Roman', 1),
('The Divine Comedy', 20, 1320, 'Épopée', 1);




-- Insertion de données dans la table Emprunts
INSERT INTO Emprunts (LivreID, UtilisateurID, DateEmprunt, DateRetour, EstRendu) VALUES 
(1, 1, GETDATE(), NULL, 0),
(2, 1, GETDATE(), NULL, 0),
(3, 2, GETDATE(), NULL, 0),
(4, 3, GETDATE(), NULL, 0),
(5, 4, GETDATE(), NULL, 0),
(6, 5, GETDATE(), NULL, 0);



