create table Usuarios(
	id_usuario int Primary Key IDENTITY(1,1) NOT NULL,
	id_persona int NOT NULL,
	nombre varchar(100) NULL,
	apellido varchar(100) NULL,
	documento int NOT NULL,
	email varchar(100) NULL,
	telefono varchar(100) NULL
);