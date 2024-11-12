create database Escuela
use Escuela
create table Registros(
ID_Usuario int identity (1,1) primary key ,
Nombres varchar(50),
Apellidos varchar(50),
NomTutor varchar(50),
Telefono varchar(10),
Contraseña nvarchar(50)
);

CREATE TABLE Actividades (
    ID_Actividad INT IDENTITY(1,1) PRIMARY KEY,
    NombreActividad VARCHAR(100) NOT NULL,
    Fecha DATE NOT NULL,
    Conducta VARCHAR(50) NOT NULL,
    ID_Usuario INT NOT NULL,
    FOREIGN KEY (ID_Usuario) REFERENCES Registros(ID_Usuario)
);



CREATE PROCEDURE sp_EditarActividad
    @ID_Actividad INT,
    @NombreActividad VARCHAR(100),
    @Fecha DATE,
    @Conducta VARCHAR(50),
    @ID_Usuario INT
AS
BEGIN
    UPDATE Actividades
    SET 
        NombreActividad = @NombreActividad,
        Fecha = @Fecha,
        Conducta = @Conducta,
        ID_Usuario = @ID_Usuario
    WHERE 
        ID_Actividad = @ID_Actividad;
END;






CREATE PROCEDURE sp_BuscarActividad
    @ID_Actividad INT = NULL,
    @NombreActividad VARCHAR(100) = NULL,
    @Fecha DATE = NULL,
    @ID_Usuario INT = NULL
AS
BEGIN
    -- Seleccionar actividades basadas en los parámetros proporcionados
    SELECT 
        ID_Actividad, 
        NombreActividad, 
        Fecha, 
        Conducta, 
        ID_Usuario
    FROM 
        Actividades
    WHERE
        (@ID_Actividad IS NULL OR ID_Actividad = @ID_Actividad) AND
        (@NombreActividad IS NULL OR NombreActividad LIKE '%' + @NombreActividad + '%') AND
        (@Fecha IS NULL OR Fecha = @Fecha) AND
        (@ID_Usuario IS NULL OR ID_Usuario = @ID_Usuario);
END;






CREATE PROCEDURE sp_EliminarActividad
    @ID_Actividad INT
AS
BEGIN
    -- Verificar que la actividad exista antes de intentar eliminarla
    IF EXISTS (SELECT 1 FROM Actividades WHERE ID_Actividad = @ID_Actividad)
    BEGIN
        -- Eliminar la actividad
        DELETE FROM Actividades WHERE ID_Actividad = @ID_Actividad;

        -- Mensaje de éxito
        SELECT 'Actividad eliminada exitosamente.' AS Mensaje;
    END
    ELSE
    BEGIN
        -- Mensaje de error si la actividad no existe
        SELECT 'Error: No se encontró la actividad con el ID proporcionado.' AS Mensaje;
    END
END;









CREATE PROCEDURE sp_InsertarActividad
    @NombreActividad VARCHAR(100),
    @Fecha DATE,
    @Conducta VARCHAR(50),
    @ID_Usuario INT
AS
BEGIN
    -- Verificar que el ID de usuario exista en la tabla Registros
    IF EXISTS (SELECT 1 FROM Registros WHERE ID_Usuario = @ID_Usuario)
    BEGIN
        -- Insertar nueva actividad
        INSERT INTO Actividades (NombreActividad, Fecha, Conducta, ID_Usuario)
        VALUES (@NombreActividad, @Fecha, @Conducta, @ID_Usuario);

        -- Mensaje de éxito
        SELECT 'Actividad insertada exitosamente.' AS Mensaje;
    END
    ELSE
    BEGIN
        -- Mensaje de error si el ID de usuario no existe
        SELECT 'Error: El ID de usuario no existe.' AS Mensaje;
    END
END;


EXEC sp_InsertarActividad 'Tarea de Matemáticas', '2024-08-15', 'Positiva', 5;


CREATE PROCEDURE Sp_Actividades
AS
BEGIN
    SELECT * FROM Actividades;
END;



--Creacion de Sp_Select
CREATE PROCEDURE Sp_Select1
AS
BEGIN
    SELECT * FROM Registros;
END;


--Creacion de Sp_Incertar
CREATE PROCEDURE Sp_Incertar
    @Nombres VARCHAR(50),
    @Apellidos VARCHAR(50),
    @NomTutor VARCHAR(50),
    @Telefono VARCHAR(10),
    @Contraseña NVARCHAR(50)
AS
BEGIN
    INSERT INTO Registros (Nombres, Apellidos, NomTutor, Telefono, Contraseña)
    VALUES (@Nombres, @Apellidos, @NomTutor, @Telefono, @Contraseña);
    
    SELECT SCOPE_IDENTITY() AS ID_Usuario;
END;


--Creacion del Sp_Editar
CREATE PROCEDURE EditarUsuario
    @ID_Usuario INT,
    @Nombres VARCHAR(50),
    @Apellidos VARCHAR(50),
    @NomTutor VARCHAR(50),
    @Telefono VARCHAR(10),
    @Contraseña NVARCHAR(50)
AS
BEGIN
    UPDATE Registros
    SET 
        Nombres = @Nombres,
        Apellidos = @Apellidos,
        NomTutor = @NomTutor,
        Telefono = @Telefono,
        Contraseña = @Contraseña
    WHERE 
        ID_Usuario = @ID_Usuario;
END;




--Creacion de Sp_Eliminar
CREATE PROCEDURE EliminarUsuario
    @ID_Usuario INT
AS
BEGIN
    DELETE FROM Registros
    WHERE ID_Usuario = @ID_Usuario;
END;



--Creacion de Sp_Buscar
CREATE PROCEDURE sp_BuscarUsuario
    @ID_Usuario INT
AS
BEGIN
    SELECT 
        ID_Usuario,
        Nombres,
        Apellidos,
        NomTutor,
        Telefono,
        Contraseña
    FROM 
        Registros
    WHERE 
        ID_Usuario = @ID_Usuario;
END;

exec Sp_Select1

exec Sp_Actividades


CREATE PROCEDURE sp_ListarMateriales
AS
BEGIN
    SELECT * FROM Materiales;
END;