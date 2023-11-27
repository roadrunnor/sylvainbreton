DROP DATABASE IF EXISTS SylvainBreton;
CREATE DATABASE SylvainBreton;
USE SylvainBreton;

CREATE TABLE Artwork (
    ArtworkID INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(255),
    CreationDate YEAR,
    ArtworkType VARCHAR(50), -- Peinture, Sculpture, Dessin, Photographie, Installation, Conceptual, Performance
    Materials VARCHAR(255),
    Dimensions VARCHAR(255),
    Description TEXT,
    Conceptual TEXT
);

CREATE TABLE Places (
    PlaceID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255),
    PlaceType VARCHAR(50), -- Public, Privé (Galerie, Musée, etc.)
    Address VARCHAR(255),
    Country VARCHAR(255)
);

CREATE TABLE Performances (
    PerformanceID INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(255),
    PerformanceDate YEAR,
    Materials VARCHAR(255),
    Description TEXT,
    PlaceID INT,
    FOREIGN KEY (PlaceID) REFERENCES Places(PlaceID)
);

CREATE TABLE Events (
    EventID INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(255),
    StartDate DATE,
    EndDate DATE,
    PlaceID INT,
    Description TEXT,
    FOREIGN KEY (PlaceID) REFERENCES Places(PlaceID)
);

CREATE TABLE EventArtworks (
    EventID INT,
    ArtworkID INT,
    PRIMARY KEY (EventID, ArtworkID),
    FOREIGN KEY (EventID) REFERENCES Events(EventID),
    FOREIGN KEY (ArtworkID) REFERENCES Artwork(ArtworkID)
);

CREATE TABLE Images (
    ImageID INT AUTO_INCREMENT PRIMARY KEY,
    ArtworkID INT,
    PerformanceID INT,
    FileRoute VARCHAR(255),
    Description TEXT,
    MediaType VARCHAR(50),
    MediaDescription TEXT,
    FOREIGN KEY (ArtworkID) REFERENCES Artwork(ArtworkID),
    FOREIGN KEY (PerformanceID) REFERENCES Performances(PerformanceID)
);

CREATE TABLE Sentences (
    SentenceID INT AUTO_INCREMENT PRIMARY KEY,
    ArtworkID INT,
    Author VARCHAR(255),
    PublicationDate YEAR,
    BookTitle VARCHAR(255),
    Publisher VARCHAR(255),
    SentencePage INT,
    Content TEXT, -- Colonne ajoutée pour stocker le contenu de la citation
    CountryOfPublication VARCHAR(100),
    CityOfPublication VARCHAR(100),
    FOREIGN KEY (ArtworkID) REFERENCES Artwork(ArtworkID)
);

-- Exemples d'insertions de données pour chaque table
-- (Vous devrez adapter ou compléter ces exemples selon vos données spécifiques)

-- Insertion d'une œuvre d'art
INSERT INTO Artwork (Title, CreationDate, ArtworkType, Materials, Dimensions, Description, Conceptual)
VALUES ('Dreaming Sarah', '2011', 'Conceptual', 'Web Art', '1.5cm X 1.5cm', 'Agency of Possibilities and Impossibilities: Love as a life statement commodity. Metropology: City intelligence by Money', 'Conceptual art description');
INSERT INTO Artwork (Title, CreationDate, ArtworkType, Materials, Dimensions, Description, Conceptual)
VALUES ('What To Do With The Contemporary?', '2011', 'Performance', 'Digital Work', NULL, 'Agency of Possibilities and Impossibilities.', 'Immaterial Art');

-- Insertion d'un lieu
INSERT INTO Places (Name, PlaceType, Address, Country)
VALUES ('Web Space', 'Public', 'sylvainbreton.com', 'Canada');
INSERT INTO Places (Name, PlaceType, Address, Country)
VALUES ('Web Space', 'Public', 'sylvainbreton.com', 'Canada');

-- Insertion d'une performance
INSERT INTO Performances (Title, PerformanceDate, Materials, Description, PlaceID)
VALUES ('Dreaming Sarah Performance', '2011', 'Web', NULL, 1);
INSERT INTO Performances (Title, PerformanceDate, Materials, Description, PlaceID)
VALUES ('What To Do With The Contemporary?', '2011', 'Web', NULL, 2);

-- Insertion d'un événement
-- INSERT INTO Events (Title, StartDate, EndDate, PlaceID, Description)
-- VALUES ('Reflets Contemporains', '2023-06-01', '2023-08-30', 1, 'Une exposition collective mettant en lumière des artistes contemporains.');

-- Insertion d'une relation Artwork-Event
-- INSERT INTO EventArtworks (EventID, ArtworkID)
-- VALUES (1, 1);

-- Insertion d'une image
INSERT INTO Images (ArtworkID, FileRoute, Description, MediaType, MediaDescription)
VALUES (1, 'E:\Websites\breton\public\images\DreamingSarah\ring.jpg', 'Agency of Possibilities and Impossibilities - Love as a life statement commodity. Metropology: City intelligence by Money', 'Photography', 'Image taken from Birks');

-- Insertion d'une autre citation d'un livre liée à la même œuvre d'art (ArtworkID = 1)
INSERT INTO Sentences (ArtworkID, Author, PublicationDate, BookTitle, Publisher, SentencePage, Content, CountryOfPublication, CityOfPublication)
VALUES (2, 'Joao Ribas', '2011', 'What To Do With The Contemporary?', 'Fiorucci Art Trust with Mousse Editions', 67, 'the only thing self-evident about the contemporary is that nothing concerning the contemporary is seft-evident anymore','Italy', 'Milan');
INSERT INTO Sentences (ArtworkID, Author, PublicationDate, BookTitle, Publisher, SentencePage, Content, CountryOfPublication, CityOfPublication)
VALUES (2, 'Theodor Adorno', NULL, 'Aesthetic Theory', NULL, NULL, '(From Adorno Statement : "it is self-evident that nothing concerning art is self-evident anymore, not its inner life, not its relationship to the world, not even its right to exist")', NULL, NULL);

