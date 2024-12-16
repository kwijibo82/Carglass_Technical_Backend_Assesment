CREATE DATABASE ClientDb;
GO
USE ClientDb;
GO

CREATE TABLE Clients (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DocType NVARCHAR(10) NOT NULL,
    DocNum NVARCHAR(20) NOT NULL,
    Email NVARCHAR(100),
    GivenName NVARCHAR(50) NOT NULL,
    FamilyName1 NVARCHAR(50) NOT NULL,
    Phone NVARCHAR(15)
);
GO


INSERT INTO Clients (DocType, DocNum, Email, GivenName, FamilyName1, Phone)
VALUES 
('DNI', '12345678A', 'juan.perez@example.com', 'Juan', 'Pérez', '600123456'),
('NIE', 'X1234567B', 'maria.gomez@example.com', 'María', 'Gómez', '610987654'),
('DNI', '87654321C', 'carlos.lopez@example.com', 'Carlos', 'López', '620456789'),
('DNI', '34567890D', 'laura.martin@example.com', 'Laura', 'Martín', '630123987'),
('NIE', 'Y7654321E', 'david.garcia@example.com', 'David', 'García', '640456123'),
('DNI', '56789012F', 'ana.torres@example.com', 'Ana', 'Torres', '650789123'),
('DNI', '78901234G', 'pedro.rodriguez@example.com', 'Pedro', 'Rodríguez', '660123789'),
('NIE', 'Z8901234H', 'sofia.fernandez@example.com', 'Sofía', 'Fernández', '670456987'),
('DNI', '90123456I', 'pablo.ramos@example.com', 'Pablo', 'Ramos', '680789456'),
('NIE', 'W0123456J', 'lucia.molina@example.com', 'Lucía', 'Molina', '690123654');
GO
