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
    ArtworkID INT,
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

CREATE TABLE Clients (
    ClientID VARCHAR(200) NOT NULL,
    ClientName VARCHAR(200) NULL,
    ClientSecrets VARCHAR(2000) NULL, -- Store as a hash
    AllowedGrantTypes VARCHAR(200) NOT NULL,
    AllowedScopes VARCHAR(2000) NULL,
    RedirectUris VARCHAR(2000) NULL,
    PostLogoutRedirectUris VARCHAR(2000) NULL,
    AllowedCorsOrigins VARCHAR(2000) NULL,
    IsActive BOOLEAN NOT NULL DEFAULT TRUE,
    PRIMARY KEY (ClientID)
);

CREATE TABLE Resources (
    Name VARCHAR(200) NOT NULL,
    DisplayName VARCHAR(200) NULL,
    Description VARCHAR(1000) NULL,
    Type VARCHAR(50) NOT NULL, -- 'Identity' or 'API'
    PRIMARY KEY (Name)
);

CREATE TABLE Users (
    UserID INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(200) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL, -- Renamed to indicate hashing, length increased for hash storage
    IsActive BOOLEAN NOT NULL DEFAULT TRUE
);


CREATE TABLE Role (
    RoleID INT AUTO_INCREMENT PRIMARY KEY,
    RoleName VARCHAR(255) NOT NULL UNIQUE
);

CREATE TABLE UserClaim (
    UserClaimID INT AUTO_INCREMENT PRIMARY KEY,
    UserID INT NOT NULL,
    ClaimType VARCHAR(255) NOT NULL,
    ClaimValue VARCHAR(255) NOT NULL,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

CREATE TABLE UserRole (
    UserID INT NOT NULL,
    RoleID INT NOT NULL,
    PRIMARY KEY (UserID, RoleID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (RoleID) REFERENCES Role(RoleID)
);

INSERT INTO Role (RoleName) VALUES 
('Admin'), 
('Editor'), 
('Viewer'),
('Artist');

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

INSERT INTO Artists (FirstName, LastName) VALUES 
('Sylvain', 'Breton');

INSERT INTO Artwork (Title, CreationDate, CategoryID, CategoryName, Materials, Dimensions, Description, Conceptual) VALUES 
('Dreaming Sarah', '2011-01-01', 4, 'Performance', 'Digital work', '1.5cm X 1.5cm', 'Metropology: Love as a life statement commodity, City Intelligence by Money.', '(Conceptual art)'),
('Red Line', '2011-01-01', 6, 'Photography', 'Digital photography', '1.5cm X 1.5cm', 'Metropology: Love as a life statement commodity, City Intelligence by Money.', '(Conceptual art)'),
('Bathroom', '2011-01-01', 4, 'Photography', 'Mixed media', '1.5cm X 1.5cm', 'Agency of Possibilities and Impossibilities.', '(Conceptual art)'),
('Red Tornado', '2011-01-01', 4, 'Performance', 'Glass, water, red pencil, wooden table', '1.5cm X 1.5cm', 'Agency of Possibilities and Impossibilities.', '(Conceptual art)'),
('Unknown', '2011-01-01', 4, 'Performance', 'Digital work', '74 X 53 inches', 'Agency of Possibilities and Impossibilities.', '(Conceptual art)'),
('Statements', '2013-09-12', 8,'Sentence', 'Estonian & Russian newspapers, orange ink', '74 X 53 inches', 'M6 : Exhibition as a School, Art Factory Polymer', '(Conceptual art)');

INSERT INTO Place (Name, PlaceType, Address, Country) VALUES 
('Web Space', 'Public', 'sylvainbreton.com', 'Canada'),
('Museum Space', 'Private', '123 Museum Drive', 'France');

INSERT INTO Performance (Title, PerformanceDate, Materials, Description, PlaceID) VALUES 
('Dreaming Sarah Performance', '2011-01-01', 'Digital Image', 'Performance of Dreaming Sarah', 1),
('What To Do With The Contemporary?', '2011-01-01', 'Digital Image', 'Discussion about contemporary art', 2);

-- images (artwork images)
INSERT INTO Image (ArtworkID, PerformanceID, FileName, FilePath, URL) VALUES 
(1, NULL, 'dreaming-sarah.jpg', '/assets/images/', '/assets/images/dreaming-sarah.jpg'),
(2, NULL, 'red-line.jpg', '/assets/images/', '/assets/images/red-line.jpg'),
(3, NULL, 'bathroom-m6-event-032.jpg', '/assets/images/', '/assets/images/bathroom-m6-event-032.jpg'),
(4, NULL, 'red-tornado-center---something-043.jpg', '/assets/images/', '/assets/images/red-tornado-center---something-043.jpg'),
(5, NULL, 'unknown.jpg', '/assets/images/', '/assets/images/unknown.jpg'),
(6, NULL, 'statements-art-007.png', '/assets/images/', '/assets/images/statements-art-007.png');

-- images (not artwork images)
INSERT INTO Image (ArtworkID, PerformanceID, FileName, FilePath, URL, Description, MediaType, MediaDescription) VALUES 
(NULL, NULL, 'byanalogy-logo.jpg', '/assets/images/', '/assets/images/byanalogy-logo.jpg', 'exhibition image', 'jpg', 'the next documenta should be curated by a performance artist'),
(NULL, NULL, 'world.webp', '/assets/images/', '/assets/images/world.webp', 'world image', 'Webp', 'world with iron lines'),
(NULL, NULL, 'bg-mousse.jpg', '/assets/images/', '/assets/images/bg-mousse.jpg', 'journal print paper background', 'jpg', 'mousse magazine paper background'),
(NULL, NULL, 'no-image.jpg', '/assets/images/', '/assets/images/no-image.jpg', 'placehorder image', 'webp', 'no image written');

INSERT INTO ArtworkImage (ArtworkID, ImageID) VALUES 
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6);

INSERT INTO Event (Title, StartDate, EndDate, PlaceID, Description) VALUES 
('Reseau Contact', '2008-05-21', '2009-07-12', 1, 'agency of possibilities and impossibilities (love as a life statement commodity), metropology: city intelligence by money.'),
('Playa del Carmen Lotilus', '2008-05-21', '2009-07-12', 2, 'Christmas Party in Mexico');

INSERT INTO EventArtwork (EventID, ArtworkID) VALUES 
(1, 1),
(1, 2),
(2, 3);

INSERT INTO Sentence (ArtworkID, Author, PublicationDate, BookTitle, Publisher, SentencePage, Content, CountryOfPublication, CityOfPublication) VALUES 
(NULL, ' Joao Ribas', '2011-01-01', ' What To Do with The Contemporary?', ' Fiorucci Art Trust in Mousse Editions', 67, ' the only thing self-evident about the contemporary is that nothing concerning the contemporary is seft-evident anymore', ' Italy', ' Milan'),
(NULL, ' Theodor W. Adorno', '1970-01-01', ' Aesthetic Theory', ' Suhrkamp Verlag', 1, ' From Adorno statement: " it is self-evident that nothing concerning art is self-evident anymore, not its inner life, not its relationship to the world, not even its right to exist"', 'Germany', 'Frankfurt am Main');
