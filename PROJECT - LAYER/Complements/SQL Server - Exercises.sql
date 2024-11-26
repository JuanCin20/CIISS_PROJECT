--EJERCICIOS
USE DataBase_Inventory_Management;

----PROCEDIMIENTOS ALMACENADOS
GO
	CREATE
	OR ALTER PROCEDURE SP_USER AS BEGIN
SELECT
	TU.ID_Usuario,
	TTU.ID_Tipo_Usuario,
	TTU.Nombre_Tipo_Usuario,
	TU.Nombre_Usuario,
	TU.Apellido_Usuario,
	TU.E_Mail_Usuario,
	TU.Password_Usuario,
	TU.Reestablecer_Password_Usuario,
	TU.Estado_Usuario,
	CONVERT(
		VARCHAR(10),
		TU.Fecha_Registro_Usuario,
		103
	) AS [Fecha_Registro_Usuario],
	TU.Ruta_Imagen_Usuario,
	TU.Nombre_Imagen_Usuario
FROM
	Tabla_Usuario TU
	INNER JOIN Tabla_Tipo_Usuario TTU ON TU.ID_Tipo_Usuario = TTU.ID_Tipo_Usuario
WHERE
	TU.Estado_Usuario = 1;

END;

----EXECUTE SP_USER;
GO
	CREATE
	OR ALTER PROCEDURE SP_USER_LIST (@Estado_Usuario BIT) AS BEGIN
SELECT
	TU.ID_Usuario,
	TTU.ID_Tipo_Usuario,
	TTU.Nombre_Tipo_Usuario,
	TU.Nombre_Usuario,
	TU.Apellido_Usuario,
	TU.E_Mail_Usuario,
	TU.Password_Usuario,
	TU.Reestablecer_Password_Usuario,
	TU.Estado_Usuario,
	CONVERT(
		VARCHAR(10),
		TU.Fecha_Registro_Usuario,
		103
	) AS [Fecha_Registro_Usuario],
	TU.Ruta_Imagen_Usuario,
	TU.Nombre_Imagen_Usuario
FROM
	Tabla_Usuario TU
	INNER JOIN Tabla_Tipo_Usuario TTU ON TU.ID_Tipo_Usuario = TTU.ID_Tipo_Usuario
WHERE
	TU.Estado_Usuario = @Estado_Usuario
	AND TTU.ID_Tipo_Usuario = 2;

END;

----DECLARE @Estado_Usuario BIT = 1;
----EXECUTE SP_USER_LIST @Estado_Usuario;
GO
	CREATE
	OR ALTER PROCEDURE SP_USER_CREATE (
		@ID_Tipo_Usuario INT,
		@Nombre_Usuario VARCHAR (50),
		@Apellido_Usuario VARCHAR (50),
		@E_Mail_Usuario VARCHAR (30),
		@Password_Usuario VARCHAR (150),
		@Message VARCHAR (500) OUTPUT,
		@Result INT OUTPUT
	) AS BEGIN
SET
	@Result = 0 IF NOT EXISTS (
		SELECT
			*
		FROM
			Tabla_Usuario
		WHERE
			E_Mail_Usuario = @E_Mail_Usuario
	) BEGIN
INSERT INTO
	Tabla_Usuario (
		ID_Tipo_Usuario,
		Nombre_Usuario,
		Apellido_Usuario,
		E_Mail_Usuario,
		Password_Usuario
	)
VALUES
	(
		@ID_Tipo_Usuario,
		@Nombre_Usuario,
		@Apellido_Usuario,
		@E_Mail_Usuario,
		@Password_Usuario
	)
SET
	@Result = SCOPE_IDENTITY()
END;

ELSE
SET
	@Message = 'El Correo Electrónico del Usuario ya se Encuentra Registrado'
END;

GO
	CREATE
	OR ALTER PROCEDURE SP_USER_UPDATE (
		@ID_Usuario INT,
		@E_Mail_Usuario VARCHAR (30),
		@Message VARCHAR (500) OUTPUT,
		@Result INT OUTPUT
	) AS BEGIN
SET
	@Result = 0 IF NOT EXISTS (
		SELECT
			*
		FROM
			Tabla_Usuario
		WHERE
			E_Mail_Usuario = @E_Mail_Usuario
			AND ID_Usuario != @ID_Usuario
	) BEGIN
UPDATE
	TOP (1) Tabla_Usuario
SET
	E_Mail_Usuario = @E_Mail_Usuario
WHERE
	ID_Usuario = @ID_Usuario
SET
	@Result = 1
END;

ELSE
SET
	@Message = 'El Correo Electrónico del Usuario ya se Encuentra Registrado'
END;

GO
	CREATE
	OR ALTER PROCEDURE SP_USER_DELETE (
		@ID_Usuario INT,
		@Message VARCHAR (500) OUTPUT,
		@Result INT OUTPUT
	) AS BEGIN
SET
	@Result = 0 IF EXISTS (
		SELECT
			*
		FROM
			Tabla_Usuario
		WHERE
			ID_Usuario = @ID_Usuario
	) BEGIN
UPDATE
	TOP (1) Tabla_Usuario
SET
	Estado_Usuario = 0
WHERE
	ID_Usuario = @ID_Usuario
SET
	@Result = 1
END;

ELSE
SET
	@Message = 'El Usuario no se Encuentra Registrado'
END;

----UPDATE Tabla_Usuario SET Ruta_Imagen_Usuario = @Ruta_Imagen_Usuario, Nombre_Imagen_Usuario = @Nombre_Imagen_Usuario WHERE ID_Usuario = @ID_Usuario;
GO
	CREATE
	OR ALTER PROCEDURE SP_CATEGORY_LIST (@Estado_Categoria_Insumo BIT) AS BEGIN IF(@Estado_Categoria_Insumo = 1) BEGIN
SELECT
	TCI.ID_Categoria_Insumo,
	TCI.Nombre_Categoria_Insumo,
	CAST(TCI.Descripcion_Categoria_Insumo AS VARCHAR(255)) AS [Descripcion_Categoria_Insumo],
	TCI.Estado_Categoria_Insumo,
	CONVERT(
		VARCHAR(10),
		TCI.Fecha_Registro_Categoria_Insumo,
		103
	) AS [Fecha_Registro_Categoria_Insumo],
	COUNT(TI.ID_Categoria_Insumo) AS [Supply_Number]
