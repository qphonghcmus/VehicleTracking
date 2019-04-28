USE [master]
GO
/****** Object:  Database [VehicleData]    Script Date: 4/28/2019 8:47:48 PM ******/
CREATE DATABASE [VehicleData]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VehicleData', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\VehicleData.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'VehicleData_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\VehicleData_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [VehicleData] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VehicleData].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VehicleData] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VehicleData] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VehicleData] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VehicleData] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VehicleData] SET ARITHABORT OFF 
GO
ALTER DATABASE [VehicleData] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VehicleData] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VehicleData] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VehicleData] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VehicleData] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VehicleData] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VehicleData] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VehicleData] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VehicleData] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VehicleData] SET  DISABLE_BROKER 
GO
ALTER DATABASE [VehicleData] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VehicleData] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VehicleData] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VehicleData] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VehicleData] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VehicleData] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VehicleData] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VehicleData] SET RECOVERY FULL 
GO
ALTER DATABASE [VehicleData] SET  MULTI_USER 
GO
ALTER DATABASE [VehicleData] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VehicleData] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VehicleData] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VehicleData] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [VehicleData] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'VehicleData', N'ON'
GO
USE [VehicleData]
GO
/****** Object:  Table [dbo].[CT]    Script Date: 4/28/2019 8:47:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CT](
	[_From] [varchar](50) NOT NULL,
	[SDT] [varchar](50) NOT NULL,
	[Malenh] [varchar](50) NOT NULL,
	[OK/ERROR] [varchar](50) NOT NULL,
	[Datetime] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[D]    Script Date: 4/28/2019 8:47:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[D](
	[IDCuocxe] [varchar](50) NOT NULL,
	[Thoigian] [varchar](50) NOT NULL,
	[Datetime] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[G]    Script Date: 4/28/2019 8:47:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[G](
	[Status] [varchar](50) NOT NULL,
	[Vido] [varchar](50) NOT NULL,
	[Kinhdo] [varchar](50) NOT NULL,
	[Vantoc] [varchar](50) NOT NULL,
	[Khoangcach] [varchar](50) NOT NULL,
	[TongKhoangcach] [varchar](50) NOT NULL,
	[Datetime] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[H1]    Script Date: 4/28/2019 8:47:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[H1](
	[MaUID] [varchar](50) NOT NULL,
	[Giaypheplaixe] [varchar](50) NOT NULL,
	[Vantocxe] [varchar](50) NOT NULL,
	[Datetime] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[I]    Script Date: 4/28/2019 8:47:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[I](
	[ID] [varchar](50) NOT NULL,
	[Serial] [varchar](50) NOT NULL,
	[Datetime] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RawData]    Script Date: 4/28/2019 8:47:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RawData](
	[Rawdata] [nvarchar](max) NOT NULL,
	[Datetime] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[S1]    Script Date: 4/28/2019 8:47:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[S1](
	[Trangthai] [varchar](50) NOT NULL,
	[Dienapbinh] [varchar](50) NOT NULL,
	[Dienappin] [varchar](50) NOT NULL,
	[CuongdoGSM] [varchar](50) NOT NULL,
	[Loithenho] [varchar](50) NOT NULL,
	[Datetime] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [master]
GO
ALTER DATABASE [VehicleData] SET  READ_WRITE 
GO
