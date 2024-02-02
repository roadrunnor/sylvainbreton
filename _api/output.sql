CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `Artists` (
    `ArtistID` int NOT NULL AUTO_INCREMENT,
    `FirstName` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `LastName` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Artists` PRIMARY KEY (`ArtistID`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Category` (
    `CategoryID` int NOT NULL AUTO_INCREMENT,
    `CategoryName` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Category` PRIMARY KEY (`CategoryID`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `DynamicContent` (
    `ContentID` int NOT NULL AUTO_INCREMENT,
    `Keyword` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Content` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_DynamicContent` PRIMARY KEY (`ContentID`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Place` (
    `PlaceID` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `PlaceType` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `Address` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Country` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Place` PRIMARY KEY (`PlaceID`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `Artwork` (
    `ArtworkID` int NOT NULL AUTO_INCREMENT,
    `Title` longtext CHARACTER SET utf8mb4 NULL,
    `CreationDate` datetime(6) NOT NULL,
    `CategoryID` int NOT NULL,
    `CategoryName` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Materials` varchar(500) CHARACTER SET utf8mb4 NULL,
    `Dimensions` varchar(100) CHARACTER SET utf8mb4 NULL,
    `Description` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `Conceptual` varchar(255) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Artwork` PRIMARY KEY (`ArtworkID`),
    CONSTRAINT `FK_Artwork_Category_CategoryID` FOREIGN KEY (`CategoryID`) REFERENCES `Category` (`CategoryID`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Event` (
    `EventID` int NOT NULL AUTO_INCREMENT,
    `Title` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `StartDate` datetime(6) NOT NULL,
    `EndDate` datetime(6) NOT NULL,
    `PlaceID` int NOT NULL,
    `Description` varchar(1000) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Event` PRIMARY KEY (`EventID`),
    CONSTRAINT `FK_Event_Place_PlaceID` FOREIGN KEY (`PlaceID`) REFERENCES `Place` (`PlaceID`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Performance` (
    `PerformanceID` int NOT NULL AUTO_INCREMENT,
    `Title` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `PerformanceDate` datetime(6) NOT NULL,
    `Materials` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `Description` varchar(1000) CHARACTER SET utf8mb4 NULL,
    `PlaceID` int NOT NULL,
    CONSTRAINT `PK_Performance` PRIMARY KEY (`PerformanceID`),
    CONSTRAINT `FK_Performance_Place_PlaceID` FOREIGN KEY (`PlaceID`) REFERENCES `Place` (`PlaceID`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Sentence` (
    `SentenceID` int NOT NULL AUTO_INCREMENT,
    `ArtworkID` int NULL,
    `Author` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `PublicationDate` datetime(6) NOT NULL,
    `BookTitle` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Publisher` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `SentencePage` int NOT NULL,
    `Content` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CountryOfPublication` varchar(255) CHARACTER SET utf8mb4 NULL,
    `CityOfPublication` varchar(255) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Sentence` PRIMARY KEY (`SentenceID`),
    CONSTRAINT `FK_Sentence_Artwork_ArtworkID` FOREIGN KEY (`ArtworkID`) REFERENCES `Artwork` (`ArtworkID`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `EventArtwork` (
    `EventID` int NOT NULL,
    `ArtworkID` int NOT NULL,
    CONSTRAINT `PK_EventArtwork` PRIMARY KEY (`EventID`, `ArtworkID`),
    CONSTRAINT `FK_EventArtwork_Artwork_ArtworkID` FOREIGN KEY (`ArtworkID`) REFERENCES `Artwork` (`ArtworkID`) ON DELETE CASCADE,
    CONSTRAINT `FK_EventArtwork_Event_EventID` FOREIGN KEY (`EventID`) REFERENCES `Event` (`EventID`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `Image` (
    `ImageID` int NOT NULL AUTO_INCREMENT,
    `ArtworkID` int NULL,
    `PerformanceID` int NULL,
    `FileName` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `FilePath` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `URL` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Image` PRIMARY KEY (`ImageID`),
    CONSTRAINT `FK_Image_Artwork_ArtworkID` FOREIGN KEY (`ArtworkID`) REFERENCES `Artwork` (`ArtworkID`),
    CONSTRAINT `FK_Image_Performance_PerformanceID` FOREIGN KEY (`PerformanceID`) REFERENCES `Performance` (`PerformanceID`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `ArtworkImage` (
    `ArtworkID` int NOT NULL,
    `ImageID` int NOT NULL,
    `FileName` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `FilePath` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    `URL` varchar(1000) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_ArtworkImage` PRIMARY KEY (`ArtworkID`, `ImageID`),
    CONSTRAINT `FK_ArtworkImage_Artwork_ArtworkID` FOREIGN KEY (`ArtworkID`) REFERENCES `Artwork` (`ArtworkID`) ON DELETE CASCADE,
    CONSTRAINT `FK_ArtworkImage_Image_ImageID` FOREIGN KEY (`ImageID`) REFERENCES `Image` (`ImageID`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE INDEX `IX_Artwork_CategoryID` ON `Artwork` (`CategoryID`);

CREATE INDEX `IX_ArtworkImage_ImageID` ON `ArtworkImage` (`ImageID`);

CREATE INDEX `IX_Event_PlaceID` ON `Event` (`PlaceID`);

CREATE INDEX `IX_EventArtwork_ArtworkID` ON `EventArtwork` (`ArtworkID`);

CREATE INDEX `IX_Image_ArtworkID` ON `Image` (`ArtworkID`);

CREATE INDEX `IX_Image_PerformanceID` ON `Image` (`PerformanceID`);

CREATE INDEX `IX_Performance_PlaceID` ON `Performance` (`PlaceID`);

CREATE INDEX `IX_Sentence_ArtworkID` ON `Sentence` (`ArtworkID`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240202141153_InitialMigration', '8.0.1');

COMMIT;

START TRANSACTION;

CREATE TABLE `AspNetRoles` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(256) CHARACTER SET utf8mb4 NULL,
    `NormalizedName` varchar(256) CHARACTER SET utf8mb4 NULL,
    `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_AspNetRoles` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `AspNetUsers` (
    `Id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ProfilePictureUrl` longtext CHARACTER SET utf8mb4 NULL,
    `DateOfBirth` datetime(6) NOT NULL,
    `UserName` varchar(256) CHARACTER SET utf8mb4 NULL,
    `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 NULL,
    `Email` varchar(256) CHARACTER SET utf8mb4 NULL,
    `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 NULL,
    `EmailConfirmed` tinyint(1) NOT NULL,
    `PasswordHash` longtext CHARACTER SET utf8mb4 NULL,
    `SecurityStamp` longtext CHARACTER SET utf8mb4 NULL,
    `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 NULL,
    `PhoneNumber` longtext CHARACTER SET utf8mb4 NULL,
    `PhoneNumberConfirmed` tinyint(1) NOT NULL,
    `TwoFactorEnabled` tinyint(1) NOT NULL,
    `LockoutEnd` datetime(6) NULL,
    `LockoutEnabled` tinyint(1) NOT NULL,
    `AccessFailedCount` int NOT NULL,
    CONSTRAINT `PK_AspNetUsers` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `RoleClaims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `RoleId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ClaimType` longtext CHARACTER SET utf8mb4 NULL,
    `ClaimValue` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_RoleClaims` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_RoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserClaims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ClaimType` longtext CHARACTER SET utf8mb4 NULL,
    `ClaimValue` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_UserClaims` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_UserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserLogins` (
    `LoginProvider` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ProviderKey` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ProviderDisplayName` longtext CHARACTER SET utf8mb4 NULL,
    `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_UserLogins` PRIMARY KEY (`LoginProvider`, `ProviderKey`),
    CONSTRAINT `FK_UserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserRoles` (
    `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `RoleId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_UserRoles` PRIMARY KEY (`UserId`, `RoleId`),
    CONSTRAINT `FK_UserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_UserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `UserTokens` (
    `UserId` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `LoginProvider` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Value` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_UserTokens` PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
    CONSTRAINT `FK_UserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE UNIQUE INDEX `RoleNameIndex` ON `AspNetRoles` (`NormalizedName`);

CREATE INDEX `EmailIndex` ON `AspNetUsers` (`NormalizedEmail`);

CREATE UNIQUE INDEX `UserNameIndex` ON `AspNetUsers` (`NormalizedUserName`);

CREATE INDEX `IX_RoleClaims_RoleId` ON `RoleClaims` (`RoleId`);

CREATE INDEX `IX_UserClaims_UserId` ON `UserClaims` (`UserId`);

CREATE INDEX `IX_UserLogins_UserId` ON `UserLogins` (`UserId`);

CREATE INDEX `IX_UserRoles_RoleId` ON `UserRoles` (`RoleId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240202190543_UpdateIdentitySchema', '8.0.1');

COMMIT;

START TRANSACTION;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240202213508_UpdateWithApplicationUser', '8.0.1');

COMMIT;

