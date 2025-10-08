CREATE DATABASE TaskManager;
GO

USE TaskManager;
GO

CREATE TABLE Tasks (
    Id INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255),
    Description NVARCHAR(MAX),
    IsCompleted BIT,
    CreatedAt DATETIME DEFAULT GETDATE()
);
GO
