create table Especialidades(
	id_especialidad int Primary Key IDENTITY(1,1) NOT NULL,
	id_estado int NOT NULL,
	id_horario varchar(100) NOT NULL,
	nombre varchar(100) NULL,
	codigo varchar(100) NULL
);