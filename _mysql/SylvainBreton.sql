DROP DATABASE IF EXISTS SylvainBreton;
CREATE DATABASE SylvainBreton;
USE SylvainBreton;

CREATE TABLE Artists (
    ArtistID INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(255),
    LastName VARCHAR(255)
);

CREATE TABLE Artwork (
    ArtworkID INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(255),
    CreationDate DATE,
    ArtworkType VARCHAR(50), -- Peinture, Sculpture, Dessin, Photographie, Installation, Conceptual, Performance
    Materials VARCHAR(255),
    Dimensions VARCHAR(255),
    Description TEXT,
    Conceptual TEXT
);

CREATE TABLE Place (
    PlaceID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255),
    PlaceType VARCHAR(50), -- Public, Privé (Galerie, Musée, etc.)
    Address VARCHAR(255),
    Country VARCHAR(255)
);

CREATE TABLE Performance (
    PerformanceID INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(255),
    PerformanceDate DATE,
    Materials VARCHAR(255),
    Description TEXT,
    PlaceID INT,
    FOREIGN KEY (PlaceID) REFERENCES Place(PlaceID)
);

CREATE TABLE Event (
    EventID INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(255),
    StartDate DATE,
    EndDate DATE,
    PlaceID INT,
    Description TEXT,
    FOREIGN KEY (PlaceID) REFERENCES Place(PlaceID)
);

CREATE TABLE EventArtwork (
    EventID INT,
    ArtworkID INT,
    PRIMARY KEY (EventID, ArtworkID),
    FOREIGN KEY (EventID) REFERENCES Event(EventID),
    FOREIGN KEY (ArtworkID) REFERENCES Artwork(ArtworkID)
);

CREATE TABLE Image (
    ImageID INT AUTO_INCREMENT PRIMARY KEY,
    ArtworkID INT NULL,
    PerformanceID INT NULL,
    FileRoute VARCHAR(255),
    Description TEXT,
    MediaType VARCHAR(50),
    MediaDescription TEXT,
    FOREIGN KEY (ArtworkID) REFERENCES Artwork(ArtworkID),
    FOREIGN KEY (PerformanceID) REFERENCES Performance(PerformanceID)
);


CREATE TABLE Sentence (
    SentenceID INT AUTO_INCREMENT PRIMARY KEY,
    ArtworkID INT,
    Author VARCHAR(255),
    PublicationDate DATE,
    BookTitle VARCHAR(255),
    Publisher VARCHAR(255),
    SentencePage INT,
    Content TEXT, -- Colonne ajoutée pour stocker le contenu de la citation
    CountryOfPublication VARCHAR(100),
    CityOfPublication VARCHAR(100),
    FOREIGN KEY (ArtworkID) REFERENCES Artwork(ArtworkID)
);

CREATE TABLE DynamicContent (
    ContentID INT AUTO_INCREMENT PRIMARY KEY,
    Keyword VARCHAR(255),
    Content TEXT
);

-- Insert dynamic content
INSERT INTO DynamicContent (Keyword, Content)
VALUES ('background', 'background');

INSERT INTO Artists (FirstName, LastName)
VALUES ('Sylvain', 'Breton');

-- Exemples d'insertions de données pour chaque table
-- (Vous devrez adapter ou compléter ces exemples selon vos données spécifiques)

-- Insertion d'une œuvre d'art
INSERT INTO Artwork (Title, CreationDate, ArtworkType, Materials, Dimensions, Description, Conceptual)
VALUES ('Dreaming Sarah', '2011-01-01', 'Conceptual', 'Web Art', '1.5cm X 1.5cm', 'Agency of Possibilities and Impossibilities: Love as a life statement commodity. Metropology: City intelligence by Money', 'Conceptual art description');
INSERT INTO Artwork (Title, CreationDate, ArtworkType, Materials, Dimensions, Description, Conceptual)
VALUES ('What To Do With The Contemporary?', '2011-01-01', 'Performance', 'Digital Work', NULL, 'Agency of Possibilities and Impossibilities.', 'Immaterial Art');

-- Insertion d'un lieu
INSERT INTO Place (Name, PlaceType, Address, Country)
VALUES ('Web Space', 'Public', 'sylvainbreton.com', 'Canada');
INSERT INTO Place (Name, PlaceType, Address, Country)
VALUES ('Web Space', 'Public', 'sylvainbreton.com', 'Canada');

-- Insertion d'une performance
INSERT INTO Performance (Title, PerformanceDate, Materials, Description, PlaceID)
VALUES ('Dreaming Sarah Performance', '2011-01-01', 'Web', NULL, 1);
INSERT INTO Performance (Title, PerformanceDate, Materials, Description, PlaceID)
VALUES ('What To Do With The Contemporary?', '2011-01-01', 'Web', NULL, 2);

-- Insertion d'un événement
-- INSERT INTO Event (Title, StartDate, EndDate, PlaceID, Description)
-- VALUES ('Reflets Contemporains', '2023-06-01', '2023-08-30', 1, 'Une exposition collective mettant en lumière des artistes contemporains.');

-- Insertion d'une relation Artwork-Event
-- INSERT INTO EventArtwork (EventID, ArtworkID)
-- VALUES (1, 1);

-- Insertion d'une image artistique
INSERT INTO Image (ArtworkID, FileRoute, Description, MediaType, MediaDescription)
VALUES (1, 'E:\Websites\breton\public\Image\DreamingSarah\ring.jpg', 'Agency of Possibilities and Impossibilities - Love as a life statement commodity. Metropology: City intelligence by Money', 'Photography', 'Image taken from Birks');

-- Insertion d'une image non artistique
INSERT INTO Image (FileRoute, Description, MediaType, MediaDescription)
VALUES ('E:\Websites\breton\public\Image/world/world.webp', 'Description of the World Image', 'WebP', 'World Image');

-- Insertion d'une autre citation d'un livre liée à la même œuvre d'art (ArtworkID = 1)
INSERT INTO Sentence (ArtworkID, Author, PublicationDate, BookTitle, Publisher, SentencePage, Content, CountryOfPublication, CityOfPublication)
VALUES (2, 'Joao Ribas', '2011-01-01', 'What To Do With The Contemporary?', 'Fiorucci Art Trust with Mousse Editions', 67, 'the only thing self-evident about the contemporary is that nothing concerning the contemporary is seft-evident anymore','Italy', 'Milan');
INSERT INTO Sentence (ArtworkID, Author, PublicationDate, BookTitle, Publisher, SentencePage, Content, CountryOfPublication, CityOfPublication)
VALUES (2, 'Theodor W. Adorno', '1970-01-01', 'Aesthetic Theory', 'PublisherName', 123, '(From Adorno Statement : "it is self-evident that nothing concerning art is self-evident anymore, not its inner life, not its relationship to the world, not even its right to exist")', 'Germany', 'Frankfurt');

