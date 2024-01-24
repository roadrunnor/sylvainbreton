DROP DATABASE IF EXISTS SylvainBreton;
CREATE DATABASE SylvainBreton;
USE SylvainBreton;

CREATE TABLE Artists (
    ArtistID INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(255),
    LastName VARCHAR(255)
);

CREATE TABLE Category (
    CategoryID INT AUTO_INCREMENT PRIMARY KEY,
    CategoryName VARCHAR(255) NOT NULL
);

CREATE TABLE Artwork (
    ArtworkID INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(255),
    CreationDate DATE,
    CategoryID INT,
    CategoryName VARCHAR(255),
    Materials VARCHAR(255),
    Dimensions VARCHAR(255),
    Description TEXT,
    Conceptual TEXT,
    FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID)
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

CREATE TABLE Image (
    ImageID INT AUTO_INCREMENT PRIMARY KEY,
    ArtworkID INT NULL,
    PerformanceID INT NULL,
    FileName VARCHAR(255),
    FilePath VARCHAR(255),
    URL VARCHAR(255),
    Description TEXT,
    MediaType VARCHAR(50),
    MediaDescription TEXT,
    FOREIGN KEY (ArtworkID) REFERENCES Artwork(ArtworkID),
    FOREIGN KEY (PerformanceID) REFERENCES Performance(PerformanceID)
);

CREATE TABLE ArtworkImage (
    ArtworkID INT,
    ImageID INT,
    FileName VARCHAR(255),
    FilePath VARCHAR(255),
    URL	VARCHAR(255),
    FOREIGN KEY (ArtworkID) REFERENCES Artwork(ArtworkID),
    FOREIGN KEY (ImageID) REFERENCES Image(ImageID)
);

