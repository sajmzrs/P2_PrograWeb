USE [master]
GO

CREATE DATABASE [Practica2]
GO

USE [Practica2]
GO

CREATE TABLE [dbo].[Clientes](
	[IdCliente] [bigint] IDENTITY(1,1) NOT NULL,
	[Cedula] [varchar](50) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Correo] [varchar](100) NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED
(
	[IdCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Mascotas](
	[IdMascota] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Especie] [varchar](100) NOT NULL,
	[Raza] [varchar](100) NOT NULL,
	[Peso] [decimal](8, 2) NOT NULL,
	[IdCliente] [bigint] NOT NULL,
 CONSTRAINT [PK_Mascotas] PRIMARY KEY CLUSTERED
(
	[IdMascota] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Mascotas]  WITH CHECK ADD  CONSTRAINT [FK_Mascotas_Clientes] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Clientes] ([IdCliente])
GO
ALTER TABLE [dbo].[Mascotas] CHECK CONSTRAINT [FK_Mascotas_Clientes]
GO

-- =============================================
-- Procedimientos almacenados
-- =============================================

-- Resultado: 1 = registrado, 0 = la cedula ya existe
CREATE PROCEDURE [dbo].[RegistrarCliente]
	@Cedula VARCHAR(50),
	@Nombre VARCHAR(100),
	@Correo VARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM dbo.Clientes WHERE Cedula = @Cedula)
	BEGIN
		SELECT 0 AS Resultado
		RETURN
	END

	INSERT INTO dbo.Clientes (Cedula, Nombre, Correo, Estado)
	VALUES (@Cedula, @Nombre, @Correo, 1)

	SELECT 1 AS Resultado
END
GO

CREATE PROCEDURE [dbo].[ConsultarClientes]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT IdCliente, Cedula, Nombre, Correo, Estado
	FROM dbo.Clientes
	WHERE Estado = 1
	ORDER BY Nombre
END
GO

-- Resultado: 1 = registrada, 0 = ya hay 2 mascotas de esa especie, -1 = el cliente no existe
CREATE PROCEDURE [dbo].[RegistrarMascota]
	@Nombre VARCHAR(100),
	@Especie VARCHAR(100),
	@Raza VARCHAR(100),
	@Peso DECIMAL(8, 2),
	@IdCliente BIGINT
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT 1 FROM dbo.Clientes WHERE IdCliente = @IdCliente)
	BEGIN
		SELECT -1 AS Resultado
		RETURN
	END

	IF (SELECT COUNT(*) FROM dbo.Mascotas WHERE Especie = @Especie) >= 2
	BEGIN
		SELECT 0 AS Resultado
		RETURN
	END

	INSERT INTO dbo.Mascotas (Nombre, Especie, Raza, Peso, IdCliente)
	VALUES (@Nombre, @Especie, @Raza, @Peso, @IdCliente)

	SELECT 1 AS Resultado
END
GO

CREATE PROCEDURE [dbo].[ConsultarMascotas]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	C.Cedula AS CedulaCliente,
			C.Nombre AS NombreCliente,
			M.Nombre AS NombreMascota,
			M.Especie,
			M.Peso
	FROM	dbo.Mascotas M
	INNER JOIN dbo.Clientes C ON M.IdCliente = C.IdCliente
	ORDER BY C.Nombre, M.Nombre
END
GO
