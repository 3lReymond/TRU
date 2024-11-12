create database Escuela





create database Proyecto_Escuela;

use Proyecto_Escuela

-- sp de selecciona todos los usurios
CREATE PROCEDURE Sp_Select1
AS
BEGIN
    SET NOCOUNT ON;

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





ALTER PROCEDURE sp_ListarMateriales
AS
BEGIN
    SELECT 
        ID_Materiales,
        Nombre,
        Stock,
        Estado
    FROM 
        Materiales;
END;


EXEC sp_help 'Materiales';





CREATE PROCEDURE sp_GuardarMaterial
    @Nombre NVARCHAR(100),
    @Stock INT,
    @Estado NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Materiales (Nombre, Stock, Estado)
    VALUES (@Nombre, @Stock, @Estado);
END;



DROP PROCEDURE IF EXISTS sp_EliminarMaterial;


CREATE PROCEDURE sp_EliminarMaterial
    @ID_Materiales INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Materiales
    WHERE ID_Materiales = @ID_Materiales;
END;




EXEC sp_helptext 'sp_ObtenerMateriales';

SELECT * FROM sys.objects WHERE type = 'P';



CREATE PROCEDURE sp_ObtenerMateriales
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM Materiales;  -- Asegúrate de que esto coincida con la estructura de tu tabla
END;


EXEC sp_ObtenerMateriales;













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





CREATE VIEW Vista AS
SELECT 
    R.ID_Usuario,
    R.Nombres,
    R.Apellidos,
    R.NomTutor,
    A.NombreActividad,
    A.Fecha,
    A.Conducta
FROM 
    Registros R
JOIN 
    Actividades A
ON 
    R.ID_Usuario = A.ID_Usuario;



	CREATE PROCEDURE VistaRegistros
    @ID_Usuario INT
AS
BEGIN
    SELECT 
        R.ID_Usuario,
        R.Nombres,
        R.Apellidos,
        R.NomTutor,
        A.NombreActividad,
        A.Fecha,
        A.Conducta
    FROM 
        Registros R
    INNER JOIN 
        Actividades A ON R.ID_Usuario = A.ID_Usuario
    WHERE 
        R.ID_Usuario = @ID_Usuario;
END;



exec VistaRegistros 5





CREATE PROCEDURE sp_ListarMateriales
AS
SELECT * FROM Materiales





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




SELECT TOP (1000) [ID_Usuario]
      ,[Nombres]
      ,[Apellidos]
      ,[NomTutor]
      ,[Telefono]
      ,[Contraseña]
  FROM [db_aac004_escuela].[dbo].[Registros]


--CREACION DE TABLAS DE MATERIALES 
CREATE TABLE Materiales(
ID_Materiales int identity (1,1) primary key,
Nombre nvarchar(50) not null,
Stock int not null,
Estado nvarchar(50)not null
);
SELECT * FROM Materiales
SELECT * FROM Registros;

ALTER TABLE Materiales
ADD ID_Usuario INT NOT NULL;

ALTER TABLE Materiales
ADD FOREIGN KEY (ID_Usuario) REFERENCES Registros(ID_Usuario);

CREATE PROCEDURE sp_EditarMateriales
    @ID_Materiales INT,
    @Nombre NVARCHAR(50),
    @Stock INT,
    @Estado NVARCHAR(50),
    @ID_Usuario INT
AS
BEGIN
    UPDATE Materiales
    SET 
        Nombre = @Nombre,
        Stock = @Stock,
        Estado = @Estado,
        ID_Usuario = @ID_Usuario
    WHERE 
        ID_Materiales = @ID_Materiales;
END;

CREATE PROCEDURE sp_EliminarMaterial
    @ID_Materiales INT
AS
BEGIN
    -- Verificar si el material existe antes de intentar eliminarlo
    IF EXISTS (SELECT 1 FROM Materiales WHERE ID_Materiales = @ID_Materiales)
    BEGIN
        -- Eliminar el material
        DELETE FROM Materiales WHERE ID_Materiales = @ID_Materiales;

        -- Mensaje de éxito
        SELECT 'Material eliminado exitosamente.' AS Mensaje;
    END
    ELSE
    BEGIN
        -- Mensaje de error si el material no existe
        SELECT 'Error: No se encontró el material con el ID proporcionado.' AS Mensaje;
    END
END;


CREATE PROCEDURE sp_GuardarMaterial
    @Nombre NVARCHAR(50),
    @Stock INT,
    @Estado NVARCHAR(50),
    @ID_Usuario INT
AS
BEGIN
    -- Insertar un nuevo material en la tabla Materiales
    INSERT INTO Materiales (Nombre, Stock, Estado, ID_Usuario)
    VALUES (@Nombre, @Stock, @Estado, @ID_Usuario);

    -- Devolver el ID del material recién insertado
    SELECT SCOPE_IDENTITY() AS NuevoID_Materiales;
END;



CREATE PROCEDURE sp_ObtenerMateriales
    @ID_Materiales INT = NULL,
    @Nombre NVARCHAR(50) = NULL,
    @Estado NVARCHAR(50) = NULL,
    @ID_Usuario INT = NULL
