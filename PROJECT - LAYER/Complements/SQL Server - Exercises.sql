--EJERCICIOS
----CONSULTA EMPLEANDO MÁS DE UNA TABLA
/*SELECT
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
 WHERE TU.Estado_Usuario = 1;
 
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
 INNER JOIN Tabla_Proveedor_Insumo TPI ON TI.ID_Proveedor_Insumo = TPI.ID_Proveedor_Insumo;
 */
USE DataBase_Inventory_Management;

----PROCEDIMIENTOS ALMACENADOS
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
	OR ALTER PROCEDURE SP_CATEGORY_DELETE (
		@ID_Categoria_Insumo INT,
		@Message VARCHAR (500) OUTPUT,
		@Result INT OUTPUT
	) AS BEGIN
SET
	@Result = 0 IF NOT EXISTS (
		SELECT
			*
		FROM
			Tabla_Insumo TI
			INNER JOIN Tabla_Categoria_Insumo TCI ON TI.ID_Categoria_Insumo = TCI.ID_Categoria_Insumo
		WHERE
			TI.Estado_Insumo = 1
			AND TCI.ID_Categoria_Insumo = @ID_Categoria_Insumo
	) BEGIN
UPDATE
	TOP (1) Tabla_Categoria_Insumo
SET
	Estado_Categoria_Insumo = 0
WHERE
	ID_Categoria_Insumo = @ID_Categoria_Insumo
SET
	@Result = 1
END;

ELSE
SET
	@Message = 'El Nombre de la Categoría del Insumo se Encuentra Relacionado a un Insumo'
END;

GO
	CREATE
	OR ALTER PROCEDURE SP_SUPPLIER_CREATE (
		@Nombre_Proveedor_Insumo VARCHAR (50),
		@Telefono_Proveedor_Insumo INT,
		@E_Mail_Proveedor_Insumo VARCHAR (30),
		@Direccion_Proveedor_Insumo VARCHAR (50),
		@Estado_Proveedor_Insumo BIT,
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
		Direccion_Proveedor_Insumo,
		Estado_Proveedor_Insumo
	)
