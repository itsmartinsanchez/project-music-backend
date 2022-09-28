USE master;

DROP DATABASE IF EXISTS Music;

CREATE DATABASE Music;

USE Music;

CREATE TABLE Artists (
    Id INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
    Name VARCHAR(255) NOT NULL
);

CREATE TABLE Songs (
    Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    ArtistID INT NOT NULL FOREIGN KEY REFERENCES Artists(Id),
    Lyrics VARCHAR(MAX) NOT NULL,
    Album VARCHAR(255)
);

CREATE TABLE Users (
    Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(255) NOT NULL UNIQUE,
    EncryptedPassword VARCHAR(255) NOT NULL,
    LastLogin DATETIME2,
    Token NVARCHAR(MAX),
    Role VARCHAR(MAX),
    TokenExpiration DATETIME2
);

CREATE TABLE Comments (
    Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Content VARCHAR(MAX) NOT NULL,
    Rating INT NOT NULL,
    UserID INT NOT NULL FOREIGN KEY REFERENCES Users(Id),
    SongID INT NOT NULL FOREIGN KEY REFERENCES Songs(Id)
)