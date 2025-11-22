CREATE DATABASE AmonetDb;
GO
USE AmonetDb;
GO

---------------------------------------------------
-- Tabla Clientes
---------------------------------------------------
CREATE TABLE dbo.Clientes
(
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    NombreCompleto NVARCHAR(150) NOT NULL,
    Correo NVARCHAR(150) NULL,
    Telefono NVARCHAR(50) NULL,
    FechaCreacion DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    FechaActualizacion DATETIME2 NULL
);

---------------------------------------------------
-- Tabla Artistas
---------------------------------------------------
CREATE TABLE dbo.Artistas
(
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    NombreArtistico NVARCHAR(150) NOT NULL,
    Estilos NVARCHAR(300) NULL,
    Activo BIT NOT NULL DEFAULT 1
);

---------------------------------------------------
-- Tabla Camillas
---------------------------------------------------
CREATE TABLE dbo.Camillas
(
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    Codigo NVARCHAR(50) NOT NULL UNIQUE,
    Activa BIT NOT NULL DEFAULT 1
);

---------------------------------------------------
-- Tabla Citas
---------------------------------------------------
CREATE TABLE dbo.Citas
(
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    ClienteId UNIQUEIDENTIFIER NOT NULL,
    ArtistaId UNIQUEIDENTIFIER NOT NULL,
    CamillaId UNIQUEIDENTIFIER NOT NULL,
    FechaInicio DATETIME2 NOT NULL,
    FechaFin DATETIME2 NOT NULL,
    Estado NVARCHAR(30) NOT NULL DEFAULT N'Creada',
    FechaCreacion DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),

    CONSTRAINT FK_Citas_Clientes FOREIGN KEY (ClienteId) REFERENCES dbo.Clientes(Id),
    CONSTRAINT FK_Citas_Artistas FOREIGN KEY (ArtistaId) REFERENCES dbo.Artistas(Id),
    CONSTRAINT FK_Citas_Camillas FOREIGN KEY (CamillaId) REFERENCES dbo.Camillas(Id)
);

---------------------------------------------------
-- Tabla Auditorias
---------------------------------------------------
CREATE TABLE dbo.Auditorias
(
    Id BIGINT IDENTITY PRIMARY KEY,
    Accion NVARCHAR(200) NOT NULL,
    Fecha DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    Datos NVARCHAR(MAX) NULL
);

---------------------------------------------------
-- Datos iniciales
---------------------------------------------------

INSERT INTO dbo.Artistas (NombreArtistico, Estilos)
VALUES (N'Luna', N'Linework, Realismo'),
       (N'Leo', N'Neotradicional, Color'),
       (N'Mara', N'Microrealismo, Fine Line');

INSERT INTO dbo.Camillas (Codigo)
VALUES (N'CAM-01'), (N'CAM-02'), (N'CAM-03');