CREATE TABLE EventArtwork (
    EventID INT,
    ArtworkID INT,
    PRIMARY KEY (EventID, ArtworkID),
    FOREIGN KEY (EventID) REFERENCES Event(EventID),
    FOREIGN KEY (ArtworkID) REFERENCES Artwork(ArtworkID)
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

INSERT INTO Artists (FirstName, LastName) VALUES 
('Sylvain', 'Breton');

INSERT INTO Category (CategoryName) VALUES 
('Drawing'), 
('Painting'), 
('Sculpture'), 
('Performance'), 
('Installation'), 
('Photography'), 
('Video'), 
('Sentence');

INSERT INTO Artwork (Title, CreationDate, CategoryID, CategoryName, Materials, Dimensions, Description, Conceptual) VALUES 
('Dreaming Sarah', '2011-01-01', 6, 'Photography', 'Digital Image', '1.5cm X 1.5cm', 'Agency of Possibilities and Impossibilities: Love as a life statement commodity. Metropology: City intelligence by Money.', 'Conceptual Art'),
('Red Line', '2011-01-01', 6, 'Photography', 'Digital Image', '1.5cm X 1.5cm', 'Agency of Possibilities and Impossibilities: Love as a life statement commodity. Metropology: City intelligence by Money.', 'Conceptual Art'),
('Bathroom', '2011-01-01', 6, 'Photography', 'Digital Image', '1.5cm X 1.5cm', 'Agency of Possibilities and Impossibilities: Love as a life statement commodity. Metropology: City intelligence by Money.', 'Conceptual Art'),
('ByAnalogy', '2011-01-01', 6, 'Photography', 'Digital Image', '1.5cm X 1.5cm', 'Agency of Possibilities and Impossibilities: Love as a life statement commodity. Metropology: City intelligence by Money.', 'Conceptual Art'),
('Red Tornado', '2011-01-01', 4, 'Photography', 'Digital Image', '1.5cm X 1.5cm', 'Agency of Possibilities and Impossibilities: Love as a life statement commodity. Metropology: City intelligence by Money.', 'Conceptual Art'),
('Joao Ribas', '2011-01-01', 8, 'Photography', 'Sentence, Letters', '110 character', 'Agency of Possibilities and Impossibilities: Love as a life statement commodity. Metropology: City intelligence by Money.', 'Conceptual Art'),
('Theodor W. Adorno', '1970-01-01', 8, 'Sentence (Conceptual Art)', 'Sentence, Letters', '110 character', 'Agency of Possibilities and Impossibilities: Love as a life statement commodity. Metropology: City intelligence by Money.', 'Conceptual Art'),
('Unknown', '2011-01-01', 2, 'Painting', 'Oil on canvas', '74 X 53 inches', 'Metropology: City intelligence by Money.', 'Conceptual Art, Painting, Performance, Installation');

INSERT INTO DynamicContent (Keyword, Content) VALUES 
('background', 'background');

INSERT INTO Place (Name, PlaceType, Address, Country) VALUES 
('Web Space', 'Public', 'sylvainbreton.com', 'Canada'),
('Web Space', 'Public', 'sylvainbreton.com', 'Canada');

INSERT INTO Performance (Title, PerformanceDate, Materials, Description, PlaceID) VALUES 
('Dreaming Sarah Performance', '2011-01-01', 'Web', NULL, 1),
('What To Do With The Contemporary?', '2011-01-01', 'Web', NULL, 2);

-- Insert Images artistiques
INSERT INTO Image (ArtworkID, FileName, Description, MediaType, MediaDescription) VALUES 
(1, 'dreaming-sarah.jpg', 'agency of possibilities and impossibilities (love as a life statement commodity), metropology: city intelligence by money.', 'photography', 'birks ring'),
(2, 'red-line.jpg', 'metropology: city intelligence by money.', 'digital photography', 'Mexico bar, neon, playa del carmen'),
(3, 'bathroom-m6-event-032.jpg', 'Metropology: City Intelligence by Money.', 'performance', 'culture factory polymer'),
(4, 'byanalogy_logo.jpg', 'agency of possibilities and impossibilities.', 'print', 'performance and print'),
(5, 'red-tornado-center---something-043.jpg', 'agency of possibilities and impossibilities.', 'performance', 'red pencil, water, glass'),
(6, 'unknown.jpg', 'painting', 'metropology: city intelligence by money.', 'Conceptual art, painting, performance, installation');

-- Insert ArtworkImage associations
INSERT INTO ArtworkImage (ArtworkID, ImageID, FileName, FilePath, URL) VALUES 
(1, 1, 'dreaming-sarah.jpg', 'public/assets/images/dreaming-sarah.jpg', '/assets/images/dreaming-sarah.jpg'),
(2, 2, 'red-line.jpg', 'public/assets/images/red-line.jpg', '/images/red-line.jpg'),
(3, 3, 'bathroom-m6-event-032.jpg', 'public/assets/images/bathroom-m6-event-032.jpg', '/assets/images/bathroom-m6-event-032.jpg'),
(4, 4, 'byanalogy_logo.jpg', 'public/assets/images/byanalogy_logo.jpg', '/assets/images/byanalogy_logo.jpg'),
(5, 5, 'red-tornado-center---something-043.jpg', 'public/assets/images/red-tornado-center---something-043.jpg', '/assets/images/red-tornado-center---something-043.jpg'),
(6, 6, 'unknown.jpg', 'public/assets/images/unknown.jpg', '/assets/images/unknown.jpg');

-- Insert Images non artistiques
INSERT INTO Image (FileName, Description, MediaType, MediaDescription) VALUES 
('world.webp', 'world image', 'Webp', 'world with iron lines'),
('bg-mousse', 'journal print paper background', 'jpg', 'mousse magazine paper background'),
('no-image', 'image missing', 'svg', 'two mountains with a sun'),
('no-image', 'image missing', 'webp', 'no image');

INSERT INTO Sentence (ArtworkID, Author, PublicationDate, BookTitle, Publisher, SentencePage, Content, CountryOfPublication, CityOfPublication) VALUES 
(6, ' Joao Ribas', '2011-01-01', ' What To Do with The Contemporary?', ' Fiorucci Art Trust in Mousse Editions', 67, ' the only thing self-evident about the contemporary is that nothing concerning the contemporary is seft-evident anymore', ' Italy', ' Milan'),
(6, ' Theodor W. Adorno', '1970-01-01', ' Aesthetic Theory', ' Suhrkamp Verlag', 1, ' From Adorno statement: " it is self-evident that nothing concerning art is self-evident anymore, not its inner life, not its relationship to the world, not even its right to exist"', 'Germany', 'Frankfurt am Main');