FROM
	Tabla_Categoria_Insumo TCI
	INNER JOIN Tabla_Insumo TI ON TCI.ID_Categoria_Insumo = TI.ID_Categoria_Insumo
WHERE
	TCI.Estado_Categoria_Insumo = 1
	AND TI.Estado_Insumo = 1
GROUP BY
	TCI.ID_Categoria_Insumo,
	TCI.Nombre_Categoria_Insumo,
	CAST(TCI.Descripcion_Categoria_Insumo AS VARCHAR(255)),
	TCI.Estado_Categoria_Insumo,
	TCI.Fecha_Registro_Categoria_Insumo,
	TI.ID_Categoria_Insumo;

END;

ELSE BEGIN
SELECT
	ID_Categoria_Insumo,
	Nombre_Categoria_Insumo,
	CAST(Descripcion_Categoria_Insumo AS VARCHAR(255)) AS [Descripcion_Categoria_Insumo],
	Estado_Categoria_Insumo,
	CONVERT(
		VARCHAR(10),
		Fecha_Registro_Categoria_Insumo,
		103
	) AS [Fecha_Registro_Categoria_Insumo],
	'0' AS [Supply_Number]
FROM
	Tabla_Categoria_Insumo
WHERE
	Estado_Categoria_Insumo = 0
END;

END;

----DECLARE @Estado_Categoria_Insumo BIT = 1;
----EXECUTE SP_CATEGORY_LIST @Estado_Categoria_Insumo;
GO
	CREATE
	OR ALTER PROCEDURE SP_CATEGORY_CREATE (
		@Nombre_Categoria_Insumo VARCHAR (50),
		@Descripcion_Categoria_Insumo TEXT,
		@Message VARCHAR (500) OUTPUT,
		@Result INT OUTPUT
	) AS BEGIN
SET
	@Result = 0 IF NOT EXISTS (
		SELECT
			*
		FROM
			Tabla_Categoria_Insumo
		WHERE
			Nombre_Categoria_Insumo = @Nombre_Categoria_Insumo
	) BEGIN
INSERT INTO
	Tabla_Categoria_Insumo (
		Nombre_Categoria_Insumo,
		Descripcion_Categoria_Insumo
	)
VALUES
	(
		@Nombre_Categoria_Insumo,
		@Descripcion_Categoria_Insumo
	)
SET
	@Result = SCOPE_IDENTITY()
END;

ELSE
SET
	@Message = 'El Nombre de la Categoría del Insumo ya se Encuentra Registrado'
END;

GO
	CREATE
	OR ALTER PROCEDURE SP_CATEGORY_UPDATE (
		@ID_Categoria_Insumo INT,
		@Descripcion_Categoria_Insumo TEXT,
		@Message VARCHAR (500) OUTPUT,
		@Result INT OUTPUT
	) AS BEGIN
SET
	@Result = 0 IF EXISTS (
		SELECT
			*
		FROM
			Tabla_Categoria_Insumo
		WHERE
			ID_Categoria_Insumo = @ID_Categoria_Insumo
	) BEGIN
UPDATE
	TOP (1) Tabla_Categoria_Insumo
SET
	Descripcion_Categoria_Insumo = @Descripcion_Categoria_Insumo
WHERE
	ID_Categoria_Insumo = @ID_Categoria_Insumo
SET
	@Result = 1
END;

ELSE
SET
	@Message = 'La Categoría del Insumo no se Encuentra Registrada'
END;

GO
	CREATE
	OR ALTER PROCEDURE SP_SUPPLIER_LIST (@Estado_Proveedor_Insumo BIT) AS BEGIN IF(@Estado_Proveedor_Insumo = 1) BEGIN
SELECT
	TPI.ID_Proveedor_Insumo,
	TPI.Nombre_Proveedor_Insumo,
	TPI.Telefono_Proveedor_Insumo,
	TPI.E_Mail_Proveedor_Insumo,
	TPI.Direccion_Proveedor_Insumo,
	TPI.Estado_Proveedor_Insumo,
	CONVERT(
		VARCHAR(10),
		TPI.Fecha_Registro_Proveedor_Insumo,
		103
	) AS [Fecha_Registro_Proveedor_Insumo],
	COUNT(TI.ID_Proveedor_Insumo) AS [Supply_Number]
FROM
	Tabla_Proveedor_Insumo TPI
	INNER JOIN Tabla_Insumo TI ON TPI.ID_Proveedor_Insumo = TI.ID_Proveedor_Insumo
WHERE
	TPI.Estado_Proveedor_Insumo = 1
	AND TI.Estado_Insumo = 1
GROUP BY
	TPI.ID_Proveedor_Insumo,
	TPI.Nombre_Proveedor_Insumo,
	TPI.Telefono_Proveedor_Insumo,
	TPI.E_Mail_Proveedor_Insumo,
	TPI.Direccion_Proveedor_Insumo,
	TPI.Estado_Proveedor_Insumo,
	TPI.Fecha_Registro_Proveedor_Insumo,
	TI.ID_Proveedor_Insumo
END;

ELSE BEGIN
SELECT
	ID_Proveedor_Insumo,
	Nombre_Proveedor_Insumo,
	Telefono_Proveedor_Insumo,
	E_Mail_Proveedor_Insumo,
	Direccion_Proveedor_Insumo,
	Estado_Proveedor_Insumo,
	CONVERT(
		VARCHAR(10),
		Fecha_Registro_Proveedor_Insumo,
		103
	) AS [Fecha_Registro_Proveedor_Insumo],
	'0' AS [Supply_Number]
FROM
	Tabla_Proveedor_Insumo
WHERE
	Estado_Proveedor_Insumo = 0
END;

END;

----DECLARE @Estado_Proveedor_Insumo BIT = 1;
----EXECUTE SP_SUPPLIER_LIST @Estado_Proveedor_Insumo;
GO
	CREATE
	OR ALTER PROCEDURE SP_SUPPLIER_CREATE (
		@Nombre_Proveedor_Insumo VARCHAR (50),
		@Telefono_Proveedor_Insumo INT,
		@E_Mail_Proveedor_Insumo VARCHAR (30),
		@Direccion_Proveedor_Insumo VARCHAR (50),
		@Message VARCHAR (500) OUTPUT,
		@Result INT OUTPUT
	) AS BEGIN
