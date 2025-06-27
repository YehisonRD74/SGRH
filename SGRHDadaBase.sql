CREATE DATABASE SGRH
GO

USE SGRH

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    PhoneNumber VARCHAR(20),
    Address VARCHAR(100) NOT NULL,
    Password VARCHAR(100) NOT NULL,
    Role VARCHAR(20) NOT NULL CHECK (Role IN ('Customer', 'Administrator', 'Employee')),

    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL
);

CREATE TABLE Floor (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FloorNumber INT NOT NULL
);
CREATE TABLE RoomCategories (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(50),
    Description VARCHAR(200),
    BaseRate DECIMAL(10,2)
);
CREATE TABLE Room (
    Id INT PRIMARY KEY IDENTITY(1,1),
    RoomNumber INT,
    Status VARCHAR(20) CHECK (Status IN ('Disponible ',' Ocupada ',' Mantenimiento')),
    CategoryId INT NOT NULL,
    FloorId INT NOT NULL,
    
    FOREIGN KEY (CategoryId) REFERENCES RoomCategories(Id),
    FOREIGN KEY (FloorId) REFERENCES Floor(Id)
);

CREATE TABLE Reservation (
    Id INT PRIMARY KEY IDENTITY(1,1),
    CheckInDate DATETIME NOT NULL,
    CheckOutDate DATETIME NOT NULL,
    Status VARCHAR(50) CHECK (Status IN ('Confirmed', 'Canceled', 'Pending')) NOT NULL,
    TotalAmount DECIMAL(10,2) NOT NULL,
    UserId INT NOT NULL,

    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE ReservationDetails (
    Id INT PRIMARY KEY IDENTITY(1,1),
    NightPrice DECIMAL(10,2),
    Subtotal DECIMAL(10,2),
    ReservationId INT NOT NULL,
    RoomId INT NOT NULL,

    FOREIGN KEY (ReservationId) REFERENCES Reservation(Id),
    FOREIGN KEY (RoomId) REFERENCES Room(Id)
);
CREATE TABLE Rate (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Season VARCHAR(50) NOT NULL,
    RatePrice DECIMAL(10,2),
    CategoryId INT NOT NULL,

    FOREIGN KEY (CategoryId) REFERENCES RoomCategories(Id)
);



