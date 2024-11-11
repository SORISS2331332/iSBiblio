DROP DATABASE  IF EXISTS Bibliotheque;
-- Création de la base de données
CREATE DATABASE Bibliotheque;
GO

USE Bibliotheque;
GO

-- Table des Utilisateurs
CREATE TABLE Utilisateurs (
    UtilisateurID INT PRIMARY KEY IDENTITY(1,1),
    Nom NVARCHAR(100) NOT NULL,
    Prenom NVARCHAR(100) NOT NULL,
    Adresse NVARCHAR(255),
    Email NVARCHAR(255) UNIQUE NOT NULL,
    MotDePasse VARBINARY(255) NOT NULL, -- Stocké en hash
    DateInscription DATE DEFAULT GETDATE(),
    Sel             UNIQUEIDENTIFIER,
    Role     NVARCHAR(50) 
);

GO
-- Table des Auteurs
CREATE TABLE Auteurs (
    AuteurID INT PRIMARY KEY IDENTITY(1,1),
    Nom NVARCHAR(100) NOT NULL,
    Prenom NVARCHAR(100) NOT NULL,
    DateNaissance DATE
);

GO

-- Table des Livres
CREATE TABLE Livres (
    LivreID INT PRIMARY KEY IDENTITY(1,1),
    Titre NVARCHAR(255) NOT NULL,
    LienImage NVARCHAR(255) DEFAULT 'img/couverture.png',
    AuteurID INT NOT NULL  FOREIGN KEY REFERENCES Auteurs(AuteurID),
    AnneePublication DATE,
    Genre NVARCHAR(100),
    Disponibilite BIT DEFAULT 1
);
GO

-- Table des Emprunts
CREATE TABLE Emprunts (
    EmpruntID INT PRIMARY KEY IDENTITY(1,1),
    LivreID INT FOREIGN KEY REFERENCES Livres(LivreID),
    UtilisateurID INT FOREIGN KEY REFERENCES Utilisateurs(UtilisateurID),
    DateEmprunt DATE NOT NULL,
    DateRetour DATE,
    EstRendu BIT DEFAULT 0
);
GO







