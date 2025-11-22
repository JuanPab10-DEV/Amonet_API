-- Script de migración para agregar campo Cedula a la tabla Clientes
-- Ejecutar solo si la tabla ya existe y no tiene el campo Cedula

USE AmonetDb;
GO

-- Verificar si la columna ya existe usando sys.columns
IF NOT EXISTS (
    SELECT 1 
    FROM sys.columns 
    WHERE object_id = OBJECT_ID('dbo.Clientes') 
    AND name = 'Cedula'
)
BEGIN
    -- Agregar columna Cedula como NULL primero
    ALTER TABLE dbo.Clientes
    ADD Cedula NVARCHAR(20) NULL;
    
    -- Actualizar registros existentes con un valor temporal único
    DECLARE @contador INT = 1;
    DECLARE @sql NVARCHAR(MAX);
    
    -- Actualizar cada registro con un número secuencial
    DECLARE cur CURSOR FOR SELECT Id FROM dbo.Clientes WHERE Cedula IS NULL;
    DECLARE @id UNIQUEIDENTIFIER;
    
    OPEN cur;
    FETCH NEXT FROM cur INTO @id;
    
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @sql = N'UPDATE dbo.Clientes SET Cedula = ''TEMP'' + CAST(' + CAST(@contador AS NVARCHAR(10)) + ' AS NVARCHAR(10)) WHERE Id = @Id';
        EXEC sp_executesql @sql, N'@Id UNIQUEIDENTIFIER', @Id = @id;
        SET @contador = @contador + 1;
        FETCH NEXT FROM cur INTO @id;
    END
    
    CLOSE cur;
    DEALLOCATE cur;
    
    -- Hacer la columna NOT NULL
    ALTER TABLE dbo.Clientes
    ALTER COLUMN Cedula NVARCHAR(20) NOT NULL;
    
    -- Agregar índice único (eliminar si ya existe)
    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UQ_Clientes_Cedula' AND object_id = OBJECT_ID('dbo.Clientes'))
    BEGIN
        ALTER TABLE dbo.Clientes
        ADD CONSTRAINT UQ_Clientes_Cedula UNIQUE (Cedula);
    END
    
    PRINT 'Campo Cedula agregado exitosamente';
END
ELSE
BEGIN
    PRINT 'El campo Cedula ya existe';
END
GO