SET
	@Result = 0 IF NOT EXISTS (
		SELECT
			*
		FROM
			Tabla_Proveedor_Insumo
		WHERE
			Nombre_Proveedor_Insumo = @Nombre_Proveedor_Insumo
	) BEGIN
INSERT INTO
	Tabla_Proveedor_Insumo (
		Nombre_Proveedor_Insumo,
		Telefono_Proveedor_Insumo,
		E_Mail_Proveedor_Insumo,
		Direccion_Proveedor_Insumo
	)
VALUES
	(
		@Nombre_Proveedor_Insumo,
		@Telefono_Proveedor_Insumo,
		@E_Mail_Proveedor_Insumo,
		@Direccion_Proveedor_Insumo
	)
SET
	@Result = SCOPE_IDENTITY()
END;

ELSE
SET
	@Message = 'El Nombre del Proveedor del Insumo ya se Encuentra Registrado'
END;

GO
	CREATE
	OR ALTER PROCEDURE SP_SUPPLIER_UPDATE (
		@ID_Proveedor_Insumo INT,
		@Telefono_Proveedor_Insumo INT,
		@E_Mail_Proveedor_Insumo VARCHAR (30),
		@Message VARCHAR (500) OUTPUT,
		@Result INT OUTPUT
	) AS BEGIN
SET
	@Result = 0 IF EXISTS (
		SELECT
			*
		FROM
			Tabla_Proveedor_Insumo
		WHERE
			ID_Proveedor_Insumo = @ID_Proveedor_Insumo
	) BEGIN
UPDATE
	TOP (1) Tabla_Proveedor_Insumo
SET
	Telefono_Proveedor_Insumo = @Telefono_Proveedor_Insumo,
	E_Mail_Proveedor_Insumo = @E_Mail_Proveedor_Insumo
WHERE
	ID_Proveedor_Insumo = @ID_Proveedor_Insumo
SET
	@Result = 1
END;

ELSE
SET
	@Message = 'El Proveedor del Insumo no se Encuentra Registrado'
END;

GO
	CREATE
	OR ALTER PROCEDURE SP_SUPPLY_LIST (@Estado_Insumo BIT) AS BEGIN
SELECT
	TI.ID_Insumo,
	TCI.ID_Categoria_Insumo,
	TCI.Nombre_Categoria_Insumo,
	TPI.ID_Proveedor_Insumo,
	TPI.Nombre_Proveedor_Insumo,
	TI.Nombre_Insumo,
	TI.Descripcion_Insumo,
	TI.Unidad_Medida_Insumo,
	TI.Precio_Insumo,
	TI.Stock_Insumo,
	TI.Estado_Insumo,
	CONVERT(
		VARCHAR(10),
		TI.Fecha_Registro_Insumo,
		103
	) AS [Fecha_Registro_Insumo],
	CONVERT(
		VARCHAR(10),
		TI.Fecha_Vencimiento_Insumo,
		103
	) AS [Fecha_Vencimiento_Insumo],
	TI.Ruta_Imagen_Insumo,
	TI.Nombre_Imagen_Insumo
FROM
	Tabla_Insumo TI
	INNER JOIN Tabla_Categoria_Insumo TCI ON TI.ID_Categoria_Insumo = TCI.ID_Categoria_Insumo
	INNER JOIN Tabla_Proveedor_Insumo TPI ON TI.ID_Proveedor_Insumo = TPI.ID_Proveedor_Insumo
WHERE
	TI.Estado_Insumo = @Estado_Insumo;

END;

----DECLARE @Estado_Insumo BIT = 1;
----EXECUTE SP_SUPPLY_LIST @Estado_Insumo;
GO
	CREATE
	OR ALTER PROCEDURE SP_SUPPLY_CREATE (
		@ID_Categoria_Insumo INT,
		@ID_Proveedor_Insumo INT,
		@Nombre_Insumo VARCHAR (110),
		@Descripcion_Insumo TEXT,
		@Unidad_Medida_Insumo VARCHAR (20),
		@Precio_Insumo DECIMAL (10, 2),
		@Stock_Insumo INT,
		@Fecha_Vencimiento_Insumo DATE,
		@Message VARCHAR (500) OUTPUT,
		@Result INT OUTPUT
	) AS BEGIN
SET
	@Result = 0 IF NOT EXISTS (
		SELECT
			*
		FROM
			Tabla_Insumo
		WHERE
			Nombre_Insumo = @Nombre_Insumo
	) BEGIN
INSERT INTO
	Tabla_Insumo (
		ID_Categoria_Insumo,
		ID_Proveedor_Insumo,
		Nombre_Insumo,
		Descripcion_Insumo,
		Unidad_Medida_Insumo,
		Precio_Insumo,
		Stock_Insumo,
		Fecha_Vencimiento_Insumo
	)
VALUES
	(
		@ID_Categoria_Insumo,
		@ID_Proveedor_Insumo,
		@Nombre_Insumo,
		@Descripcion_Insumo,
		@Unidad_Medida_Insumo,
		@Precio_Insumo,
		@Stock_Insumo,
		@Fecha_Vencimiento_Insumo
	)
SET
	@Result = SCOPE_IDENTITY()
END;

ELSE
SET
	@Message = 'El Nombre del Insumo ya se Encuentra Registrado'
END;

GO
	CREATE
	OR ALTER PROCEDURE SP_SUPPLY_UPDATE (
		@ID_Insumo INT,
		@Descripcion_Insumo TEXT,
		@Precio_Insumo DECIMAL (10, 2),
		@Message VARCHAR (500) OUTPUT,
		@Result INT OUTPUT
	) AS BEGIN
SET
	@Result = 0 IF EXISTS (
		SELECT
			*
		FROM
			Tabla_Insumo
		WHERE
			ID_Insumo != @ID_Insumo
	) BEGIN
UPDATE
	TOP (1) Tabla_Insumo
SET
	Descripcion_Insumo = @Descripcion_Insumo,
	Precio_Insumo = @Precio_Insumo
WHERE
	ID_Insumo = @ID_Insumo
SET
	@Result = 1
END;

