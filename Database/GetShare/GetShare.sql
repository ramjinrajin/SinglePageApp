USE [master]
GO
/****** Object:  Database [GetShare]    Script Date: 5/16/2016 11:03:29 AM ******/
CREATE DATABASE [GetShare] ON  PRIMARY 
( NAME = N'GetShare_Data', FILENAME = N'c:\dzsqls\GetShare.mdf' , SIZE = 2304KB , MAXSIZE = 15360KB , FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'GetShare_Logs', FILENAME = N'c:\dzsqls\GetShare.ldf' , SIZE = 1024KB , MAXSIZE = 20480KB , FILEGROWTH = 10%)
GO
ALTER DATABASE [GetShare] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GetShare].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GetShare] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GetShare] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GetShare] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GetShare] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GetShare] SET ARITHABORT OFF 
GO
ALTER DATABASE [GetShare] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [GetShare] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [GetShare] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GetShare] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GetShare] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GetShare] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GetShare] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GetShare] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GetShare] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GetShare] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GetShare] SET  ENABLE_BROKER 
GO
ALTER DATABASE [GetShare] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GetShare] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GetShare] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GetShare] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GetShare] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GetShare] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GetShare] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GetShare] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [GetShare] SET  MULTI_USER 
GO
ALTER DATABASE [GetShare] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GetShare] SET DB_CHAINING OFF 
GO
USE [GetShare]
GO
/****** Object:  User [ramjin]    Script Date: 5/16/2016 11:03:32 AM ******/
CREATE USER [ramjin] FOR LOGIN [ramjin] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [ramjin]
GO
/****** Object:  Schema [ramjin]    Script Date: 5/16/2016 11:03:34 AM ******/
CREATE SCHEMA [ramjin]
GO
/****** Object:  StoredProcedure [dbo].[USP_ContactUSInsert]    Script Date: 5/16/2016 11:03:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[USP_ContactUSInsert]
@Name       varchar(100),
@Email       nvarchar(max),
@Subject     varchar(100),
@Message    varchar(100)
AS
BEGIN
    SET NOCOUNT ON;

	INSERT INTO
	[dbo].[Gs_ContactUs]
	(
	Name,
	Email,
	Subject,
	Message
	)
	SELECT
	@Name, @Email,@Subject,@Message

	SET NOCOUNT OFF;
END

GO
/****** Object:  StoredProcedure [dbo].[USP_FeedInsert]    Script Date: 5/16/2016 11:03:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[USP_FeedInsert]
@Title       varchar(100),
@Description nvarchar(max),
@Images      varchar(100),
@link varchar(100),
@Category_Id int
AS
BEGIN

   SET NOCOUNT ON;

	INSERT INTO



	[dbo].[Gs_Feed]



	(



	Title,



	[Description],



	Images,



	link,

	Category_Id



	)


	SELECT



	@Title, @Description,@Images,@link,@Category_Id


	SET NOCOUNT OFF;

END
GO
/****** Object:  StoredProcedure [dbo].[USP_FeedUPDATE]    Script Date: 5/16/2016 11:03:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[USP_FeedUPDATE]

@FeedID    int,

@Title       varchar(100),

@Description nvarchar(max),

@Images      varchar(100),

@link    varchar(100),

@Rowstatus char(1),
@Category_Id int

AS

BEGIN

    SET NOCOUNT ON;



	UPDATE GS_FEED

	SET Title=@Title,

		Description=@Description,

		Images=@Images,

		link=@link,

		Rowstatus=@Rowstatus,

		modifieddate=GETDATE(),

		modifedby=100,
		Category_Id=@Category_Id

	WHERE FeedID=@FeedID

	

	SET NOCOUNT OFF;

END

GO
/****** Object:  StoredProcedure [dbo].[USP_SubscribeInsert]    Script Date: 5/16/2016 11:03:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[USP_SubscribeInsert]
@Name       varchar(100),
@Email       nvarchar(max)
AS
BEGIN
    SET NOCOUNT ON;

	INSERT INTO
	[dbo].Gs_Subscribe
	(
	Name,
	Email
	)
	SELECT
	@Name, @Email

	SET NOCOUNT OFF;
END

GO
/****** Object:  Table [dbo].[Gs_Category]    Script Date: 5/16/2016 11:03:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Gs_Category](
	[Category_Id] [int] IDENTITY(1,1) NOT NULL,
	[Category_Name] [varchar](100) NULL,
	[Rowstatus] [char](1) NULL,
	[createdate] [datetime] NULL,
	[modifieddate] [datetime] NULL,
	[createdby] [int] NULL,
	[modifedby] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Category_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Gs_ContactUs]    Script Date: 5/16/2016 11:03:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Gs_ContactUs](
	[Cont_id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[Email] [nvarchar](max) NULL,
	[Subject] [varchar](100) NULL,
	[Message] [varchar](100) NULL,
	[createdate] [datetime] NULL,
	[modifieddate] [datetime] NULL,
	[createdby] [int] NULL,
	[modifedby] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Cont_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Gs_Feed]    Script Date: 5/16/2016 11:03:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Gs_Feed](
	[FeedID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](100) NULL,
	[Description] [nvarchar](max) NULL,
	[Images] [varchar](100) NULL,
	[link] [varchar](100) NULL,
	[Rowstatus] [char](1) NULL,
	[createdate] [datetime] NULL,
	[modifieddate] [datetime] NULL,
	[createdby] [int] NULL,
	[modifedby] [int] NULL,
	[Category_Id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[FeedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Gs_Log]    Script Date: 5/16/2016 11:03:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Gs_Log](
	[LogID] [int] IDENTITY(1,1) NOT NULL,
	[Logtitle] [varchar](10) NULL,
	[ErrorDesc] [nvarchar](max) NULL,
	[Formatmsgs] [nvarchar](max) NULL,
	[createdate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Gs_Subscribe]    Script Date: 5/16/2016 11:03:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Gs_Subscribe](
	[Sub_id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[Email] [nvarchar](max) NULL,
	[createdate] [datetime] NULL,
	[modifieddate] [datetime] NULL,
	[createdby] [int] NULL,
	[modifedby] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Sub_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Gs_users]    Script Date: 5/16/2016 11:03:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Gs_users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](100) NULL,
	[EmailID] [varchar](50) NULL,
	[mobilenno] [int] NULL,
	[description] [nvarchar](max) NULL,
	[Rowstatus] [char](1) NULL,
	[createdate] [datetime] NULL,
	[modifieddate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Gs_Category] ADD  DEFAULT ('A') FOR [Rowstatus]
GO
ALTER TABLE [dbo].[Gs_Category] ADD  DEFAULT (getdate()) FOR [createdate]
GO
ALTER TABLE [dbo].[Gs_ContactUs] ADD  DEFAULT (getdate()) FOR [createdate]
GO
ALTER TABLE [dbo].[Gs_Feed] ADD  DEFAULT ('A') FOR [Rowstatus]
GO
ALTER TABLE [dbo].[Gs_Feed] ADD  DEFAULT (getdate()) FOR [createdate]
GO
ALTER TABLE [dbo].[Gs_Log] ADD  DEFAULT (getdate()) FOR [createdate]
GO
ALTER TABLE [dbo].[Gs_Subscribe] ADD  DEFAULT (getdate()) FOR [createdate]
GO
ALTER TABLE [dbo].[Gs_users] ADD  DEFAULT (getdate()) FOR [Rowstatus]
GO
ALTER TABLE [dbo].[Gs_users] ADD  DEFAULT (getdate()) FOR [createdate]
GO
USE [master]
GO
ALTER DATABASE [GetShare] SET  READ_WRITE 
GO
