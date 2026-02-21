-- CREATE DATABASE CropInsuranceDB;
-- GO

-- USE CropInsuranceDB;
-- GO

-- 1. Seasons Table
CREATE TABLE Seasons (
    SeasonId INT PRIMARY KEY IDENTITY(1,1),
    SeasonName VARCHAR(20) NOT NULL
);

-- 2. Crops Table
CREATE TABLE Crops (
    CropId INT PRIMARY KEY IDENTITY(1,1),
    CropName VARCHAR(50) NOT NULL,
    SeasonId INT NOT NULL,
    CONSTRAINT FK_Crop_Season FOREIGN KEY (SeasonId) REFERENCES Seasons(SeasonId)
);

-- 3. Insurance Application Table
CREATE TABLE InsuranceApplications (
    ApplicationId INT PRIMARY KEY IDENTITY(1,1),
    SeasonId INT NOT NULL,
    CropId INT NOT NULL,
    FarmerName VARCHAR(50) NOT NULL,        -- Max 50 characters 
    AadNo VARCHAR(12) NOT NULL,         -- Exactly 12 digits
    FatherName VARCHAR(12) NOT NULL,        -- Max 12 characters 
    CompleteAddress VARCHAR(250) NOT NULL,  -- Max 250 characters 
    FarmerCategory VARCHAR(20) NOT NULL,    -- Small/Medium/Large 
    SubmissionDate DATETIME DEFAULT GETDATE(), -- SQL Server uses GETDATE()
    
    -- Business Rule 6: Duplicate Aad for same crop NOT allowed 
    CONSTRAINT UC_Aad_Crop UNIQUE (AadNo, CropId),
    
    CONSTRAINT FK_App_Season FOREIGN KEY (SeasonId) REFERENCES Seasons(SeasonId),
    CONSTRAINT FK_App_Crop FOREIGN KEY (CropId) REFERENCES Crops(CropId)
);

-- Preload Seasons
INSERT INTO Seasons (SeasonName) VALUES ('Kharif'), ('Rabi');

-- Populate Crops based on Season
-- Assuming SeasonId 1 is Kharif and 2 is Rabi
INSERT INTO Crops (CropName, SeasonId) VALUES 
('Paddy', 1), 
('Maize', 1), 
('Wheat', 2), 
('Mustard', 2);