ELSE
SET
	@Message = 'El Insumo no se Encuentra Registrado'
END;

GO
	CREATE
	OR ALTER PROCEDURE SP_SUPPLY_DELETE (
		@ID_Insumo INT,
		@Message VARCHAR (500) OUTPUT,
		@Result INT OUTPUT
	) AS BEGIN
SET
	@Result = 0 IF EXISTS (
		SELECT
			*
		FROM
			Tabla_Insumo
		WHERE
			Stock_Insumo <= 10
			AND ID_Insumo = @ID_Insumo
	) BEGIN
UPDATE
	Tabla_Insumo
SET
	Estado_Insumo = 0
WHERE
	ID_Insumo = @ID_Insumo
SET
	@Result = 1
END;

ELSE BEGIN IF EXISTS (
	SELECT
		*
	FROM
		Tabla_Insumo
	WHERE
		DATEDIFF(DAY, GETDATE(), Fecha_Vencimiento_Insumo) <= 7
		AND ID_Insumo = @ID_Insumo
) BEGIN
UPDATE
	TOP (1) Tabla_Insumo
SET
	Estado_Insumo = 0
WHERE
	ID_Insumo = @ID_Insumo
SET
	@Result = 1
END;

ELSE
SET
	@Message = 'El Insumo aún no se Acerca a su Fecha de Vencimiento'
END;

SET
	@Message = 'El Insumo aún Cuenta con Stock Válido'
END;

----UPDATE Tabla_Insumo SET Ruta_Imagen_Insumo = @Ruta_Imagen_Insumo, Nombre_Imagen_Insumo = @Nombre_Imagen_Insumo WHERE ID_Insumo = @ID_Insumo;
GO
	CREATE
	OR ALTER PROCEDURE SP_TIP_REPORT AS BEGIN
SELECT
	(
		SELECT
			COUNT(*)
		FROM
			Tabla_Movimiento_Inventario
	) AS [Tabla_Movimiento_Inventario],
	(
		SELECT
			COUNT(*)
		FROM
			Tabla_Categoria_Insumo
	) AS [Tabla_Categoria_Insumo],
	(
		SELECT
			COUNT(*)
		FROM
			Tabla_Proveedor_Insumo
	) AS [Tabla_Proveedor_Insumo],
	(
		SELECT
			COUNT(*)
		FROM
			Tabla_Insumo
	) AS [Tabla_Insumo]
END;

----EXECUTE SP_TIP_REPORT;
GO
	CREATE
	OR ALTER PROCEDURE SP_CHART_01 AS BEGIN
SET
	LANGUAGE SPANISH;

DECLARE @Min_Date DATETIME;

DECLARE @Max_Date DATETIME;

SET
	@Min_Date = (
		SELECT
			MIN(Fecha_Movimiento_Inventario)
		FROM
			Tabla_Movimiento_Inventario
	);

SET
	@Max_Date = (
		SELECT
			MAX(Fecha_Movimiento_Inventario)
		FROM
			Tabla_Movimiento_Inventario
	);

SELECT
	DATENAME(MONTH, Fecha_Movimiento_Inventario) AS [Income_Month_Name],
	COUNT(*) AS [Income_Number]
FROM
	Tabla_Movimiento_Inventario
WHERE
	Tipo_Movimiento_Inventario = 'Entrada'
	AND Fecha_Movimiento_Inventario BETWEEN @Min_Date
	AND @Max_Date
GROUP BY
	DATENAME(MONTH, Fecha_Movimiento_Inventario)
ORDER BY
	DATENAME(MONTH, Fecha_Movimiento_Inventario) DESC
END;

----EXECUTE SP_CHART_01;
GO
	CREATE
	OR ALTER PROCEDURE SP_CHART_02 AS BEGIN
SET
	LANGUAGE SPANISH;

DECLARE @Min_Date DATETIME;

DECLARE @Max_Date DATETIME;

SET
	@Min_Date = (
		SELECT
			MIN(Fecha_Movimiento_Inventario)
		FROM
			Tabla_Movimiento_Inventario
	);

SET
	@Max_Date = (
		SELECT
			MAX(Fecha_Movimiento_Inventario)
		FROM
			Tabla_Movimiento_Inventario
	);

SELECT
	DATENAME(MONTH, Fecha_Movimiento_Inventario) AS [Exit_Month_Name],
	COUNT(*) AS [Exit_Number]
FROM
	Tabla_Movimiento_Inventario
WHERE
	Tipo_Movimiento_Inventario = 'Salida'
	AND Fecha_Movimiento_Inventario BETWEEN @Min_Date
	AND @Max_Date
GROUP BY
	DATENAME(MONTH, Fecha_Movimiento_Inventario)
ORDER BY
	DATENAME(MONTH, Fecha_Movimiento_Inventario) DESC
END;

----EXECUTE SP_CHART_02;
GO
	CREATE
	OR ALTER PROCEDURE SP_CHART_03 AS BEGIN
SELECT
	TCI.Nombre_Categoria_Insumo,
	SUM(TI.Stock_Insumo) AS [Total_Stock]
FROM
	Tabla_Insumo TI
	INNER JOIN Tabla_Categoria_Insumo TCI ON TI.ID_Categoria_Insumo = TCI.ID_Categoria_Insumo
GROUP BY
	TCI.Nombre_Categoria_Insumo,
	TI.Stock_Insumo;

END;

----EXECUTE SP_CHART_03;
GO
	CREATE
	OR ALTER PROCEDURE SP_CHART_04 AS BEGIN
SELECT
	TPI.Nombre_Proveedor_Insumo AS [Nombre_Proveedor_01],
	SUM(TI.Stock_Insumo) AS [Stock_01]
FROM
	Tabla_Insumo TI
	INNER JOIN Tabla_Categoria_Insumo TCI ON TI.ID_Categoria_Insumo = TCI.ID_Categoria_Insumo
	INNER JOIN Tabla_Proveedor_Insumo TPI ON TI.ID_Proveedor_Insumo = TPI.ID_Proveedor_Insumo
WHERE
	TCI.Nombre_Categoria_Insumo = 'Aceites'
GROUP BY
	TPI.Nombre_Proveedor_Insumo,
	TI.Stock_Insumo
END;

