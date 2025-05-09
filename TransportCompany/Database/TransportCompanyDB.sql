USE [master]
GO
/****** Object:  Database [TransportCompanyDB]    Script Date: 06.05.2025 12:18:54 ******/
CREATE DATABASE [TransportCompanyDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TransportCompanyDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\TransportCompanyDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TransportCompanyDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\TransportCompanyDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [TransportCompanyDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TransportCompanyDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TransportCompanyDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TransportCompanyDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TransportCompanyDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TransportCompanyDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TransportCompanyDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [TransportCompanyDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TransportCompanyDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TransportCompanyDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TransportCompanyDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TransportCompanyDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TransportCompanyDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TransportCompanyDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TransportCompanyDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TransportCompanyDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TransportCompanyDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TransportCompanyDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TransportCompanyDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TransportCompanyDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TransportCompanyDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TransportCompanyDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TransportCompanyDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TransportCompanyDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TransportCompanyDB] SET RECOVERY FULL 
GO
ALTER DATABASE [TransportCompanyDB] SET  MULTI_USER 
GO
ALTER DATABASE [TransportCompanyDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TransportCompanyDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TransportCompanyDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TransportCompanyDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TransportCompanyDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TransportCompanyDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TransportCompanyDB', N'ON'
GO
ALTER DATABASE [TransportCompanyDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [TransportCompanyDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [TransportCompanyDB]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 06.05.2025 12:18:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeId] [bigint] IDENTITY(1,1) NOT NULL,
	[StatusId] [bigint] NULL,
	[PostId] [bigint] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[DateOfBirth] [date] NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 06.05.2025 12:18:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[PostId] [bigint] IDENTITY(1,1) NOT NULL,
	[PostName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED 
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Routes]    Script Date: 06.05.2025 12:18:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Routes](
	[RouteId] [bigint] IDENTITY(1,1) NOT NULL,
	[StartPoint] [nvarchar](50) NOT NULL,
	[EndPoint] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Routes] PRIMARY KEY CLUSTERED 
(
	[RouteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Salary]    Script Date: 06.05.2025 12:18:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Salary](
	[SalaryId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[Count] [decimal](18, 0) NOT NULL,
	[Date] [date] NOT NULL,
 CONSTRAINT [PK_Salary] PRIMARY KEY CLUSTERED 
(
	[SalaryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shifts]    Script Date: 06.05.2025 12:18:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shifts](
	[ShiftId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[RoadTrainId] [bigint] NOT NULL,
	[SupplyId] [bigint] NOT NULL,
	[RouteId] [bigint] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[Hours] [int] NOT NULL,
	[Distance] [int] NOT NULL,
 CONSTRAINT [PK_Shifts] PRIMARY KEY CLUSTERED 
(
	[ShiftId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 06.05.2025 12:18:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statuses](
	[StatusId] [bigint] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Statuses] PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplies]    Script Date: 06.05.2025 12:18:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplies](
	[SupplyId] [bigint] IDENTITY(1,1) NOT NULL,
	[SupplyTypeId] [bigint] NOT NULL,
	[SupplyWeight] [int] NOT NULL,
 CONSTRAINT [PK_Supplies] PRIMARY KEY CLUSTERED 
(
	[SupplyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuppliesTypes]    Script Date: 06.05.2025 12:18:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuppliesTypes](
	[SupplyTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[SupplyType] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SuppliesTypes] PRIMARY KEY CLUSTERED 
(
	[SupplyTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trailer]    Script Date: 06.05.2025 12:18:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trailer](
	[TrailerId] [bigint] IDENTITY(1,1) NOT NULL,
	[TrailerTypeId] [bigint] NOT NULL,
	[TrailerNum] [nvarchar](50) NOT NULL,
	[TrailerBrand] [nvarchar](50) NOT NULL,
	[TrailerCarrying] [int] NOT NULL,
 CONSTRAINT [PK_Trailer] PRIMARY KEY CLUSTERED 
(
	[TrailerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TrailerType]    Script Date: 06.05.2025 12:18:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrailerType](
	[TrailerTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[TrailerType] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TrailerType] PRIMARY KEY CLUSTERED 
(
	[TrailerTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transport]    Script Date: 06.05.2025 12:18:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transport](
	[TransportId] [bigint] IDENTITY(1,1) NOT NULL,
	[TransportNum] [nvarchar](50) NOT NULL,
	[TransportBrand] [nvarchar](50) NOT NULL,
	[TransportModel] [nvarchar](50) NOT NULL,
	[Carrying] [int] NOT NULL,
 CONSTRAINT [PK_Transport] PRIMARY KEY CLUSTERED 
(
	[TransportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransportsAndTrailers]    Script Date: 06.05.2025 12:18:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransportsAndTrailers](
	[RoadTrainId] [bigint] IDENTITY(1,1) NOT NULL,
	[TransportId] [bigint] NOT NULL,
	[TrailerId] [bigint] NULL,
 CONSTRAINT [PK_TransportsAndTrailers_1] PRIMARY KEY CLUSTERED 
(
	[RoadTrainId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Posts] FOREIGN KEY([PostId])
REFERENCES [dbo].[Posts] ([PostId])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Posts]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Statuses] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Statuses] ([StatusId])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Statuses]
GO
ALTER TABLE [dbo].[Salary]  WITH CHECK ADD  CONSTRAINT [FK_Salary_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Salary] CHECK CONSTRAINT [FK_Salary_Employees]
GO
ALTER TABLE [dbo].[Shifts]  WITH CHECK ADD  CONSTRAINT [FK_Shifts_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Shifts] CHECK CONSTRAINT [FK_Shifts_Employees]
GO
ALTER TABLE [dbo].[Shifts]  WITH CHECK ADD  CONSTRAINT [FK_Shifts_Routes] FOREIGN KEY([RouteId])
REFERENCES [dbo].[Routes] ([RouteId])
GO
ALTER TABLE [dbo].[Shifts] CHECK CONSTRAINT [FK_Shifts_Routes]
GO
ALTER TABLE [dbo].[Shifts]  WITH CHECK ADD  CONSTRAINT [FK_Shifts_Supplies] FOREIGN KEY([SupplyId])
REFERENCES [dbo].[Supplies] ([SupplyId])
GO
ALTER TABLE [dbo].[Shifts] CHECK CONSTRAINT [FK_Shifts_Supplies]
GO
ALTER TABLE [dbo].[Shifts]  WITH CHECK ADD  CONSTRAINT [FK_Shifts_TransportsAndTrailers1] FOREIGN KEY([RoadTrainId])
REFERENCES [dbo].[TransportsAndTrailers] ([RoadTrainId])
GO
ALTER TABLE [dbo].[Shifts] CHECK CONSTRAINT [FK_Shifts_TransportsAndTrailers1]
GO
ALTER TABLE [dbo].[Supplies]  WITH CHECK ADD  CONSTRAINT [FK_Supplies_SuppliesTypes] FOREIGN KEY([SupplyTypeId])
REFERENCES [dbo].[SuppliesTypes] ([SupplyTypeId])
GO
ALTER TABLE [dbo].[Supplies] CHECK CONSTRAINT [FK_Supplies_SuppliesTypes]
GO
ALTER TABLE [dbo].[Trailer]  WITH CHECK ADD  CONSTRAINT [FK_Trailer_TrailerType] FOREIGN KEY([TrailerTypeId])
REFERENCES [dbo].[TrailerType] ([TrailerTypeId])
GO
ALTER TABLE [dbo].[Trailer] CHECK CONSTRAINT [FK_Trailer_TrailerType]
GO
ALTER TABLE [dbo].[TransportsAndTrailers]  WITH CHECK ADD  CONSTRAINT [FK_TransportsAndTrailers_Trailer1] FOREIGN KEY([TrailerId])
REFERENCES [dbo].[Trailer] ([TrailerId])
GO
ALTER TABLE [dbo].[TransportsAndTrailers] CHECK CONSTRAINT [FK_TransportsAndTrailers_Trailer1]
GO
ALTER TABLE [dbo].[TransportsAndTrailers]  WITH CHECK ADD  CONSTRAINT [FK_TransportsAndTrailers_Transport1] FOREIGN KEY([TransportId])
REFERENCES [dbo].[Transport] ([TransportId])
GO
ALTER TABLE [dbo].[TransportsAndTrailers] CHECK CONSTRAINT [FK_TransportsAndTrailers_Transport1]
GO
USE [master]
GO
ALTER DATABASE [TransportCompanyDB] SET  READ_WRITE 
GO
