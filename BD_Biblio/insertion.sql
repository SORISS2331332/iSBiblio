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






-- Insertion des livres pour le genre 'Fiction'
INSERT INTO Livres (Titre, AuteurID, AnneePublication, Genre)
VALUES 
('Les Ombres de la Ville', 1, 2020, 'Fiction'),
('Le Dernier Voyage de l''Horizon', 2, 2021, 'Fiction'),
('Sous le Ciel de Paris', 3, 2019, 'Fiction'),
('La Nuit des Étoiles', 4, 2018, 'Fiction'),
('Les Secrets de l''île oubliée', 5, 2020, 'Fiction'),
('L''Écho des Silences', 6, 2017, 'Fiction'),
('Le Miroir du Temps', 7, 2021, 'Fiction'),
('Les Mémoires d''un Voyageur', 8, 2022, 'Fiction'),
('La Forêt des Rêves Perdus', 9, 2021, 'Fiction'),
('Le Masque de l''Inconnu', 10, 2020, 'Fiction');

-- Insertion des livres pour le genre 'Science'
INSERT INTO Livres (Titre, AuteurID, AnneePublication, Genre)
VALUES 
('L''Univers Dévoilé', 1, 2019, 'Science'),
('Les Mystères de la Génétique', 2, 2020, 'Science'),
('Voyage au Centre de l''Atome', 3, 2021, 'Science'),
('Le Cerveau Humain: Une Aventure', 4, 2018, 'Science'),
('Les Frontières de la Biologie', 5, 2021, 'Science'),
('L''Énergie et ses Secrets', 6, 2020, 'Science'),
('Quand la Terre S''Exprime', 7, 2022, 'Science'),
('Les Fondements de la Physique Quantique', 8, 2019, 'Science'),
('Dans les Profondeurs de l''Océan', 9, 2021, 'Science'),
('Exploration des Étoiles Lointaines', 10, 2020, 'Science');

-- Insertion des livres pour le genre 'Histoire'
INSERT INTO Livres (Titre, AuteurID, AnneePublication, Genre)
VALUES 
('L''Empire Romain : Une Histoire de Puissance', 1, 2018, 'Histoire'),
('La Révolution Française : Un Nouveau Monde', 2, 2021, 'Histoire'),
('Les Grands Explorateurs du Moyen Âge', 3, 2019, 'Histoire'),
('L''Âge des Découvertes', 4, 2020, 'Histoire'),
('Les Rois et les Guerres : Une Chronique des Temps Anciens', 5, 2017, 'Histoire'),
('Les Civilisations Perdues', 6, 2021, 'Histoire'),
('La Guerre de Cent Ans : Stratégies et Destins', 7, 2020, 'Histoire'),
('Le Secret des Pharaons', 8, 2022, 'Histoire'),
('La Route de la Soie : Trafic et Conquêtes', 9, 2021, 'Histoire'),
('Le Moyen Âge à travers les Yeux des Peuples', 10, 2020, 'Histoire');

-- Insertion des livres pour le genre 'Philosophie'
INSERT INTO Livres (Titre, AuteurID, AnneePublication, Genre)
VALUES 
('La Quête de la Vérité', 1, 2020, 'Philosophie'),
('Éthique et Morale : L''Homme face à ses Dilemmes', 2, 2019, 'Philosophie'),
('La Philosophie de l''Existence', 3, 2021, 'Philosophie'),
('Le Paradoxe de la Liberté', 4, 2020, 'Philosophie'),
('La Sagesse des Anciens', 5, 2018, 'Philosophie'),
('Réflexions sur le Sens de la Vie', 6, 2021, 'Philosophie'),
('L''Art de la Pensée Critique', 7, 2020, 'Philosophie'),
('Le Conflit entre Raison et Foi', 8, 2022, 'Philosophie'),
('La Philosophie du Bonheur', 9, 2021, 'Philosophie'),
('Les Grandes Théories de la Connaissance', 10, 2020, 'Philosophie');

-- Insertion des livres pour le genre 'Art'
INSERT INTO Livres (Titre, AuteurID, AnneePublication, Genre)
VALUES 
('Les Grands Maîtres de la Peinture', 1, 2022, 'Art'),
('L''Histoire de l''Art Moderne', 2, 2020, 'Art'),
('Les Techniques de la Sculpture', 3, 2019, 'Art'),
('L''Art de la Photographie Contemporaine', 4, 2021, 'Art'),
('Peindre avec les Cieux : L''Art Abstrait', 5, 2022, 'Art'),
('Les Secrets des Maquilleurs Artistiques', 6, 2020, 'Art'),
('L''Influence de l''Art dans la Société', 7, 2018, 'Art'),
('Les Courants Artistiques du 20e Siècle', 8, 2021, 'Art'),
('Le Dessin au-delà des Contours', 9, 2020, 'Art'),
('L''Expression Artistique au Féminin', 10, 2022, 'Art');

-- Insertion des livres pour le genre 'Voyage'
INSERT INTO Livres (Titre, AuteurID, AnneePublication, Genre)
VALUES 
('Autour du Monde en 80 Jours', 1, 2017, 'Voyage'),
('À la Découverte des Îles Perdues', 2, 2021, 'Voyage'),
('Voyager en Solitaire : Mon Expérience', 3, 2019, 'Voyage'),
('Les Merveilles Inconnues de l''Asie', 4, 2020, 'Voyage'),
('Explorer l''Afrique : Les Grands Espaces', 5, 2018, 'Voyage'),
('Voyage au Coeur de l''Amérique Latine', 6, 2022, 'Voyage'),
('Les Routes Perdues d''Europe', 7, 2021, 'Voyage'),
('Le Tour du Monde en 365 Jours', 8, 2019, 'Voyage'),
('Les Plages Secrets du Pacifique', 9, 2020, 'Voyage'),
('Voyager autrement : L''Art du Slow Travel', 10, 2022, 'Voyage');

-- Insertion des livres pour le genre 'Finance'
INSERT INTO Livres (Titre, AuteurID, AnneePublication, Genre)
VALUES 
('Les Secrets de l''Investissement', 1, 2021, 'Finance'),
('La Stratégie Financière Personnelle', 2, 2020, 'Finance'),
('Le Pouvoir de l''Épargne', 3, 2022, 'Finance'),
('Comprendre la Bourse et les Marchés', 4, 2019, 'Finance'),
('Les Clés de la Réussite Financière', 5, 2020, 'Finance'),
('Crypto-monnaies : L''Avenir de l''Investissement', 6, 2021, 'Finance'),
('Les Bases de la Gestion d''Entreprises', 7, 2022, 'Finance'),
('Réussir ses Investissements Immobiliers', 8, 2021, 'Finance'),
('Les Investissements Responsables', 9, 2020, 'Finance'),
('La Finance Décryptée pour les Débutants', 10, 2022, 'Finance');