----EXECUTE SP_CHART_04;
GO
	CREATE
	OR ALTER PROCEDURE SP_CHART_05 AS BEGIN
SELECT
	TPI.Nombre_Proveedor_Insumo AS [Nombre_Proveedor_02],
	SUM(TI.Stock_Insumo) AS [Stock_02]
FROM
	Tabla_Insumo TI
	INNER JOIN Tabla_Categoria_Insumo TCI ON TI.ID_Categoria_Insumo = TCI.ID_Categoria_Insumo
	INNER JOIN Tabla_Proveedor_Insumo TPI ON TI.ID_Proveedor_Insumo = TPI.ID_Proveedor_Insumo
WHERE
	TCI.Nombre_Categoria_Insumo = 'Bebidas'
GROUP BY
	TPI.Nombre_Proveedor_Insumo,
	TI.Stock_Insumo
END;

----EXECUTE SP_CHART_05;
GO
	CREATE
	OR ALTER PROCEDURE SP_CHART_06 AS BEGIN
SELECT
	TPI.Nombre_Proveedor_Insumo AS [Nombre_Proveedor_03],
	SUM(TI.Stock_Insumo) AS [Stock_03]
FROM
	Tabla_Insumo TI
	INNER JOIN Tabla_Categoria_Insumo TCI ON TI.ID_Categoria_Insumo = TCI.ID_Categoria_Insumo
	INNER JOIN Tabla_Proveedor_Insumo TPI ON TI.ID_Proveedor_Insumo = TPI.ID_Proveedor_Insumo
WHERE
	TCI.Nombre_Categoria_Insumo = 'Carnes'
GROUP BY
	TPI.Nombre_Proveedor_Insumo,
	TI.Stock_Insumo
END;

----EXECUTE SP_CHART_06;
GO
	CREATE
	OR ALTER PROCEDURE SP_DEADLINE_REPORT AS BEGIN
SELECT
	*
FROM
	(
		SELECT
			TIA.ID_Insumo,
			TCI.Nombre_Categoria_Insumo AS [Nombre_Categoria_Insumo_01],
			TPI.Nombre_Proveedor_Insumo AS [Nombre_Proveedor_Insumo_01],
			TIA.Nombre_Insumo AS [Nombre_Insumo_01],
			TIA.Descripcion_Insumo AS [Descripcion_Insumo_01],
			TIA.Unidad_Medida_Insumo,
			TIA.Precio_Insumo AS [Precio_Insumo_01],
			TIA.Stock_Insumo,
			TIA.Estado_Insumo,
			CONVERT(
				VARCHAR(10),
				TIA.Fecha_Vencimiento_Insumo,
				103
			) AS [Fecha_Vencimiento_Insumo],
			TIA.Ruta_Imagen_Insumo,
			TIA.Nombre_Imagen_Insumo,
			DATEDIFF(DAY, GETDATE(), TIA.Fecha_Vencimiento_Insumo) AS [Deadline]
		FROM
			Tabla_Insumo TIA
			INNER JOIN Tabla_Categoria_Insumo TCI ON TIA.ID_Categoria_Insumo = TCI.ID_Categoria_Insumo
			INNER JOIN Tabla_Proveedor_Insumo TPI ON TIA.ID_Proveedor_Insumo = TPI.ID_Proveedor_Insumo
		WHERE
			TIA.Estado_Insumo = 1
	) TI
WHERE
	[Deadline] <= 7;

END;

----EXECUTE SP_DEADLINE_REPORT;
GO
	CREATE
	OR ALTER PROCEDURE SP_DEADLINE_REPORT_DELETE (@ID_Insumo INT) AS BEGIN
UPDATE
	Tabla_Insumo
SET
	Estado_Insumo = 0
WHERE
	ID_Insumo = @ID_Insumo;

END;

----DECLARE @ID_Insumo INT = 1;
----EXECUTE SP_DEADLINE_REPORT_DELETE @ID_Insumo;
GO
	CREATE
	OR ALTER PROCEDURE SP_TRANSACTION_REPORT (
		@Initial_Fecha_Movimiento_Inventario VARCHAR(10),
		@Final_Fecha_Movimiento_Inventario VARCHAR(10),
		@ID_Movimiento_Inventario INT
	) AS BEGIN
SET
	DATEFORMAT DMY;

SELECT
	TMI.ID_Movimiento_Inventario,
	TMI.Tipo_Movimiento_Inventario,
	CONCAT (TU.Nombre_Usuario, ' ', TU.Apellido_Usuario) AS [Usuario_Transaction],
	TCI.Nombre_Categoria_Insumo AS [Nombre_Categoria_Insumo_02],
	TPI.Nombre_Proveedor_Insumo AS [Nombre_Proveedor_Insumo_02],
	TI.Nombre_Insumo AS [Nombre_Insumo_02],
	TI.Descripcion_Insumo AS [Descripcion_Insumo_02],
	TI.Precio_Insumo AS [Precio_Insumo_02],
	TDMI.Cantidad_Insumo_Detalle_Movimiento_Inventario AS [Cantidad_Movimiento_Inventario],
	TDMI.Monto_Total_Detalle_Movimiento_Inventario AS [Total_Transaction],
	CONVERT(
		VARCHAR(10),
		TMI.Fecha_Movimiento_Inventario,
		103
	) AS [Fecha_Movimiento_Inventario]
FROM
	Tabla_Detalle_Movimiento_Inventario TDMI
	INNER JOIN Tabla_Insumo TI ON TDMI.ID_Insumo = TI.ID_Insumo
	INNER JOIN Tabla_Categoria_Insumo TCI ON TI.ID_Categoria_Insumo = TCI.ID_Categoria_Insumo
	INNER JOIN Tabla_Proveedor_Insumo TPI ON TI.ID_Proveedor_Insumo = TPI.ID_Proveedor_Insumo
	INNER JOIN Tabla_Movimiento_Inventario TMI ON TDMI.ID_Movimiento_Inventario = TMI.ID_Movimiento_Inventario
	INNER JOIN Tabla_Usuario TU ON TMI.ID_Usuario = TU.ID_Usuario
