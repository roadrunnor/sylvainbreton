DROP DATABASE IF EXISTS SylvainBreton;
CREATE DATABASE SylvainBreton;
USE SylvainBreton;

CREATE TABLE Artists (
    ArtistID INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(255) NOT NULL,
    LastName VARCHAR(255) NOT NULL
);

CREATE TABLE Category (
    CategoryID INT AUTO_INCREMENT PRIMARY KEY,
    CategoryName VARCHAR(255) NOT NULL
);

CREATE TABLE Artwork (
    ArtworkID INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    CreationDate DATE NOT NULL,
    CategoryID INT NOT NULL,
    CategoryName VARCHAR(255) NOT NULL,
    Materials VARCHAR(255) NOT NULL,
    Dimensions VARCHAR(255) NOT NULL,
    Description TEXT NOT NULL,
    Conceptual TEXT NOT NULL,
    FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID)
);

CREATE TABLE Place (
    PlaceID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    PlaceType VARCHAR(50) NOT NULL, -- Public, Privé (Galerie, Musée, etc.)
    Address VARCHAR(255) NOT NULL,
    Country VARCHAR(255) NOT NULL
);

CREATE TABLE Performance (
    PerformanceID INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    PerformanceDate DATE NOT NULL,
    Materials VARCHAR(255) NOT NULL,
    Description TEXT NOT NULL,
    PlaceID INT NOT NULL,
    FOREIGN KEY (PlaceID) REFERENCES Place(PlaceID)
);

CREATE TABLE Event (
    EventID INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    PlaceID INT NOT NULL,
    Description TEXT NOT NULL,
    FOREIGN KEY (PlaceID) REFERENCES Place(PlaceID)
);

CREATE TABLE Image (
    ImageID INT AUTO_INCREMENT PRIMARY KEY,
    ArtworkID INT,
    PerformanceID INT,
    FileName VARCHAR(255) NOT NULL,
    FilePath VARCHAR(255) NOT NULL,
    URL VARCHAR(255) NOT NULL,
    Description TEXT,
    MediaType VARCHAR(50),
    MediaDescription TEXT,
    FOREIGN KEY (ArtworkID) REFERENCES Artwork(ArtworkID),
    FOREIGN KEY (PerformanceID) REFERENCES Performance(PerformanceID)
);

CREATE TABLE ArtworkImage (
    ArtworkID INT NOT NULL,
    ImageID INT NOT NULL,
    PRIMARY KEY (ArtworkID, ImageID),
    FOREIGN KEY (ArtworkID) REFERENCES Artwork(ArtworkID),
    FOREIGN KEY (ImageID) REFERENCES Image(ImageID)
);

CREATE TABLE EventArtwork (
    EventID INT NOT NULL,
    ArtworkID INT NOT NULL,
    PRIMARY KEY (EventID, ArtworkID),
    FOREIGN KEY (EventID) REFERENCES Event(EventID),
    FOREIGN KEY (ArtworkID) REFERENCES Artwork(ArtworkID)
);

CREATE TABLE Sentence (
    SentenceID INT AUTO_INCREMENT PRIMARY KEY,
    ArtworkID INT NOT NULL,
    Author VARCHAR(255) NOT NULL,
    PublicationDate DATE NOT NULL,
    BookTitle VARCHAR(255) NOT NULL,
    Publisher VARCHAR(255) NOT NULL,
    SentencePage INT NOT NULL,
    Content TEXT NOT NULL, -- Colonne ajoutée pour stocker le contenu de la citation
    CountryOfPublication VARCHAR(100) NOT NULL,
    CityOfPublication VARCHAR(100) NOT NULL,
    FOREIGN KEY (ArtworkID) REFERENCES Artwork(ArtworkID)
);