AS
BEGIN
    -- Seleccionar materiales basados en los parámetros proporcionados
    SELECT 
        ID_Materiales, 
        Nombre, 
        Stock, 
        Estado, 
        ID_Usuario
    FROM 
        Materiales
    WHERE
        (@ID_Materiales IS NULL OR ID_Materiales = @ID_Materiales) AND
        (@Nombre IS NULL OR Nombre LIKE '%' + @Nombre + '%') AND
        (@Estado IS NULL OR Estado LIKE '%' + @Estado + '%') AND
        (@ID_Usuario IS NULL OR ID_Usuario = @ID_Usuario);
END;

--Tabla intermedia
CREATE TABLE ActividadesMateriales (
    ID_Actividad INT NOT NULL,
    ID_Materiales INT NOT NULL,
    Cantidad INT NOT NULL, --cantidad de materiales usados en la actividad
    PRIMARY KEY (ID_Actividad, ID_Materiales),
    FOREIGN KEY (ID_Actividad) REFERENCES Actividades(ID_Actividad),
    FOREIGN KEY (ID_Materiales) REFERENCES Materiales(ID_Materiales)
	);


CREATE VIEW MatPorAct
AS
SELECT A.NombreActividad, M.Nombre, AM.Cantidad
FROM Actividades A
INNER JOIN ActividadesMateriales AM ON A.ID_Actividad = AM.ID_Actividad
INNER JOIN Materiales M ON AM.ID_Materiales = M.ID_Materiales;

CREATE PROCEDURE sp_MatPorAct
AS
SELECT * FROM MatPorAct

--Registro de usuario
CREATE TABLE USUARIO(
IdUsuario int primary key identity(1,1),
Correo varchar(100),
Clave varchar(500)
)

CREATE PROCEDURE sp_RegistrarUsuario(
@Correo varchar(100),
@Clave varchar(500),
@Registrado bit output,
@Mensaje varchar(100) output
)
as
begin
	if(not exists(select * from USUARIO where Correo = @Correo))
	begin
		insert into USUARIO(Correo,Clave) values(@Correo,@Clave)
		set @Registrado = 1
		set @Mensaje = 'usuario registrado'
	end
	else
	begin
		set @Registrado = 0
		set @Mensaje = 'correo ya existe'
	end
end



create proc sp_ValidarUsuario(
    @Correo varchar(100),
    @Clave varchar(500)
)
as
begin
    if(exists(select * from USUARIO where Correo = @Correo and Clave = @Clave))
        select IdUsuario from USUARIO where Correo = @Correo and Clave = @Clave
    else
        select '0'
end;



create proc sp_ValidarUsuario(
    @Correo varchar(100),
    @Clave varchar(500)
)
as
begin
    if(exists(select * from USUARIO where Correo = @Correo and Clave = @Clave))
        select IdUsuario from USUARIO where Correo = @Correo and Clave = @Clave
    else
        select '0'
end


declare @registrado bit, @mensaje varchar(100)

exec sp_RegistrarUsuario 'jose@gmail.com', 'ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae', @registrado output, @mensaje output

select @registrado
select @mensaje

SELECT * FROM USUARIO
exec sp_ValidarUsuario'jose123@gmail.com', 'ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae'



DROP TABLE Materiales;










-- Elimina el procedimiento si existe
IF OBJECT_ID('sp_ObtenerMateriales', 'P') IS NOT NULL
BEGIN
    DROP PROCEDURE sp_ObtenerMateriales; 
END
GO

-- Crea el procedimiento almacenado
CREATE PROCEDURE sp_ObtenerMateriales
    @ID_Materiales INT -- Define el parámetro
AS
BEGIN
    SET NOCOUNT ON;  
    SELECT * FROM Materiales WHERE ID_Materiales = @ID_Materiales;  -- Consulta el material por ID
END
GO









DROP PROCEDURE IF EXISTS sp_ObtenerMateriales;  -- Elimina el procedimiento si existe





CREATE PROCEDURE sp_ObtenerMateriales 
    @ID_Materiales INT 
AS 
BEGIN 
    SET NOCOUNT ON; 
    SELECT ID_Materiales, Nombre, Stock, Estado 
    FROM Materiales 
    WHERE ID_Materiales = @ID_Materiales;  
END;



CREATE PROCEDURE sp_ObtenerMateriales 
    @ID_Materiales INT 
AS 
BEGIN 
    SET NOCOUNT ON; 
    SELECT ID_Materiales, Nombre, Stock, Estado 
    FROM Materiales 
    WHERE ID_Materiales = @ID_Materiales;  
END;


DROP PROCEDURE IF EXISTS sp_ObtenerMateriales;


CREATE PROCEDURE sp_ObtenerMateriales 
    @ID_Materiales INT 
AS 
BEGIN 
    SET NOCOUNT ON; 
    SELECT ID_Materiales, Nombre, Stock, Estado 
    FROM Materiales 
    WHERE ID_Materiales = @ID_Materiales;  
END;


SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_ObtenerMateriales';