WHERE
	CONVERT(DATE, TMI.Fecha_Movimiento_Inventario) BETWEEN @Initial_Fecha_Movimiento_Inventario
	AND @Final_Fecha_Movimiento_Inventario
	AND TMI.ID_Movimiento_Inventario = IIF(
		@ID_Movimiento_Inventario = 0,
		TMI.ID_Movimiento_Inventario,
		@ID_Movimiento_Inventario
	)
END;

----DECLARE @Initial_Fecha_Movimiento_Inventario VARCHAR(10) = '2024-11-25';
----DECLARE @Final_Fecha_Movimiento_Inventario VARCHAR(10) = '2024-11-25';
----DECLARE @ID_Movimiento_Inventario INT = 0;
----EXECUTE SP_TRANSACTION_REPORT @Initial_Fecha_Movimiento_Inventario, @Final_Fecha_Movimiento_Inventario, @ID_Movimiento_Inventario;
GO
	CREATE
	OR ALTER TRIGGER TRI_01 ON Tabla_Insumo FOR
INSERT
	AS BEGIN DECLARE @ID_Insumo INT DECLARE @ID_Categoria_Insumo INT DECLARE @ID_Proveedor_Insumo INT DECLARE @Stock_Insumo INT
SELECT
	@ID_Insumo = ID_Insumo,
	@ID_Categoria_Insumo = ID_Categoria_Insumo,
	@ID_Proveedor_Insumo = ID_Proveedor_Insumo
FROM
	INSERTED
SELECT
	@Stock_Insumo = Stock_Insumo
FROM
	Tabla_Insumo
WHERE
	ID_Insumo = @ID_Insumo IF (@Stock_Insumo > 10) BEGIN
UPDATE
	Tabla_Categoria_Insumo
SET
	Estado_Categoria_Insumo = 1
WHERE
	ID_Categoria_Insumo = @ID_Categoria_Insumo
UPDATE
	Tabla_Proveedor_Insumo
SET
	Estado_Proveedor_Insumo = 1
WHERE
	ID_Proveedor_Insumo = @ID_Proveedor_Insumo
END
ELSE BEGIN
UPDATE
	Tabla_Categoria_Insumo
SET
	Estado_Categoria_Insumo = 0
WHERE
	ID_Categoria_Insumo = @ID_Categoria_Insumo
UPDATE
	Tabla_Proveedor_Insumo
SET
	Estado_Proveedor_Insumo = 0
WHERE
	ID_Proveedor_Insumo = @ID_Proveedor_Insumo
END
END;

GO
	CREATE
	OR ALTER TRIGGER TRI_02 ON Tabla_Insumo FOR
UPDATE
	AS BEGIN DECLARE @ID_Insumo INT DECLARE @ID_Categoria_Insumo INT
SELECT
	@ID_Insumo = ID_Insumo
FROM
	INSERTED
SELECT
	@ID_Categoria_Insumo = ID_Categoria_Insumo
FROM
	Tabla_Insumo
WHERE
	ID_Insumo = @ID_Insumo IF EXISTS (
		SELECT
			*
		FROM
			Tabla_Insumo
		WHERE
			ID_Categoria_Insumo = @ID_Categoria_Insumo
			AND Estado_Insumo = 1
	) BEGIN
UPDATE
	Tabla_Categoria_Insumo
SET
	Estado_Categoria_Insumo = 1
WHERE
	ID_Categoria_Insumo = @ID_Categoria_Insumo
END
ELSE BEGIN
UPDATE
	Tabla_Categoria_Insumo
SET
	Estado_Categoria_Insumo = 0
WHERE
	ID_Categoria_Insumo = @ID_Categoria_Insumo
END
END;

GO
	CREATE
	OR ALTER TRIGGER TRI_03 ON Tabla_Insumo FOR
UPDATE
	AS BEGIN DECLARE @ID_Insumo INT DECLARE @ID_Proveedor_Insumo INT
SELECT
	@ID_Insumo = ID_Insumo
FROM
	INSERTED
SELECT
	@ID_Proveedor_Insumo = ID_Proveedor_Insumo
FROM
	Tabla_Insumo
WHERE
	ID_Insumo = @ID_Insumo IF EXISTS (
		SELECT
			*
		FROM
			Tabla_Insumo
		WHERE
			ID_Proveedor_Insumo = @ID_Proveedor_Insumo
			AND Estado_Insumo = 1
	) BEGIN
UPDATE
	Tabla_Proveedor_Insumo
SET
	Estado_Proveedor_Insumo = 1
WHERE
	ID_Proveedor_Insumo = @ID_Proveedor_Insumo
END
ELSE BEGIN
UPDATE
	Tabla_Proveedor_Insumo
SET
	Estado_Proveedor_Insumo = 0
WHERE
	ID_Proveedor_Insumo = @ID_Proveedor_Insumo
END
END;

/*INSERT INTO Tabla_Categoria_Insumo (Nombre_Categoria_Insumo, Descripcion_Categoria_Insumo) VALUES ('Juan Carlos Aronés Peña', 'Juan Carlos Aronés Peña.')*/
/*INSERT INTO Tabla_Proveedor_Insumo (Nombre_Proveedor_Insumo, Telefono_Proveedor_Insumo, E_Mail_Proveedor_Insumo, Direccion_Proveedor_Insumo)
 VALUES ('Juan Carlos Aronés Peña', '959748008', 'rasterfrack@gmail.com', 'Juan Carlos Aronés Peña.')*/
/*INSERT INTO Tabla_Insumo (ID_Categoria_Insumo, ID_Proveedor_Insumo, Nombre_Insumo, Descripcion_Insumo, Unidad_Medida_Insumo, Precio_Insumo, Stock_Insumo, Fecha_Vencimiento_Insumo, Ruta_Imagen_Insumo, Nombre_Imagen_Insumo)
 VALUES (13, 26, 'Juan Carlos Aronés Peña', 'Juan Carlos Aronés Peña', 'Un.', 500, 5, '2024-11-20', 'E:\JuanCin20\DATA\CIISS - INVENTORY MANAGEMENT\CIISS - INVENTORY MANAGEMENT\PROJECT - LAYER\wwwroot\Supply_Images', 'Image_Error.jpg')*/
/*UPDATE Tabla_Insumo SET Estado_Insumo = 1 WHERE ID_Insumo = 213*/
GO
	CREATE
	OR ALTER PROCEDURE SP_SUPPLIER_LIST_ALTERNATIVE (@ID_Categoria_Insumo INT) AS BEGIN
