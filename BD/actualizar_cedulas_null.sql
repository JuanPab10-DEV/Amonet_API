USE AmonetDb;
GO

-- Primero, hacer la columna nullable temporalmente si no lo es
IF EXISTS (
    SELECT 1 
    FROM sys.columns 
    WHERE object_id = OBJECT_ID('dbo.Clientes') 
    AND name = 'Cedula'
    AND is_nullable = 0
)
BEGIN
    ALTER TABLE dbo.Clientes
    ALTER COLUMN Cedula NVARCHAR(20) NULL;
END
GO

-- Actualizar valores NULL con valores temporales únicos
DECLARE @contador INT = 1;
DECLARE @id UNIQUEIDENTIFIER;
DECLARE @cedula NVARCHAR(20);

DECLARE cur CURSOR FOR 
    SELECT Id FROM dbo.Clientes WHERE Cedula IS NULL;

OPEN cur;
FETCH NEXT FROM cur INTO @id;

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @cedula = 'TEMP' + CAST(@contador AS NVARCHAR(10));
    
    -- Verificar que no exista ya
    WHILE EXISTS (SELECT 1 FROM dbo.Clientes WHERE Cedula = @cedula)
    BEGIN
        SET @contador = @contador + 1;
        SET @cedula = 'TEMP' + CAST(@contador AS NVARCHAR(10));
    END
    
    UPDATE dbo.Clientes
    SET Cedula = @cedula
    WHERE Id = @id;
    
    SET @contador = @contador + 1;
    FETCH NEXT FROM cur INTO @id;
END

CLOSE cur;
DEALLOCATE cur;
GO

-- Ahora hacer la columna NOT NULL
ALTER TABLE dbo.Clientes
ALTER COLUMN Cedula NVARCHAR(20) NOT NULL;
GO

-- Agregar constraint único si no existe
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'UQ_Clientes_Cedula' AND object_id = OBJECT_ID('dbo.Clientes'))
BEGIN
    ALTER TABLE dbo.Clientes
    ADD CONSTRAINT UQ_Clientes_Cedula UNIQUE (Cedula);
END
GO

PRINT 'Cédulas actualizadas exitosamente';
GO

