USE [CondiminioDB]
GO
/****** Object:  Table [dbo].[FastVisits]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FastVisits](
	[ServiceName] [varchar](max) NOT NULL,
	[Category] [varchar](max) NOT NULL,
	[IDHabitation] [int] NOT NULL,
	[FastVisitDate] [date] NOT NULL,
	[IDProject] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FavortiteVisits]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FavortiteVisits](
	[DNI] [int] NOT NULL,
	[FirstName] [varchar](max) NOT NULL,
	[LastName] [varchar](max) NOT NULL,
	[Kinship] [varchar](max) NOT NULL,
	[VehicleMarc] [varchar](max) NOT NULL,
	[VehicleModel] [varchar](max) NOT NULL,
	[VehicleColor] [varchar](max) NOT NULL,
	[VehiclePlate] [varchar](max) NOT NULL,
	[IDHabitation] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Guards]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Guards](
	[IDGuard] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](255) NOT NULL,
	[Password] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IDGuard] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Habitation]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Habitation](
	[IDHabitation] [int] NOT NULL,
	[IDProject] [int] NOT NULL,
	[IDUser] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HabitationalProjects]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HabitationalProjects](
	[IdProject] [int] IDENTITY(1,1) NOT NULL,
	[Logo] [varchar](max) NOT NULL,
	[Code] [varchar](max) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Adress] [varchar](max) NOT NULL,
	[OfficeTelephone] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProject] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RootUsers]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RootUsers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[IdUser] [int] IDENTITY(1,1) NOT NULL,
	[DNI] [varchar](max) NOT NULL,
	[FirsName] [varchar](max) NOT NULL,
	[LastName] [varchar](max) NOT NULL,
	[Telephone1] [varchar](max) NOT NULL,
	[Telephone2] [varchar](max) NULL,
	[Email] [varchar](max) NOT NULL,
	[Picture] [varchar](max) NOT NULL,
	[Password] [varchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicles]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicles](
	[Plate] [varchar](50) NOT NULL,
	[Marc] [varchar](50) NOT NULL,
	[Model] [varchar](50) NOT NULL,
	[Color] [varchar](50) NOT NULL,
	[IDUser] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Visits]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Visits](
	[DNI] [varchar](max) NOT NULL,
	[FirstName] [varchar](max) NOT NULL,
	[LastName] [varchar](max) NOT NULL,
	[VehicleMarc] [varchar](max) NOT NULL,
	[VehicleModel] [varchar](max) NOT NULL,
	[VehicleColor] [varchar](max) NOT NULL,
	[VehiclePlate] [varchar](max) NOT NULL,
	[IDHabitation] [int] NULL,
	[VisitDate] [date] NULL,
	[VisitTime] [time](7) NULL,
	[IDProject] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Habitation]  WITH CHECK ADD  CONSTRAINT [FK_Habitation_HabitationalProjects] FOREIGN KEY([IDProject])
REFERENCES [dbo].[HabitationalProjects] ([IdProject])
GO
ALTER TABLE [dbo].[Habitation] CHECK CONSTRAINT [FK_Habitation_HabitationalProjects]
GO
ALTER TABLE [dbo].[Habitation]  WITH CHECK ADD  CONSTRAINT [FK_Habitation_users] FOREIGN KEY([IDUser])
REFERENCES [dbo].[Users] ([IdUser])
GO
ALTER TABLE [dbo].[Habitation] CHECK CONSTRAINT [FK_Habitation_users]
GO
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_Vehicles_Users] FOREIGN KEY([IDUser])
REFERENCES [dbo].[Users] ([IdUser])
GO
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_Users]
GO
/****** Object:  StoredProcedure [dbo].[GetHabitationDetailsForProject]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetHabitationDetailsForProject]
    @ProjectId INT
AS
BEGIN
    -- Seleccionar datos de la tabla Habitation con información de las tablas relacionadas
    SELECT 
        H.IDHabitation,
        U.DNI,
        U.FirsName,
        U.LastName
    FROM 
        [dbo].[Habitation] AS H
    JOIN 
        [dbo].[HabitationalProjects] AS HP ON H.[IDProject] = HP.[IdProject]
    JOIN 
        [dbo].[Users] AS U ON H.[IDUser] = U.[IdUser]
    WHERE 
        HP.[IdProject] = @ProjectId;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetVehicleByUserId]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetVehicleByUserId]
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Vehicles
    WHERE IDUser = @UserId;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetVisitsByProjectID]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetVisitsByProjectID]
    @ProjectID INT
AS
BEGIN
    SELECT
        V.DNI,
        V.FirstName,
        V.LastName,
        H.IDHabitation,
        V.VehicleMarc,
        V.VehiclePlate,
        V.VehicleModel,
        V.VehicleColor,
        CAST(V.VisitDate AS DATE) AS VisitDate,
        V.VisitTime
    FROM
        Habitation H
    JOIN
        Visits V ON H.IDHabitation = V.IDHabitation AND V.IDProject = @ProjectID
    WHERE
        H.IDProject = @ProjectID
    ORDER BY
        H.IDHabitation, V.VisitDate, V.VisitTime;
END;
GO
/****** Object:  StoredProcedure [dbo].[spGetFastVisit]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spGetFastVisit]
    @IDHabitation int,
	@IDProject int
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM FastVisits
    WHERE IDHabitation = @IDHabitation and IDProject = @IDProject
END;
GO
/****** Object:  StoredProcedure [dbo].[spGetGuards]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spGetGuards] 


AS
BEGIN

	SELECT	
		IDGuard,
		UserName,
		Password
	FROM Guards
END
GO
/****** Object:  StoredProcedure [dbo].[spGetHabitationalProjectByID]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetHabitationalProjectByID] 

	@IdProject int

AS
BEGIN

	SELECT	
		IdProject,
		Logo,
		Code,
		Name,
		Adress,
		OfficeTelephone
	FROM HabitationalProjects where IdProject = @IdProject 	
END
GO
/****** Object:  StoredProcedure [dbo].[spGetHabitationalProjects]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spGetHabitationalProjects] 


AS
BEGIN

	SELECT	
		IdProject,
		Logo,
		Code,
		Name,
		Adress,
		OfficeTelephone
	FROM HabitationalProjects	
END
GO
/****** Object:  StoredProcedure [dbo].[spGetRootUsers]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetRootUsers] 


AS
BEGIN

	SELECT	
		ID,
		UserName,
		Password
	FROM RootUsers
END
GO
/****** Object:  StoredProcedure [dbo].[spGetUsers]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetUsers] 
AS
BEGIN
    SELECT
        U.IdUser,
        U.DNI,
        U.FirsName,
        U.LastName,
        U.Telephone1,
        U.Telephone2,
        U.Email,
        U.Picture,
        U.Password,
        ISNULL(H.IDHabitation, 0) AS IDHabitation,
        HP.Name AS ProjectName
    FROM Users U
    LEFT JOIN Habitation H ON U.IdUser = H.IDUser
    LEFT JOIN HabitationalProjects HP ON H.IDProject = HP.IdProject
END

GO
/****** Object:  StoredProcedure [dbo].[spGetUsersByEmail]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spGetUsersByEmail] 
    @Email NVARCHAR(255)
AS
BEGIN
    -- Comprobar si existe un usuario con el correo electrónico proporcionado
    IF EXISTS (SELECT 1 FROM Users WHERE Email = @Email)
    BEGIN
        -- Usuario encontrado, devolver detalles del usuario
        SELECT
            U.IdUser,
            U.DNI,
            U.FirsName,
            U.LastName,
            U.Telephone1,
            U.Telephone2,
            U.Email,
            U.Picture,
            U.Password,
            ISNULL(H.IDHabitation, 0) AS IDHabitation,
            HP.IdProject, -- Agregado: Devuelve el IdProject
            HP.Name AS ProjectName
        FROM Users U
        LEFT JOIN Habitation H ON U.IdUser = H.IDUser
        LEFT JOIN HabitationalProjects HP ON H.IDProject = HP.IdProject
        WHERE U.Email = @Email;
    END
    ELSE
    BEGIN
        -- Usuario no encontrado, devolver un resultado de 1
        SELECT 1 AS Result;
    END
END
GO
/****** Object:  StoredProcedure [dbo].[spGetVisitDetail]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetVisitDetail]
    @IDHabitation int,
	@IDProject int
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Visits
    WHERE IDHabitation = @IDHabitation and IDProject = @IDProject
END;
GO
/****** Object:  StoredProcedure [dbo].[spInsertFastVisit]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spInsertFastVisit]
    @ServiceName VARCHAR(MAX),
    @Category VARCHAR(MAX),
    @FastVisitDate VARCHAR(MAX),
    @IDHabitation INT,
    @IDProject INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Inicializa el resultado en 0 (datos duplicados) por defecto.
		DECLARE @Result INT = 0
	
       
        INSERT INTO FastVisits([ServiceName], [Category], [FastVisitDate], [IDHabitation], [IDProject])
        VALUES (@ServiceName, @Category, @FastVisitDate, @IDHabitation, @IDProject)

        
        SET @Result = 2
   
    SELECT @Result AS Result
END
GO
/****** Object:  StoredProcedure [dbo].[spInsertVehicle]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spInsertVehicle]
    @Plate VARCHAR(MAX),
    @Marc VARCHAR(MAX),
    @Model VARCHAR(MAX),
    @Color VARCHAR(MAX),
    @IdUser INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Inicializa el resultado en 0 (datos duplicados) por defecto.
    DECLARE @Result INT = 0

    -- Verifica si existe algún dato igual en la tabla Vehicles
    IF EXISTS (
        SELECT 1
        FROM Vehicles
        WHERE
            [Plate] = @Plate 
    )
    BEGIN
        -- Datos duplicados
        SET @Result = 1
    END
    ELSE
    BEGIN
        -- Si no hay datos duplicados, inserta un nuevo elemento en la tabla
        INSERT INTO Vehicles([Plate], [Marc], [Model], [Color], [IDUser])
        VALUES (@Plate, @Marc, @Model, @Color, @IdUser)

        -- Establece el resultado en 2 para indicar la inserción exitosa
        SET @Result = 2
    END

    -- Devuelve el resultado
    SELECT @Result AS Result
END
GO
/****** Object:  StoredProcedure [dbo].[spInsertVisit]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spInsertVisit]
    @DNI VARCHAR(MAX),
    @FirstName VARCHAR(MAX),
    @LastName VARCHAR(MAX),
    @Marc VARCHAR(MAX),
    @Model VARCHAR(MAX),
    @Color VARCHAR(MAX),
    @Plate VARCHAR(MAX),
    @Date DATE,
    @Hour TIME,
    @IDHabitation INT,
    @IDProject INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Inicializa el resultado en 0 (datos duplicados) por defecto.
    DECLARE @Result INT = 0

    -- Verifica si existe algún dato igual en la tabla Visits
    IF EXISTS (
        SELECT 1
        FROM Visits
        WHERE
            [DNI] = @DNI AND
            [VisitDate] = @Date AND
            [VisitTime] = @Hour
    )
    BEGIN
        -- Datos duplicados
        SET @Result = 1
    END
    ELSE
    BEGIN
        -- Si no hay datos duplicados, inserta un nuevo elemento en la tabla
        INSERT INTO Visits([DNI], [FirstName], [LastName], [VehicleMarc], [VehicleModel], [VehicleColor], [VehiclePlate], [VisitDate], [VisitTime], [IDHabitation], [IDProject])
        VALUES (@DNI, @FirstName, @LastName, @Marc, @Model, @Color, @Plate, @Date, @Hour, @IDHabitation, @IDProject)

        -- Establece el resultado en 2 para indicar la inserción exitosa
        SET @Result = 2
    END

    -- Devuelve el resultado
    SELECT @Result AS Result
END
GO
/****** Object:  StoredProcedure [dbo].[VerifyAndAssignHouse]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[VerifyAndAssignHouse]
    @IDHabitation int,
    @IDProject int,
    @IDUser int
AS
BEGIN
    SET NOCOUNT ON;

    -- Inicializa el resultado en 0 (datos duplicados) por defecto.
    DECLARE @Result INT = 0

    -- Verifica si el usuario ya tiene una habitación en cualquier proyecto
    IF EXISTS (
        SELECT 1
        FROM Habitation
        WHERE [IDUser] = @IDUser
    )
    BEGIN
        -- Usuario ya tiene una habitación
        SET @Result = 1
    END
    ELSE
    BEGIN
        -- Verifica si existe alguna habitación con los mismos datos en la tabla Habitation
        IF EXISTS (
            SELECT 1
            FROM Habitation
            WHERE
                [IDHabitation] = @IDHabitation
                AND [IDProject] = @IDProject 
                AND [IDUser] = @IDUser
        )
        BEGIN
            -- Datos duplicados
            SET @Result = 2
        END
        ELSE
        BEGIN
            -- Si no hay datos duplicados, inserta un nuevo elemento en la tabla
            INSERT INTO Habitation ([IDHabitation], [IDProject], [IDUser])
            VALUES (@IDHabitation, @IDProject, @IDUser)

            -- Establece el resultado en 3 para indicar la inserción exitosa
            SET @Result = 3
        END
    END

    -- Devuelve el resultado
    SELECT @Result AS Result
END
GO
/****** Object:  StoredProcedure [dbo].[VerifyAndCreateGuard]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[VerifyAndCreateGuard]
    @UserName VARCHAR(MAX),
    @Password VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    -- Inicializa el resultado en 0 (datos duplicados) por defecto.
    DECLARE @Result INT = 0

    -- Verifica si existe algún dato igual en la tabla HabitationalProjects
    IF EXISTS (
        SELECT 1
        FROM Guards
        WHERE
            [UserName] = @UserName
            OR [Password] = @Password
    )
    BEGIN
        -- Datos duplicados
        SET @Result = 1
    END
    ELSE
    BEGIN
        -- Si no hay datos duplicados, inserta un nuevo elemento en la tabla
        INSERT INTO Guards([UserName], [Password])
        VALUES (@UserName, @Password)

        -- Establece el resultado en 2 para indicar la inserción exitosa
        SET @Result = 2
    END

    -- Devuelve el resultado
    SELECT @Result AS Result
END
GO
/****** Object:  StoredProcedure [dbo].[VerifyAndCreateRoot]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[VerifyAndCreateRoot]
    @UserName VARCHAR(MAX),
    @Password VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    -- Inicializa el resultado en 0 (datos duplicados) por defecto.
    DECLARE @Result INT = 0

    -- Verifica si existe algún dato igual en la tabla HabitationalProjects
    IF EXISTS (
        SELECT 1
        FROM RootUsers
        WHERE
            [UserName] = @UserName
            OR [Password] = @Password
    )
    BEGIN
        -- Datos duplicados
        SET @Result = 1
    END
    ELSE
    BEGIN
        -- Si no hay datos duplicados, inserta un nuevo elemento en la tabla
        INSERT INTO RootUsers([UserName], [Password])
        VALUES (@UserName, @Password)

        -- Establece el resultado en 2 para indicar la inserción exitosa
        SET @Result = 2
    END

    -- Devuelve el resultado
    SELECT @Result AS Result
END
GO
/****** Object:  StoredProcedure [dbo].[VerifyAndCreateUser]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[VerifyAndCreateUser]
    @DNI VARCHAR(MAX),
    @FirsName VARCHAR(MAX),
    @LastName VARCHAR(MAX),
    @Telephone1 VARCHAR(MAX),
    @Telephone2 VARCHAR(MAX),
    @Email VARCHAR(MAX),
    @Picture VARCHAR(MAX),
    @Password VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    -- Inicializa el resultado en 0 (datos duplicados) por defecto.
    DECLARE @Result INT = 0

    -- Verifica si existe algún dato igual en la tabla Users
    IF EXISTS (
        SELECT 1
        FROM Users
        WHERE
            [DNI] = @DNI OR
            [FirsName] = @FirsName OR
            [LastName] = @LastName OR
            [Telephone1] = @Telephone1 OR
            [Telephone2] = @Telephone2 OR
            [Email] = @Email OR
            [Picture] = @Picture OR
            [Password] = @Password
    )
    BEGIN
        -- Datos duplicados
        SET @Result = 1
    END
    ELSE
    BEGIN
        -- Si no hay datos duplicados, inserta un nuevo elemento en la tabla
        INSERT INTO Users([DNI], [FirsName], [LastName], [Telephone1], [Telephone2], [Email], [Picture], [Password])
        VALUES (@DNI, @FirsName, @LastName, @Telephone1, @Telephone2, @Email, @Picture, @Password)

        -- Establece el resultado en 2 para indicar la inserción exitosa
        SET @Result = 2
    END

    -- Devuelve el resultado
    SELECT @Result AS Result
END
GO
/****** Object:  StoredProcedure [dbo].[VerifyAndDeleteGuard]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[VerifyAndDeleteGuard]
    @IDGuard VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    -- Inicializa el resultado en 0 (eliminación fallida) por defecto.
    DECLARE @Result INT = 0

    -- Verifica si existe algún dato igual en la tabla HabitationalProjects
    IF EXISTS (
        SELECT 1
        FROM Guards
        WHERE [IDGuard] = @IDGuard
    )
    BEGIN
        -- Si encuentra un registro con el mismo código, elimina la fila
        DELETE FROM Guards
        WHERE [IDGuard] = @IDGuard

        -- Establece el resultado en 2 para indicar la eliminación exitosa
        SET @Result = 2
    END
    ELSE
    BEGIN
        -- Si no hay datos duplicados, establece el resultado en 1 (eliminación no realizada)
        SET @Result = 1
    END

    -- Devuelve el resultado
    SELECT @Result AS Result
END
GO
/****** Object:  StoredProcedure [dbo].[VerifyAndDeleteProjectData]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[VerifyAndDeleteProjectData]
    @Code VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    -- Inicializa el resultado en 0 (eliminación fallida) por defecto.
    DECLARE @Result INT = 0

    -- Verifica si existe algún dato igual en la tabla HabitationalProjects
    IF EXISTS (
        SELECT 1
        FROM HabitationalProjects
        WHERE [Code] = @Code
    )
    BEGIN
        -- Si encuentra un registro con el mismo código, elimina la fila
        DELETE FROM HabitationalProjects
        WHERE [Code] = @Code

        -- Establece el resultado en 2 para indicar la eliminación exitosa
        SET @Result = 2
    END
    ELSE
    BEGIN
        -- Si no hay datos duplicados, establece el resultado en 1 (eliminación no realizada)
        SET @Result = 1
    END

    -- Devuelve el resultado
    SELECT @Result AS Result
END
GO
/****** Object:  StoredProcedure [dbo].[VerifyAndDeleteRoot]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[VerifyAndDeleteRoot]
    @ID VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    -- Inicializa el resultado en 0 (eliminación fallida) por defecto.
    DECLARE @Result INT = 0

    -- Verifica si existe algún dato igual en la tabla HabitationalProjects
    IF EXISTS (
        SELECT 1
        FROM RootUsers
        WHERE [ID] = @ID
    )
    BEGIN
        -- Si encuentra un registro con el mismo código, elimina la fila
        DELETE FROM RootUsers
        WHERE [ID] = @ID

        -- Establece el resultado en 2 para indicar la eliminación exitosa
        SET @Result = 2
    END
    ELSE
    BEGIN
        -- Si no hay datos duplicados, establece el resultado en 1 (eliminación no realizada)
        SET @Result = 1
    END

    -- Devuelve el resultado
    SELECT @Result AS Result
END
GO
/****** Object:  StoredProcedure [dbo].[VerifyAndDeleteUser]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[VerifyAndDeleteUser]
    @IdUser VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    -- Inicializa el resultado en 0 (eliminación fallida) por defecto.
    DECLARE @Result INT = 0

    -- Verifica si existe algún dato igual en la tabla Habitation
    IF EXISTS (
        SELECT 1
        FROM Habitation
        WHERE [IDUser] = @IdUser
    )
    BEGIN
        -- Si encuentra un registro con el mismo IDUser en Habitation, elimina la fila
        DELETE FROM Habitation
        WHERE [IDUser] = @IdUser
    END

    -- Verifica si existe algún dato igual en la tabla Users
    IF EXISTS (
        SELECT 1
        FROM Users
        WHERE [IdUser] = @IdUser
    )
    BEGIN
        -- Si encuentra un registro con el mismo IdUser en Users, elimina la fila
        DELETE FROM Users
        WHERE [IdUser] = @IdUser

        -- Establece el resultado en 2 para indicar la eliminación exitosa
        SET @Result = 2
    END
    ELSE
    BEGIN
        -- Si no hay datos duplicados, establece el resultado en 1 (eliminación no realizada)
        SET @Result = 1
    END

    -- Devuelve el resultado
    SELECT @Result AS Result
END
GO
/****** Object:  StoredProcedure [dbo].[VerifyAndInsertProjectData]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[VerifyAndInsertProjectData]
    @Name VARCHAR(MAX),
    @Adress VARCHAR(MAX),
    @Code VARCHAR(MAX),
    @OfficeTelephone VARCHAR(MAX),
    @Logo VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    -- Inicializa el resultado en 0 (datos duplicados) por defecto.
    DECLARE @Result INT = 0

    -- Verifica si existe algún dato igual en la tabla HabitationalProjects
    IF EXISTS (
        SELECT 1
        FROM HabitationalProjects
        WHERE
            [Name] = @Name
            OR [Adress] = @Adress
            OR [Code] = @Code
            OR ISNULL([OfficeTelephone], '') = ISNULL(@OfficeTelephone, '')
            OR [Logo] = @Logo
    )
    BEGIN
        -- Datos duplicados
        SET @Result = 1
    END
    ELSE
    BEGIN
        -- Si no hay datos duplicados, inserta un nuevo elemento en la tabla
        INSERT INTO HabitationalProjects ([Name], [Adress], [Code], [OfficeTelephone], [Logo])
        VALUES (@Name, @Adress, @Code, @OfficeTelephone, @Logo)

        -- Establece el resultado en 2 para indicar la inserción exitosa
        SET @Result = 2
    END

    -- Devuelve el resultado
    SELECT @Result AS Result
END
GO
/****** Object:  StoredProcedure [dbo].[VerifyAndUpdateGuard]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



Create PROCEDURE [dbo].[VerifyAndUpdateGuard]
    @IDGuard VARCHAR(MAX),
    @UserName VARCHAR(MAX),
    @Password VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    -- Inicializa el resultado en 0 (datos duplicados) por defecto.
    DECLARE @Result INT = 0

    -- Verifica si existe algún dato igual en la tabla HabitationalProjects
    IF EXISTS (
        SELECT 1
        FROM Guards
        WHERE [IDGuard] = @IDGuard
    )
    BEGIN
        -- Datos encontrados, realiza el update solo si los datos son diferentes
        UPDATE Guards
        SET
            [UserName] = CASE WHEN [UserName] <> @UserName THEN @UserName ELSE [UserName] END,
            [Password] = CASE WHEN [Password] <> @Password THEN @Password ELSE [Password] END
        WHERE [IDGuard] = @IDGuard

        -- Establece el resultado en 2 para indicar la actualización exitosa
        SET @Result = 2
    END
    ELSE
    BEGIN
        -- Si no hay datos duplicados, devuelve el resultado en 1
        SET @Result = 1
    END

    -- Devuelve el resultado
    SELECT @Result AS Result
END
GO
/****** Object:  StoredProcedure [dbo].[VerifyAndUpdateProjectData]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[VerifyAndUpdateProjectData]
    @Name VARCHAR(MAX),
    @Adress VARCHAR(MAX),
    @Code VARCHAR(MAX),
    @OfficeTelephone VARCHAR(MAX),
    @Logo VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    -- Inicializa el resultado en 0 (datos duplicados) por defecto.
    DECLARE @Result INT = 0

    -- Verifica si existe algún dato igual en la tabla HabitationalProjects
    IF EXISTS (
        SELECT 1
        FROM HabitationalProjects
        WHERE [Code] = @Code
    )
    BEGIN
        -- Datos encontrados, realiza el update solo si los datos son diferentes
        UPDATE HabitationalProjects
        SET
            [Name] = CASE WHEN [Name] <> @Name THEN @Name ELSE [Name] END,
            [Adress] = CASE WHEN [Adress] <> @Adress THEN @Adress ELSE [Adress] END,
            [OfficeTelephone] = CASE WHEN ISNULL([OfficeTelephone], '') <> ISNULL(@OfficeTelephone, '') THEN @OfficeTelephone ELSE [OfficeTelephone] END,
            [Logo] = CASE WHEN [Logo] <> @Logo THEN @Logo ELSE [Logo] END
        WHERE [Code] = @Code

        -- Establece el resultado en 2 para indicar la actualización exitosa
        SET @Result = 2
    END
    ELSE
    BEGIN
        -- Si no hay datos duplicados, devuelve el resultado en 1
        SET @Result = 1
    END

    -- Devuelve el resultado
    SELECT @Result AS Result
END
GO
/****** Object:  StoredProcedure [dbo].[VerifyAndUpdateRoot]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create PROCEDURE [dbo].[VerifyAndUpdateRoot]
    @ID VARCHAR(MAX),
    @UserName VARCHAR(MAX),
    @Password VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    -- Inicializa el resultado en 0 (datos duplicados) por defecto.
    DECLARE @Result INT = 0

    -- Verifica si existe algún dato igual en la tabla HabitationalProjects
    IF EXISTS (
        SELECT 1
        FROM RootUsers
        WHERE [ID] = @ID
    )
    BEGIN
        -- Datos encontrados, realiza el update solo si los datos son diferentes
        UPDATE RootUsers
        SET
            [UserName] = CASE WHEN [UserName] <> @UserName THEN @UserName ELSE [UserName] END,
            [Password] = CASE WHEN [Password] <> @Password THEN @Password ELSE [Password] END
        WHERE [ID] = @ID

        -- Establece el resultado en 2 para indicar la actualización exitosa
        SET @Result = 2
    END
    ELSE
    BEGIN
        -- Si no hay datos duplicados, devuelve el resultado en 1
        SET @Result = 1
    END

    -- Devuelve el resultado
    SELECT @Result AS Result
END
GO
/****** Object:  StoredProcedure [dbo].[VerifyLoginGuard]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[VerifyLoginGuard]
    @Email NVARCHAR(255),
    @Password NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StoredPassword NVARCHAR(255)

    -- Inicializa el resultado en 0 (credenciales inválidas) por defecto.
    DECLARE @Result INT = 0

    -- Verifica si el nombre de usuario existe en la tabla rootusers
    IF EXISTS (SELECT 1 FROM Guards WHERE UserName = @Email)
    BEGIN
        -- Busca la contraseña correspondiente al nombre de usuario proporcionado
        SELECT @StoredPassword = Password
        FROM guards
        WHERE UserName = @Email

        -- Verifica si la contraseña proporcionada coincide con la almacenada
        IF @StoredPassword IS NOT NULL AND @StoredPassword = @Password
        BEGIN
            -- Las credenciales son válidas
            SET @Result = 1
        END
    END

    -- Devuelve el resultado
    SELECT @Result AS Result
END
GO
/****** Object:  StoredProcedure [dbo].[VerifyLoginRoot]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[VerifyLoginRoot]
    @Email NVARCHAR(255),
    @Password NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StoredPassword NVARCHAR(255)

    -- Inicializa el resultado en 0 (credenciales inválidas) por defecto.
    DECLARE @Result INT = 0

    -- Verifica si el nombre de usuario existe en la tabla rootusers
    IF EXISTS (SELECT 1 FROM RootUsers WHERE UserName = @Email)
    BEGIN
        -- Busca la contraseña correspondiente al nombre de usuario proporcionado
        SELECT @StoredPassword = Password
        FROM rootusers
        WHERE UserName = @Email

        -- Verifica si la contraseña proporcionada coincide con la almacenada
        IF @StoredPassword IS NOT NULL AND @StoredPassword = @Password
        BEGIN
            -- Las credenciales son válidas
            SET @Result = 1
        END
    END

    -- Devuelve el resultado
    SELECT @Result AS Result
END
GO
/****** Object:  StoredProcedure [dbo].[VerifyLoginUser]    Script Date: 14/12/2023 8:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[VerifyLoginUser]
    @Email NVARCHAR(255),
    @Password NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StoredPassword NVARCHAR(255)

    -- Inicializa el resultado en 0 (credenciales inválidas) por defecto.
    DECLARE @Result INT = 0

    -- Verifica si el nombre de usuario existe en la tabla rootusers
    IF EXISTS (SELECT 1 FROM Users WHERE Email = @Email)
    BEGIN
        -- Busca la contraseña correspondiente al nombre de usuario proporcionado
        SELECT @StoredPassword = Password
        FROM users
        WHERE Email = @Email

        -- Verifica si la contraseña proporcionada coincide con la almacenada
        IF @StoredPassword IS NOT NULL AND @StoredPassword = @Password
        BEGIN
            -- Las credenciales son válidas
            SET @Result = 1
        END
    END

    -- Devuelve el resultado
    SELECT @Result AS Result
END
GO