SELECT
	DISTINCT TPI.ID_Proveedor_Insumo,
	TPI.Nombre_Proveedor_Insumo
FROM
	Tabla_Insumo TI
	INNER JOIN Tabla_Categoria_Insumo TCI ON TI.ID_Categoria_Insumo = TCI.ID_Categoria_Insumo
	INNER JOIN Tabla_Proveedor_Insumo TPI ON TI.ID_Proveedor_Insumo = TPI.ID_Proveedor_Insumo
	AND TPI.Estado_Proveedor_Insumo = 1
WHERE
	TCI.ID_Categoria_Insumo = IIF(
		@ID_Categoria_Insumo = 0,
		TCI.ID_Categoria_Insumo,
		@ID_Categoria_Insumo
	);

END;

----DECLARE @ID_Categoria_Insumo INT = 1;
----EXECUTE SP_SUPPLIER_LIST_ALTERNATIVE @ID_Categoria_Insumo;
GO
	CREATE
	OR ALTER PROCEDURE SP_MIDDLE_LIST(
		@ID_Usuario INT,
		@ID_Insumo INT,
		@Result BIT OUTPUT
	) AS BEGIN IF EXISTS (
		SELECT
			*
		FROM
			Tabla_Middle
		WHERE
			ID_Usuario = @ID_Usuario
			AND ID_Insumo = @ID_Insumo
	)
SET
	@Result = 1
	ELSE
SET
	@Result = 0
END;

GO
	CREATE
	OR ALTER PROCEDURE SP_MIDDLE_CREATE_UPDATE(
		@ID_Usuario INT,
		@ID_Insumo INT,
		@Boolean_Operation BIT,
		@Message VARCHAR (500) OUTPUT,
		@Result BIT OUTPUT
	) AS BEGIN
SET
	@Result = 1
SET
	@Message = '' DECLARE @Exists_Middle BIT = IIF(
		EXISTS(
			SELECT
				*
			FROM
				Tabla_Middle
			WHERE
				ID_Usuario = @ID_Usuario
				AND ID_Insumo = @ID_Insumo
		),
		1,
		0
	) DECLARE @Stock_Insumo INT = (
		SELECT
			Stock_Insumo
		FROM
			Tabla_Insumo
		WHERE
			ID_Insumo = @ID_Insumo
	) BEGIN TRY BEGIN TRANSACTION OPERATION_01 IF(@Boolean_Operation = 1) BEGIN IF(@Stock_Insumo > 0) BEGIN IF(@Exists_Middle = 1)
UPDATE
	Tabla_Middle
SET
	Cantidad_Insumo_Middle = Cantidad_Insumo_Middle + 100
WHERE
	ID_Usuario = @ID_Usuario
	AND ID_Insumo = @ID_Insumo
	ELSE
INSERT INTO
	Tabla_Middle(ID_Usuario, ID_Insumo, Cantidad_Insumo_Middle)
VALUES
	(@ID_Usuario, @ID_Insumo, 100)
UPDATE
	Tabla_Insumo
SET
	Stock_Insumo = Stock_Insumo - 100
WHERE
	ID_Insumo = @ID_Insumo
END
ELSE BEGIN
SET
	@Result = 0
SET
	@Message = 'Stock Insuficiente para Satisfacer la Demanda'
END
END
ELSE BEGIN
UPDATE
	Tabla_Middle
SET
	Cantidad_Insumo_Middle = Cantidad_Insumo_Middle - 100
WHERE
	ID_Usuario = @ID_Usuario
	AND ID_Insumo = @ID_Insumo
UPDATE
	Tabla_Insumo
SET
	Stock_Insumo = Stock_Insumo + 100
WHERE
	ID_Insumo = @ID_Insumo
END COMMIT TRANSACTION OPERATION_01
END TRY BEGIN CATCH
SET
	@Result = 0
SET
	@Message = ERROR_MESSAGE() ROLLBACK TRANSACTION OPERATION_01
END CATCH
END;

GO
	CREATE
	OR ALTER PROCEDURE SP_MIDDLE_CREATE_UPDATE_ALTERNATIVE(
		@ID_Usuario INT,
		@ID_Insumo INT,
		@Boolean_Operation BIT,
		@Message VARCHAR (500) OUTPUT,
		@Result BIT OUTPUT
	) AS BEGIN
SET
	@Result = 1
SET
	@Message = '' DECLARE @Exists_Middle BIT = IIF(
		EXISTS(
			SELECT
				*
			FROM
				Tabla_Middle
			WHERE
				ID_Usuario = @ID_Usuario
				AND ID_Insumo = @ID_Insumo
		),
		1,
		0
	) BEGIN TRY BEGIN TRANSACTION OPERATION_02 IF(@Boolean_Operation = 1) BEGIN IF(@Exists_Middle = 1)
UPDATE
	Tabla_Middle
SET
	Cantidad_Insumo_Middle = Cantidad_Insumo_Middle + 100
WHERE
	ID_Usuario = @ID_Usuario
	AND ID_Insumo = @ID_Insumo
	ELSE
INSERT INTO
	Tabla_Middle(ID_Usuario, ID_Insumo, Cantidad_Insumo_Middle)
VALUES
	(@ID_Usuario, @ID_Insumo, 100)
UPDATE
	Tabla_Insumo
SET
	Stock_Insumo = Stock_Insumo + 100
WHERE
	ID_Insumo = @ID_Insumo
END
ELSE BEGIN
UPDATE
	Tabla_Middle
SET
	Cantidad_Insumo_Middle = Cantidad_Insumo_Middle - 100
WHERE
	ID_Usuario = @ID_Usuario
	AND ID_Insumo = @ID_Insumo
UPDATE
	Tabla_Insumo
SET
	Stock_Insumo = Stock_Insumo - 100
WHERE
	ID_Insumo = @ID_Insumo
END COMMIT TRANSACTION OPERATION_02
END TRY BEGIN CATCH
SET
	@Result = 0
SET
	@Message = ERROR_MESSAGE() ROLLBACK TRANSACTION OPERATION_02
END CATCH
END;