VALUES
	(
		@Nombre_Proveedor_Insumo,
		@Telefono_Proveedor_Insumo,
		@E_Mail_Proveedor_Insumo,
		@Direccion_Proveedor_Insumo,
		@Estado_Proveedor_Insumo
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
	OR ALTER PROCEDURE SP_SUPPLIER_DELETE (
		@ID_Proveedor_Insumo INT,
		@Message VARCHAR (500) OUTPUT,
		@Result INT OUTPUT
	) AS BEGIN
SET
	@Result = 0 IF NOT EXISTS (
		SELECT
			*
		FROM
			Tabla_Insumo TI
			INNER JOIN Tabla_Proveedor_Insumo TPI ON TI.ID_Proveedor_Insumo = TPI.ID_Proveedor_Insumo
		WHERE
			TI.ID_Proveedor_Insumo = @ID_Proveedor_Insumo
	) BEGIN
UPDATE
	TOP (1) Tabla_Proveedor_Insumo
SET
	Estado_Proveedor_Insumo = 0
WHERE
	ID_Proveedor_Insumo = @ID_Proveedor_Insumo
SET
	@Result = 1
END;

ELSE
SET
	@Message = 'El Nombre del Proveedor del Insumo se Encuentra Relacionado a un Insumo'
END;

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
		@Estado_Insumo BIT,
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
		Estado_Insumo,
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
		@Estado_Insumo,
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
		@Stock_Insumo INT,
		@Estado_Insumo BIT,
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
	Precio_Insumo = @Precio_Insumo,
	Stock_Insumo = @Stock_Insumo,
	Estado_Insumo = @Estado_Insumo
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
			Stock_Insumo < 10
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

ELSE BEGIN IF EXISTS (
	SELECT
		*
	FROM
		Tabla_Insumo
	WHERE
		DATEDIFF(DAY, GETDATE(), Fecha_Vencimiento_Insumo) <= 7
		AND ID_Insumo = 1
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
	*
FROM
	(
		SELECT
			TOP 6 Fecha_Movimiento_Inventario AS [Fecha_Movimiento_Inventario],
			YEAR(Fecha_Movimiento_Inventario) AS [Income_Year],
			MONTH(Fecha_Movimiento_Inventario) AS [Income_Month],
			DATENAME(MONTH, Fecha_Movimiento_Inventario) AS [Income_Month_Name],
			COUNT(*) AS [Income_Number]
		FROM
			Tabla_Movimiento_Inventario
		WHERE
			Tipo_Movimiento_Inventario = 'Entrada'
			AND Fecha_Movimiento_Inventario BETWEEN @Min_Date
			AND @Max_Date
		GROUP BY
			Fecha_Movimiento_Inventario
		ORDER BY
			Fecha_Movimiento_Inventario DESC
	) TMI
ORDER BY
	[Fecha_Movimiento_Inventario] ASC
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
	*
FROM
	(
		SELECT
			TOP 6 Fecha_Movimiento_Inventario AS [Fecha_Movimiento_Inventario],
			YEAR(Fecha_Movimiento_Inventario) AS [Exit_Year],
			MONTH(Fecha_Movimiento_Inventario) AS [Exit_Month],
			DATENAME(MONTH, Fecha_Movimiento_Inventario) AS [Exit_Month_Name],
			COUNT(*) AS [Exit_Number]
		FROM
			Tabla_Movimiento_Inventario
		WHERE
			Tipo_Movimiento_Inventario = 'Salida'
			AND Fecha_Movimiento_Inventario BETWEEN @Min_Date
			AND @Max_Date
		GROUP BY
			Fecha_Movimiento_Inventario
		ORDER BY
			Fecha_Movimiento_Inventario DESC
	) TMI
ORDER BY
	[Fecha_Movimiento_Inventario] ASC
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
	TCI.Nombre_Categoria_Insumo AS [Nombre_Categoria_Insumo_02],
	TPI.Nombre_Proveedor_Insumo AS [Nombre_Proveedor_Insumo_02],
	TI.Nombre_Insumo AS [Nombre_Insumo_02],
	TI.Descripcion_Insumo AS [Descripcion_Insumo_02],
	TI.Precio_Insumo AS [Precio_Insumo_02],
	TMI.Cantidad_Movimiento_Inventario,
	(
		TI.Precio_Insumo * TMI.Cantidad_Movimiento_Inventario
	) AS [Total_Transaction],
	CONVERT(
		VARCHAR(10),
		TMI.Fecha_Movimiento_Inventario,
		103
	) AS [Fecha_Movimiento_Inventario],
	CONCAT (TU.Nombre_Usuario, ' ', TU.Apellido_Usuario) [Usuario_Transaction]
FROM
	Tabla_Movimiento_Inventario TMI
	INNER JOIN Tabla_Insumo TI ON TMI.ID_Insumo = TI.ID_Insumo
	INNER JOIN Tabla_Categoria_Insumo TCI ON TCI.ID_Categoria_Insumo = TI.ID_Categoria_Insumo
	INNER JOIN Tabla_Proveedor_Insumo TPI ON TPI.ID_Proveedor_Insumo = TI.ID_Proveedor_Insumo
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

----DECLARE @Initial_Fecha_Movimiento_Inventario VARCHAR(10) = '2023-01-01';
----DECLARE @Final_Fecha_Movimiento_Inventario VARCHAR(10) = '2024-11-15';
----DECLARE @ID_Movimiento_Inventario INT = 0;
----EXECUTE SP_TRANSACTION_REPORT @Initial_Fecha_Movimiento_Inventario, @Final_Fecha_Movimiento_Inventario, @ID_Movimiento_Inventario;
GO
	CREATE
	OR ALTER PROCEDURE SP_QUERY (@ID_Categoria_Insumo INT) AS BEGIN
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
----EXECUTE SP_QUERY @ID_Categoria_Insumo;