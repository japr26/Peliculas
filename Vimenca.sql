USE master
GO

CREATE DATABASE VIMENCA
 ON  PRIMARY(
	NAME = 'VIMENCA', FILENAME = 'D:\MSSQL15.MSSQLSERVER\MSSQL\DATA\VIMENCA.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB 
 )
 LOG ON(
	NAME = 'VIMENCA_log', FILENAME = 'D:\MSSQL15.MSSQLSERVER\MSSQL\DATA\VIMENCA_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB
 )
GO


CREATE TABLE Categoria(
	id int primary key,
	descripcion varchar(20)
);

CREATE TABLE Peliculas(
	idPelicula int primary key,
	titulo varchar(50) not null,
	ano int not null,
	idCategoria int not null,
	rutaImagen varchar(250)
);

ALTER TABLE Peliculas
ADD CONSTRAINT FK_Categoria
FOREIGN KEY (idCategoria) REFERENCES Categoria(id);