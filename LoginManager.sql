﻿USE [master]
GO
if exists(SELECT * FROM master..sysdatabases WHERE name='TechWiz')
DROP DATABASE [TechWiz]
GO
create  database [TechWiz]
go
use [TechWiz]
go
ALTER DATABASE [TechWiz] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TechWiz].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TechWiz] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TechWiz] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TechWiz] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TechWiz] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TechWiz] SET ARITHABORT OFF 
GO
ALTER DATABASE [TechWiz] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TechWiz] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TechWiz] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TechWiz] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TechWiz] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TechWiz] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TechWiz] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TechWiz] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TechWiz] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TechWiz] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TechWiz] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TechWiz] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TechWiz] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TechWiz] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TechWiz] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TechWiz] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [TechWiz] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TechWiz] SET RECOVERY FULL 
GO
ALTER DATABASE [TechWiz] SET  MULTI_USER 
GO
ALTER DATABASE [TechWiz] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TechWiz] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TechWiz] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TechWiz] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TechWiz] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TechWiz] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TechWiz', N'ON'
GO
ALTER DATABASE [TechWiz] SET QUERY_STORE = OFF
GO
USE [TechWiz]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 8/7/2023 1:42:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleClaims]    Script Date: 8/7/2023 1:42:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_RoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 8/7/2023 1:42:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClaims]    Script Date: 8/7/2023 1:42:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogins]    Script Date: 8/7/2023 1:42:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 8/7/2023 1:42:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 8/7/2023 1:42:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[FullName] [nvarchar] (256) NULL,
	[DateOfBirth] datetime null,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTokens]    Script Date: 8/7/2023 1:42:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleClaims_RoleId]    Script Date: 8/7/2023 1:42:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleClaims_RoleId] ON [dbo].[RoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 8/7/2023 1:42:27 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[Roles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserClaims_UserId]    Script Date: 8/7/2023 1:42:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserClaims_UserId] ON [dbo].[UserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserLogins_UserId]    Script Date: 8/7/2023 1:42:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserLogins_UserId] ON [dbo].[UserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserRoles_RoleId]    Script Date: 8/7/2023 1:42:27 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserRoles_RoleId] ON [dbo].[UserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 8/7/2023 1:42:27 PM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[Users]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 8/7/2023 1:42:27 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[Users]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[RoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleClaims] CHECK CONSTRAINT [FK_RoleClaims_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserClaims]  WITH CHECK ADD  CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserClaims] CHECK CONSTRAINT [FK_UserClaims_Users_UserId]
GO
ALTER TABLE [dbo].[UserLogins]  WITH CHECK ADD  CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserLogins] CHECK CONSTRAINT [FK_UserLogins_Users_UserId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users_UserId]
GO
ALTER TABLE [dbo].[UserTokens]  WITH CHECK ADD  CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserTokens] CHECK CONSTRAINT [FK_UserTokens_Users_UserId]
GO


--------------------------------------------------------------

CREATE TABLE Category(
	[Id] int identity primary key,
	[Name] varchar(255) not null,
)

CREATE TABLE Discount(
	[Id] int identity primary key,
	[Name] varchar(255) not null,
	[Percent] float not null,
)

CREATE TABLE Product(
	[Id] int identity primary key,
	[Name] nvarchar(255) not null,
	[Description] nvarchar(255) not null,
	[Price] decimal(10,2),
	[BasePrice] decimal(10,2),
	[ImageURL] nvarchar(255) not null,
	[TypeProduct] varchar(255) check (TypeProduct in ('Plant','Accessories')),
	[DiscountId] int foreign key references Discount(Id),
)

CREATE TABLE CategoryProduct(
	[Id] int identity primary key,
	[CategoryId] int foreign key references Category(Id),
	[ProductId] int foreign key references Product(Id),
)


CREATE TABLE Review(
	[Id] int identity primary key,
	[Content] nvarchar(255),
	[Rating] int check (Rating in (0,1,2,3,4,5)),
	[ReviewDate] datetime not null,
	[ProductId] int foreign key references Product(id),
	[UserId] nvarchar(450) foreign key references Users(Id)
)

CREATE TABLE Feedback (
  [Id] int PRIMARY KEY identity,
  Content nvarchar(255) NOT NULL,
  FeedbackDate datetime NOT NULL,
  [UserId] nvarchar(450) foreign key references Users(Id) null
);

CREATE TABLE Bill (
  Id int PRIMARY KEY identity,
  OrderDate datetime NOT NULL,
  Total decimal NOT NULL,
  [Status] varchar check (Status in ('Cancel','Pending','Success')),
  DeliveryPhone varchar(13) NOT NULL,
  DeliveryAddress varchar(255) Not null,
  [UserId] nvarchar(450) foreign key references Users(Id)
);

CREATE TABLE ProductBill (
  [Id] int PRIMARY KEY identity,
  [Quantity] int not null,
  [SalePrice] decimal(10,2) not null,
  [ProductId] int foreign key references Product(Id),
  [BillId] int foreign key references Bill(Id)
);

insert into Roles(ID,Name,NormalizedName) values ('R01','admin','admin'),('R02','customer','CUSTOMER');
insert into Users(ID,UserName,NormalizedUserName,FullName,EmailConfirmed,PasswordHash,SecurityStamp,
ConcurrencyStamp,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnabled,AccessFailedCount) 
values ('a60e78a0-c268-43fc-921a-703e2f49b2dc','admin','ADMIN','Boss',1,
'AQAAAAEAACcQAAAAEDa4YtfB1rQQoAwzXUQXk4hKBWACb0NJS1ccNPDTtOtzH4DjUG2ZUTlZS1pXGV22HA==',
'EKLE7XVENYNZGUOEAA56TBV4I76DDC7U',
'545a001c-35be-4cda-ab99-b11320b7ccfe',0,0,1,0),
('96ad3edd-6c77-4cc0-98e9-6112d50565c0','admin2','ADMIN2','Boss',1,
'AQAAAAEAACcQAAAAEHL461RdCShEKbs9moHwe4gtfFsi69CL4OD2lt69YtLMg2r2/q0JVSmEjEjz1G3h4A==',
'OYHUVUGBADB456ZFQWGF4H3OECBW26QI',
'51ee78a1-4764-4a95-9280-521a05aaa8c0',0,0,1,0);
go
insert into UserRoles 
values ('a60e78a0-c268-43fc-921a-703e2f49b2dc','R01'),('96ad3edd-6c77-4cc0-98e9-6112d50565c0','R01')
go



CREATE TRIGGER insertUserRoles
ON Users
AFTER INSERT
AS
BEGIN
    INSERT INTO UserRoles (UserId, RoleId)
    SELECT i.id,'R02'
    FROM inserted i;
END;