CREATE TABLE DynamicContent (
    ContentID INT AUTO_INCREMENT PRIMARY KEY,
    Keyword VARCHAR(255) NOT NULL,
    Content TEXT NOT NULL
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
('Dreaming Sarah', '2011-01-01', 4, 'Performance', 'Digital', '1.5cm X 1.5cm', 'Agency of Possibilities and Impossibilities: Love as a life statement commodity. Metropology: City intelligence by Money.', 'Conceptual Art'),
('Red Line', '2011-01-01', 6, 'Photography', 'Digital', '1.5cm X 1.5cm', 'Agency of Possibilities and Impossibilities: Love as a life statement commodity. Metropology: City intelligence by Money.', 'Conceptual Art'),
('Bathroom', '2011-01-01', 4, 'Performance', 'Digital', '1.5cm X 1.5cm', 'Agency of Possibilities and Impossibilities: Love as a life statement commodity. Metropology: City intelligence by Money.', 'Conceptual Art'),
('ByAnalogy', '2011-01-01', 6, 'Photography', 'Digital', '1.5cm X 1.5cm', 'Agency of Possibilities and Impossibilities: Love as a life statement commodity. Metropology: City intelligence by Money.', 'Conceptual Art'),
('Red Tornado', '2011-01-01', 4, 'Performance', 'Digital', '1.5cm X 1.5cm', 'Agency of Possibilities and Impossibilities: Love as a life statement commodity. Metropology: City intelligence by Money.', 'Conceptual Art'),
('Unknown', '2011-01-01', 4, 'Performance', 'Digital', '74 X 53 inches', 'Metropology: City intelligence by Money.', 'Conceptual Art, Painting, Performance, Installation'),
('Joao Ribas', '2011-01-01', 8, 'Sentence (Conceptual Art)', 'Digital', '80 characters', 'Agency of Possibilities and Impossibilities: Love as a life statement commodity. Metropology: City intelligence by Money.', 'Conceptual Art'),
('Theodor W. Adorno', '1970-01-01', 8, 'Sentence (Conceptual Art)', 'Digital', '210 characters', 'Agency of Possibilities and Impossibilities: Love as a life statement commodity. Metropology: City intelligence by Money.', 'Conceptual Art');

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
('Dreaming Sarah', '2011-01-01', 4, 'Performance', 'Digital Image', '1.5cm X 1.5cm', 'Agency of Possibilities and Impossibilities: Love as a life statement commodity. Metropology: City intelligence by Money.', 'Conceptual Art'),
('Red Line', '2011-01-01', 6, 'Photography', 'Digital Image', '1.5cm X 1.5cm', 'Agency of Possibilities and Impossibilities: Love as a life statement commodity. Metropology: City intelligence by Money.', 'Conceptual Art'),
('Bathroom', '2011-01-01', 4, 'Photography', 'Digital Image', '1.5cm X 1.5cm', 'Agency of Possibilities and Impossibilities: Love as a life statement commodity. Metropology: City intelligence by Money.', 'Conceptual Art'),
('ByAnalogy', '2011-01-01', 6, 'Photography', 'Digital Image', '1.5cm X 1.5cm', 'Agency of Possibilities and Impossibilities: Love as a life statement commodity. Metropology: City intelligence by Money.', 'Conceptual Art'),
('Red Tornado', '2011-01-01', 4, 'Performance', 'Digital Image', '1.5cm X 1.5cm', 'Agency of Possibilities and Impossibilities: Love as a life statement commodity. Metropology: City intelligence by Money.', 'Conceptual Art'),
('Unknown', '2011-01-01', 4, 'Performance', 'Digital Image', '74 X 53 inches', 'Metropology: City intelligence by Money.', 'Conceptual Art, Painting, Performance, Installation'),
('Joao Ribas', '2011-01-01', 8, 'Sentence (Conceptual Art)', 'Digital Image', '80 characters', 'Agency of Possibilities and Impossibilities: Love as a life statement commodity. Metropology: City intelligence by Money.', 'Conceptual Art'),
('Theodor W. Adorno', '1970-01-01', 8, 'Sentence (Conceptual Art)', 'Digital Image', '210 characters', 'Agency of Possibilities and Impossibilities: Love as a life statement commodity. Metropology: City intelligence by Money.', 'Conceptual Art');

INSERT INTO Place (Name, PlaceType, Address, Country) VALUES 
('Web Space', 'Public', 'sylvainbreton.com', 'Canada'),
('Museum Space', 'Private', '123 Museum Drive', 'France');

INSERT INTO Performance (Title, PerformanceDate, Materials, Description, PlaceID) VALUES 
('Dreaming Sarah Performance', '2011-01-01', 'Digital Image', 'Performance of Dreaming Sarah', 1),
('What To Do With The Contemporary?', '2011-01-01', 'Digital Image', 'Discussion about contemporary art', 2);

INSERT INTO Image (ArtworkID, PerformanceID, FileName, FilePath, URL, Description, MediaType, MediaDescription) VALUES 
(1, NULL, 'dreaming-sarah.jpg', '/assets/images/', '/assets/images/dreaming-sarah.jpg', 'agency of possibilities and impossibilities (love as a life statement commodity), metropology: city intelligence by money.', 'Photography', 'birks ring'),
(2, NULL, 'red-line.jpg', '/assets/images/', '/assets/images/red-line.jpg', 'metropology: city intelligence by money.', 'Digital Photography', 'Mexico bar, neon, playa del carmen'),
(3, NULL, 'bathroom-m6-event-032.jpg', '/assets/images/', '/assets/images/bathroom-m6-event-032.jpg', 'Metropology: City Intelligence by Money.', 'Performance', 'culture factory polymer'),
(4, NULL, 'red-tornado-center---something-043.jpg', '/assets/images/', '/assets/images/red-tornado-center---something-043.jpg', 'agency of possibilities and impossibilities.', 'Performance', 'red pencil, water, glass'),
(5, NULL, 'unknown.jpg', '/assets/images/', '/assets/images/unknown.jpg', 'painting', 'Metropology: city intelligence by money.', 'Conceptual Art, Painting, Performance, Installation');

INSERT INTO ArtworkImage (ArtworkID, ImageID) VALUES 
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5);

INSERT INTO Event (Title, StartDate, EndDate, PlaceID, Description) VALUES 
('Reseau Contact', '2008-05-21', '2009-07-12', 1, 'agency of possibilities and impossibilities (love as a life statement commodity), metropology: city intelligence by money.'),
('Playa del Carmen Lotilus', '2008-05-21', '2009-07-12', 2, 'Christmas Party in Mexico');


INSERT INTO EventArtwork (EventID, ArtworkID) VALUES 
(1, 1),
(1, 2),
(2, 3);


INSERT INTO Sentence (ArtworkID, Author, PublicationDate, BookTitle, Publisher, SentencePage, Content, CountryOfPublication, CityOfPublication) VALUES 
(7, ' Joao Ribas', '2011-01-01', ' What To Do with The Contemporary?', ' Fiorucci Art Trust in Mousse Editions', 67, ' the only thing self-evident about the contemporary is that nothing concerning the contemporary is seft-evident anymore', ' Italy', ' Milan'),
(8, ' Theodor W. Adorno', '1970-01-01', ' Aesthetic Theory', ' Suhrkamp Verlag', 1, ' From Adorno statement: " it is self-evident that nothing concerning art is self-evident anymore, not its inner life, not its relationship to the world, not even its right to exist"', 'Germany', 'Frankfurt am Main');
