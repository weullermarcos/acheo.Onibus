USE [master]
GO

IF NOT EXISTS (SELECT NAME FROM sys.databases WHERE name = 'dbAcheoOnibus')
      CREATE DATABASE dbAcheoOnibus
GO

USE dbAcheoOnibus
GO

IF OBJECT_ID('tblViagem') IS NOT NULL
	DROP TABLE tblViagem
GO

IF OBJECT_ID('tblOnibusItinerario') IS NOT NULL
	DROP TABLE tblOnibusItinerario
GO

IF OBJECT_ID('tblOnibus') IS NOT NULL
	DROP TABLE tblOnibus
GO

IF OBJECT_ID('tblItinerario') IS NOT NULL
	DROP TABLE tblItinerario
GO

IF OBJECT_ID('getItinerarios') IS NOT NULL
	DROP VIEW getItinerarios
GO

IF OBJECT_ID('getViagens') IS NOT NULL
	DROP VIEW getViagens
GO

IF OBJECT_ID('getOnibus') IS NOT NULL
	DROP VIEW getOnibus
GO

CREATE TABLE tblOnibus(
	idOnibus INT PRIMARY KEY IDENTITY(1,1),
	placa VARCHAR(8) NOT NULL UNIQUE,
	latitude VARCHAR(255) NOT NULL,
	longitude VARCHAR(255) NOT NULL,
	numeroOnibus INT NOT NULL
)
GO

CREATE TABLE tblItinerario(
	idItinerario INT PRIMARY KEY IDENTITY(1,1),
	tarifa MONEY NOT NULL,
	numero VARCHAR(50) NOT NULL
)
GO

CREATE TABLE tblViagem(
	idViagem INT PRIMARY KEY IDENTITY(1,1),
	sentidoViagem INT NOT NULL,
	origem VARCHAR(255) NOT NULL,
	destino VARCHAR(255) NOT NULL,
	idItinerarioFK INT NOT NULL FOREIGN KEY REFERENCES tblItinerario(idItinerario)
)
GO

CREATE TABLE tblOnibusItinerario(
	idOnibusItinerario INT PRIMARY KEY IDENTITY(1,1),
	data DATE NOT NULL,
	idOnibusFK INT NOT NULL FOREIGN KEY REFERENCES tblOnibus(idOnibus),
	idItinerarioFK INT NOT NULL FOREIGN KEY REFERENCES tblItinerario(idItinerario)
)
GO

CREATE VIEW getItinerarios as 
	select *
	from tblItinerario
GO

CREATE VIEW getViagens as
	select *
	from tblViagem
GO

CREATE VIEW getOnibus as
	select i.numero, v.origem, v.destino, v.sentidoViagem, o.latitude, o.longitude, o.placa, o.numeroOnibus, i.tarifa, oi.data
	from tblOnibusItinerario as oi
	inner join tblOnibus o
	on o.idOnibus = oi.idOnibusFK
	inner join tblItinerario i
	on i.idItinerario = oi.idItinerarioFK
	inner join tblViagem v
	on v.idItinerarioFK = oi.idItinerarioFK
GO

/*CREATE VIEW getItinerarios as 
	select *
	from tblItinerario i
	inner join tblViagem v 
	on i.idItinerario = v.idItinerarioFK
GO*/

/*CREATE VIEW getLastItinerario as
	select TOP 1 *
	from tblItinerario ORDER BY idItinerario DESC*/
