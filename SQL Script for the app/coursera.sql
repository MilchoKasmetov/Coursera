USE [master]
GO
/****** Object:  Database [coursera]    Script Date: 3/16/2020 1:59:43 PM ******/
CREATE DATABASE [coursera]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'coursera', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\coursera.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'coursera_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\coursera_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [coursera] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [coursera].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [coursera] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [coursera] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [coursera] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [coursera] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [coursera] SET ARITHABORT OFF 
GO
ALTER DATABASE [coursera] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [coursera] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [coursera] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [coursera] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [coursera] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [coursera] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [coursera] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [coursera] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [coursera] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [coursera] SET  DISABLE_BROKER 
GO
ALTER DATABASE [coursera] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [coursera] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [coursera] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [coursera] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [coursera] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [coursera] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [coursera] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [coursera] SET RECOVERY FULL 
GO
ALTER DATABASE [coursera] SET  MULTI_USER 
GO
ALTER DATABASE [coursera] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [coursera] SET DB_CHAINING OFF 
GO
ALTER DATABASE [coursera] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [coursera] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [coursera] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'coursera', N'ON'
GO
USE [coursera]
GO
/****** Object:  Table [dbo].[courses]    Script Date: 3/16/2020 1:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[courses](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](150) NOT NULL,
	[instructor_id] [int] NOT NULL,
	[total_time] [tinyint] NOT NULL,
	[credit] [tinyint] NOT NULL,
	[time_created] [datetime] NOT NULL CONSTRAINT [DF_courses_time_created]  DEFAULT (getdate()),
 CONSTRAINT [PK_courses] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[instructors]    Script Date: 3/16/2020 1:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[instructors](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [nvarchar](100) NOT NULL,
	[last_name] [nvarchar](100) NOT NULL,
	[time_created] [datetime] NOT NULL CONSTRAINT [DF_teachers_time_created]  DEFAULT (getdate()),
 CONSTRAINT [PK_teachers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[students]    Script Date: 3/16/2020 1:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[students](
	[pin] [nchar](10) NOT NULL,
	[first_name] [nvarchar](50) NOT NULL,
	[last_name] [nvarchar](50) NOT NULL,
	[time_created] [datetime] NOT NULL CONSTRAINT [DF_students_time_created]  DEFAULT (getdate()),
 CONSTRAINT [PK_students] PRIMARY KEY CLUSTERED 
(
	[pin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[students_courses_xref]    Script Date: 3/16/2020 1:59:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[students_courses_xref](
	[student_pin] [nchar](10) NOT NULL,
	[course_id] [int] NOT NULL,
	[completion_date] [date] NULL,
 CONSTRAINT [PK_students_courses_xref] PRIMARY KEY CLUSTERED 
(
	[student_pin] ASC,
	[course_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[courses] ON 

GO
INSERT [dbo].[courses] ([id], [name], [instructor_id], [total_time], [credit], [time_created]) VALUES (1, N'Analysis', 1, 20, 10, CAST(N'2020-03-16 13:26:44.283' AS DateTime))
GO
INSERT [dbo].[courses] ([id], [name], [instructor_id], [total_time], [credit], [time_created]) VALUES (2, N'Linear Algebra', 1, 30, 15, CAST(N'2020-03-16 13:27:26.300' AS DateTime))
GO
INSERT [dbo].[courses] ([id], [name], [instructor_id], [total_time], [credit], [time_created]) VALUES (3, N'Statistics', 2, 30, 15, CAST(N'2020-03-16 13:27:38.417' AS DateTime))
GO
INSERT [dbo].[courses] ([id], [name], [instructor_id], [total_time], [credit], [time_created]) VALUES (4, N'Geometry', 3, 35, 20, CAST(N'2020-03-16 13:27:54.013' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[courses] OFF
GO
SET IDENTITY_INSERT [dbo].[instructors] ON 

GO
INSERT [dbo].[instructors] ([id], [first_name], [last_name], [time_created]) VALUES (1, N'Neno', N'Dimitrov', CAST(N'2020-03-16 13:25:34.973' AS DateTime))
GO
INSERT [dbo].[instructors] ([id], [first_name], [last_name], [time_created]) VALUES (2, N'Petko', N'Valchev', CAST(N'2020-03-16 13:26:00.143' AS DateTime))
GO
INSERT [dbo].[instructors] ([id], [first_name], [last_name], [time_created]) VALUES (3, N'Petar', N'Penchev', CAST(N'2020-03-16 13:26:12.613' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[instructors] OFF
GO
INSERT [dbo].[students] ([pin], [first_name], [last_name], [time_created]) VALUES (N'9412011005', N'Krasimir', N'Petrov', CAST(N'2020-03-16 13:23:58.777' AS DateTime))
GO
INSERT [dbo].[students] ([pin], [first_name], [last_name], [time_created]) VALUES (N'9501011014', N'Elena', N'Foteva', CAST(N'2020-03-16 13:24:29.853' AS DateTime))
GO
INSERT [dbo].[students] ([pin], [first_name], [last_name], [time_created]) VALUES (N'9507141009', N'Ivan', N'Ivanov', CAST(N'2020-03-16 13:23:39.220' AS DateTime))
GO
INSERT [dbo].[students_courses_xref] ([student_pin], [course_id], [completion_date]) VALUES (N'9412011005', 1, CAST(N'2019-07-16' AS Date))
GO
INSERT [dbo].[students_courses_xref] ([student_pin], [course_id], [completion_date]) VALUES (N'9412011005', 2, CAST(N'2019-08-20' AS Date))
GO
INSERT [dbo].[students_courses_xref] ([student_pin], [course_id], [completion_date]) VALUES (N'9501011014', 1, CAST(N'2019-07-16' AS Date))
GO
INSERT [dbo].[students_courses_xref] ([student_pin], [course_id], [completion_date]) VALUES (N'9501011014', 2, CAST(N'2019-08-01' AS Date))
GO
INSERT [dbo].[students_courses_xref] ([student_pin], [course_id], [completion_date]) VALUES (N'9501011014', 3, CAST(N'2019-10-01' AS Date))
GO
INSERT [dbo].[students_courses_xref] ([student_pin], [course_id], [completion_date]) VALUES (N'9501011014', 4, CAST(N'2019-12-05' AS Date))
GO
INSERT [dbo].[students_courses_xref] ([student_pin], [course_id], [completion_date]) VALUES (N'9507141009', 4, CAST(N'2019-08-20' AS Date))
GO
ALTER TABLE [dbo].[courses]  WITH CHECK ADD  CONSTRAINT [FK_courses_instructors] FOREIGN KEY([instructor_id])
REFERENCES [dbo].[instructors] ([id])
GO
ALTER TABLE [dbo].[courses] CHECK CONSTRAINT [FK_courses_instructors]
GO
ALTER TABLE [dbo].[students_courses_xref]  WITH CHECK ADD  CONSTRAINT [FK_students_courses_xref_courses] FOREIGN KEY([course_id])
REFERENCES [dbo].[courses] ([id])
GO
ALTER TABLE [dbo].[students_courses_xref] CHECK CONSTRAINT [FK_students_courses_xref_courses]
GO
ALTER TABLE [dbo].[students_courses_xref]  WITH CHECK ADD  CONSTRAINT [FK_students_courses_xref_students] FOREIGN KEY([student_pin])
REFERENCES [dbo].[students] ([pin])
GO
ALTER TABLE [dbo].[students_courses_xref] CHECK CONSTRAINT [FK_students_courses_xref_students]
GO
USE [master]
GO
ALTER DATABASE [coursera] SET  READ_WRITE 
GO