GO
	CREATE
	OR ALTER FUNCTION FN_MIDDLE_LIST(@ID_Usuario INT) RETURNS TABLE AS RETURN (
		SELECT
			TI.ID_Insumo,
			TPI.Nombre_Proveedor_Insumo,
			TI.Nombre_Insumo,
			TI.Precio_Insumo,
			TM.Cantidad_Insumo_Middle,
			TI.Ruta_Imagen_Insumo,
			TI.Nombre_Imagen_Insumo
		FROM
			Tabla_Middle TM
			INNER JOIN Tabla_Insumo TI ON TM.ID_Insumo = TI.ID_Insumo
			INNER JOIN Tabla_Proveedor_Insumo TPI ON TPI.ID_Proveedor_Insumo = TI.ID_Proveedor_Insumo
		WHERE
			TM.ID_Usuario = @ID_Usuario
	) ----SELECT * FROM FN_MIDDLE_LIST(53)
GO
	CREATE
	OR ALTER PROCEDURE SP_MIDDLE_DELETE(
		@ID_Usuario INT,
		@ID_Insumo INT,
		@Result BIT OUTPUT
	) AS BEGIN
SET
	@Result = 1 DECLARE @Cantidad_Insumo_Middle INT = (
		SELECT
			Cantidad_Insumo_Middle
		FROM
			Tabla_Middle
		WHERE
			ID_Usuario = @ID_Usuario
			AND ID_Insumo = @ID_Insumo
	) BEGIN TRY BEGIN TRANSACTION OPERATION_01
UPDATE
	Tabla_Insumo
SET
	Stock_Insumo = Stock_Insumo + @Cantidad_Insumo_Middle
WHERE
	ID_Insumo = @ID_Insumo DELETE TOP (1)
FROM
	Tabla_Middle
WHERE
	ID_Usuario = @ID_Usuario
	AND ID_Insumo = @ID_Insumo COMMIT TRANSACTION OPERATION_01
END TRY BEGIN CATCH
SET
	@Result = 0 ROLLBACK TRANSACTION OPERATION_01
END CATCH
END;

GO
	CREATE
	OR ALTER PROCEDURE SP_MIDDLE_DELETE_ALTERNATIVE(
		@ID_Usuario INT,
		@ID_Insumo INT,
		@Result BIT OUTPUT
	) AS BEGIN
SET
	@Result = 1 DECLARE @Cantidad_Insumo_Middle INT = (
		SELECT
			Cantidad_Insumo_Middle
		FROM
			Tabla_Middle
		WHERE
			ID_Usuario = @ID_Usuario
			AND ID_Insumo = @ID_Insumo
	) BEGIN TRY BEGIN TRANSACTION OPERATION_02
UPDATE
	Tabla_Insumo
SET
	Stock_Insumo = Stock_Insumo - @Cantidad_Insumo_Middle
WHERE
	ID_Insumo = @ID_Insumo DELETE TOP (1)
FROM
	Tabla_Middle
WHERE
	ID_Usuario = @ID_Usuario
	AND ID_Insumo = @ID_Insumo COMMIT TRANSACTION OPERATION_02
END TRY BEGIN CATCH
SET
	@Result = 0 ROLLBACK TRANSACTION OPERATION_02
END CATCH
END;

/**/
/**/
/**/
/**/
/**/
CREATE TYPE Tabla_Detalle_Movimiento_Inventario AS TABLE (
	ID_Insumo INT NULL,
	Cantidad_Insumo_Detalle_Movimiento_Inventario INT NULL,
	Monto_Total_Detalle_Movimiento_Inventario DECIMAL (10, 2) NULL
);

GO
	CREATE
	OR ALTER PROCEDURE SP_TRANSACTION_CREATE(
		@ID_Usuario INT,
		@Tipo_Movimiento_Inventario VARCHAR (10),
		@Cantidad_Insumo_Movimiento_Inventario INT,
		@Monto_Total_Movimiento_Inventario DECIMAL (10, 2),
		@Restaurante_Movimiento_Inventario VARCHAR (50),
		@Telefono_Movimiento_Inventario INT,
		@Direccion_Movimiento_Inventario VARCHAR (150),
		@ID_Distrito INT,
		@Tabla_Detalle_Movimiento_Inventario Tabla_Detalle_Movimiento_Inventario READONLY,
		@Message VARCHAR (500) OUTPUT,
		@Result BIT OUTPUT
	) AS BEGIN BEGIN TRY DECLARE @ID_Movimiento_Inventario INT = 0
SET
	@Message = ''
SET
	@Result = 1 BEGIN TRANSACTION REGISTER
INSERT INTO
	Tabla_Movimiento_Inventario (
		ID_Usuario,
		Tipo_Movimiento_Inventario,
		Cantidad_Insumo_Movimiento_Inventario,
		Monto_Total_Movimiento_Inventario,
		Restaurante_Movimiento_Inventario,
		Telefono_Movimiento_Inventario,
		Direccion_Movimiento_Inventario,
		ID_Distrito
	)
VALUES
	(
		@ID_Usuario,
		@Tipo_Movimiento_Inventario,
		@Cantidad_Insumo_Movimiento_Inventario,
		@Monto_Total_Movimiento_Inventario,
		@Restaurante_Movimiento_Inventario,
		@Telefono_Movimiento_Inventario,
		@Direccion_Movimiento_Inventario,
		@ID_Distrito
	)
SET
	@ID_Movimiento_Inventario = SCOPE_IDENTITY()
INSERT INTO
	Tabla_Detalle_Movimiento_Inventario(
		ID_Movimiento_Inventario,
		ID_Insumo,
		Cantidad_Insumo_Detalle_Movimiento_Inventario,
		Monto_Total_Detalle_Movimiento_Inventario
	)
SELECT
	@ID_Movimiento_Inventario,
	ID_Insumo,
	Cantidad_Insumo_Detalle_Movimiento_Inventario,
	Monto_Total_Detalle_Movimiento_Inventario
FROM
	@Tabla_Detalle_Movimiento_Inventario
DELETE FROM
	Tabla_Middle
WHERE
	ID_Usuario = @ID_Usuario COMMIT TRANSACTION REGISTER
END TRY BEGIN CATCH
SET
	@Message = ERROR_MESSAGE()
SET
	@Result = 0 ROLLBACK TRANSACTION REGISTER
END CATCH
END;