USE [master]
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
	[Address] nvarchar(256) null,
	[status] bit default 1,
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
	[DateBegin] datetime,
	[DateEnd] datetime
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
	[InventoryQuantity] int,
	[Status] bit default 1
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
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name]) VALUES (1, N'Flowering')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (2, N'Non-flowering')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (3, N'Indoor')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (4, N'Outdoor')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (5, N'Succulents')
INSERT [dbo].[Category] ([Id], [Name]) VALUES (6, N'Medicinal and so on')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[CategoryProduct] ON 

INSERT [dbo].[CategoryProduct] ([Id], [CategoryId], [ProductId]) VALUES (2, 1, 6)
INSERT [dbo].[CategoryProduct] ([Id], [CategoryId], [ProductId]) VALUES (3, 2, 14)
INSERT [dbo].[CategoryProduct] ([Id], [CategoryId], [ProductId]) VALUES (4, 3, 10)
INSERT [dbo].[CategoryProduct] ([Id], [CategoryId], [ProductId]) VALUES (5, 4, 17)
INSERT [dbo].[CategoryProduct] ([Id], [CategoryId], [ProductId]) VALUES (6, 5, 9)
INSERT [dbo].[CategoryProduct] ([Id], [CategoryId], [ProductId]) VALUES (7, 6, 16)
INSERT [dbo].[CategoryProduct] ([Id], [CategoryId], [ProductId]) VALUES (8, 2, 11)
INSERT [dbo].[CategoryProduct] ([Id], [CategoryId], [ProductId]) VALUES (9, 4, 15)
SET IDENTITY_INSERT [dbo].[CategoryProduct] OFF
GO
SET IDENTITY_INSERT [dbo].[Discount] ON 

INSERT [dbo].[Discount] ([Id], [Name], [Percent]) VALUES (1, N'10% OFF', 0.1)
INSERT [dbo].[Discount] ([Id], [Name], [Percent]) VALUES (2, N'20% OFF', 0.2)
INSERT [dbo].[Discount] ([Id], [Name], [Percent]) VALUES (3, N'30 % OFF', 0.3)
SET IDENTITY_INSERT [dbo].[Discount] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [BasePrice], [ImageURL], [TypeProduct], [DiscountId], [InventoryQuantity], [CreatedAt], [Size]) VALUES (6, N'Yoshino Cherry Tree', N'Pink Blooms and Tropical Growth Through Autumn
Why Pink Silk Floss Trees?
A uniform silhouette sprouts green limbs for a tropical-inspired, ultra-healthy look, making the Pink Silk Floss Tree second to none. But it truly wows with gorgeous, funnel-shaped and Hibiscus-like pink blooms that cover the canopy during the autumn months, from October to November.

Imagine stunning, one-of-a-kind summer color that emerges in fall. The florals are followed up woody, pear-shaped seed pods containing silken floss. Living up to its name, this South America native’s floss was traditionally used for jackets, pillows and rope.

Why Fast-Growing-Trees.com is Better
The best part about the Pink Silk Floss Tree? For starters, though it’s initially a fast grower, its growth slows as it matures, so it’s more manageable for you. Whether it’s used along highways, residential areas or as a focal point shade tree in your backyard, the Pink Silk Floss is adaptable and versatile. But the top benefit is the fact that it’s planted, monitored and shipped with the utmost care, so it arrives ready to grow for you.

We’ve done the hard work long before the Pink Silk Floss arrives to your door, meaning so hassle for you. It doesn’t get much simpler than a ready-made, great-looking plant! Get a Pink Silk Floss Tree of your own – see what all the hype’s about!', CAST(250.00 AS Decimal(10, 2)), CAST(149.00 AS Decimal(10, 2)), N'/images/yoshiocherry', N'Plant', 1, 20, CAST(N'2023-08-04T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [BasePrice], [ImageURL], [TypeProduct], [DiscountId], [InventoryQuantity], [CreatedAt], [Size]) VALUES (9, N'Hong Kong Orchid Tree', N'Five Months of Vibrant Winter Flowers
Why Hong Kong Orchid Trees?

Bring your winter landscape to life with the large, pinkish-purple blooms of the Hong Kong Orchid Tree. These six-inch fragrant blooms first appear in November, just in time for the holidays, and deliver five months of eye-catching color.

The Hong Kong’s vivid blooms pop against barren winter landscapes. Plus, the Hong Kong Orchid Tree is versatile – if you live in a colder climate, you can plant it in a container for your patio or even bring it indoors when the temperatures drop. In the South, the Hong Kong’s thick, lush foliage grows in an umbrella-shaped canopy through the season, offering the perfect privacy screen or border along your driveway.

Even better is the fact that the Hong Kong Orchid couldn’t be easier to grow. Hong Kong Orchid Trees will adapt to a variety of poor soils, ranging from sandy to heavy clay. And with a high level of drought tolerance and heat resistance, they flourish in dry, hot conditions. Nothing stops the Hong Kong Orchid from blooming for months.

Why Fast-Growing-Trees.com is Better

You won’t find a Hong Kong Orchid that’s this healthy and well-developed at your local nursery or garden center…if they’re even available.

Each tree is a direct descendent from rootstock, sourced in a botanical garden in Hong Kong. And we’ve planted, grown and shipped this hard-to-find Orchid Tree with meticulous care. Now, your Hong Kong Orchid Tree is hassle-free and ready to produce color as soon as it arrives at your door.

When you buy our larger-sized Hong Kong Orchids, you can even get amazing winter color as soon as the first year.

Enliven your winter landscape with the vibrant blooms and effortless growth of the Hong Kong Orchid. Don’t wait – these unique trees sell out fast. Order your Hong Kong Orchid Tree today!', CAST(360.00 AS Decimal(10, 2)), CAST(250.00 AS Decimal(10, 2)), N'/images/hkorchird.jpg', N'Plant', 2, 23, CAST(N'2023-07-09T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [BasePrice], [ImageURL], [TypeProduct], [DiscountId], [InventoryQuantity], [CreatedAt], [Size]) VALUES (10, N'Royal Poinciana Tree', N'Welcomes Spring with Flame-Colored Blooms
Why Royal Poinciana Trees?

Marking the start of spring each year with thousands of bright red blooms, the Royal Poinciana Tree’s rich color is not commonly seen in other flowering varieties. In fact, it’s also called the ‘Flame Tree’ for its blazing color.

While many others offer pink or white flowers at the start of the season, the Poinciana’s flame-colored hues stand out. In fact, Poinciana Trees are often the brightest trees in the neighborhood, attracting the attention of guests and neighbors alike.

And the Poinciana’s lush, long slender leaves offer a tropical look against its crimson blooms. Plus, because Poincianas are semi-evergreen, they won''t lose their leaves in warmer climates. and will provide your yard with abundant green foliage all year.

Even better? Because they can grow up to five feet a year, you get these exotic colors and shade quickly. So, not only do you get one-of-a-kind beauty, but you also get a tree that shields your home from the summer heat and provides extra insulation in the winter, saving you money and energy from season to season. And these vigorous growers have an extremely high drought tolerance and grow well in the dry, sandy areas where other flowering trees can''t take the heat.

Why Fast-Growing-Trees.com is Better

Transforming any landscape, even one with tough growing conditions, into a relaxing oasis, your Poinciana Tree is second to none. Especially when you order from Fast Growing Trees since we’ve planted, grown and nurtured your Poinciana, long before shipping.

Now, your Royal Poinciana is shipped to your door with a healthier root system, better branching and a head start on growth. And with our larger sizes, you’ll see blazing color and shade as soon as the first season.

Don''t miss your chance – Poincianas are selling out fast and we recommend getting yours today, before they''re gone. Order the vibrant red hues of your Royal Poinciana today!', CAST(183.00 AS Decimal(10, 2)), CAST(286.00 AS Decimal(10, 2)), N'/images/royalpoinciana.jpg', N'Plant', 2, 10, CAST(N'2023-04-06T00:00:00.000' AS DateTime), 3)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [BasePrice], [ImageURL], [TypeProduct], [DiscountId], [InventoryQuantity], [CreatedAt], [Size]) VALUES (11, N'Meyer Lemon Tree', N'The All-In-One Citrus Tree for Every Home and Patio
Why Meyer Lemon Trees?

Meyer Lemons are different from the small, tart and acidic lemons at your grocery store because the fruit is literally a cross between traditional sour lemons and sweet oranges – yes, you get both sweet and savory flavors from each squeeze of this world-beating fruit!

As you know, the fruit available at your grocery store is not chosen for flavor but rather shelf life – that is why the lemons available to you are small with very thick skin. Meyer Lemons are not available in grocery stores because the fruit skin is so wonderfully thin that it would bruise while riding in a crate – however, the thin skin is perfect for home chefs that want tantalizingly fresh fruit right off the branch! The thin skin allows the citrus juices to develop fully, making it the perfect raw fruit for juices, desserts, and flavoring. Can I grow it? YES, YOU CAN! The Meyer Lemon Tree has remarkable cold and heat tolerance so anyone in the country can grow it – if your winters get cold, simply bring your Meyer Lemon Tree indoors for the winter. Our trees max out at around 8 feet so you don’t have to worry about them out-growing your space. And when you bring it indoors, you can enjoy the jasmine-citrus fragrance throughout those long winter months. If your sun exposure or growing conditions are less than ideal, then we recommend you start with one of our larger trees.

Place your order NOW and have your own Meyer Lemon Tree delivered right to your door.

Looking for that unique gift? Look no further! It’s the gift that truly keeps on giving – a memorable keepsake to commemorate holidays, housewarmings, birthdays, bereavements – this tree will produce fruit for decades and every time someone picks a lemon they will think of you!', CAST(145.00 AS Decimal(10, 2)), CAST(287.00 AS Decimal(10, 2)), N'/images/meyerlemon.jpg', N'Plant', 3, 46, CAST(N'2023-01-09T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [BasePrice], [ImageURL], [TypeProduct], [DiscountId], [InventoryQuantity], [CreatedAt], [Size]) VALUES (14, N'Sweetheart Blueberry Bush', N'Produces Two Large Crops Each Year!
Why Sweetheart Blueberry Bushes?

With the Sweetheart Blueberry Bush, you get huge harvests each year under most conditions. A large bounty of fist-sized blueberry clusters arrives in late May or June and the second one in August, yielding around 15 pounds of blueberries per year.

The big, sweet berries are its main attraction. The Sweetheart Bush will produce medium to large sweet blueberries. The perfect hybrid of Southern and Northern Highbush varieties, the fruit is amazingly juicy and plump yet firm. If you like to bake blueberry muffins and make blueberry pancakes, these blueberries are ideal. The texture plus the rich flavor make them a favorite for cooking, but they''re also a delicious snack picked right off the bush.

Why Fast-Growing-Trees.com is Better

It''s easy: You get the promise of delicious, home-grown fruit delivered, without any hassle on your part. That''s because we''ve planted, grown and nurtured your Sweetheart Blueberry Bush from day one, long before it ships to your door, so you get a healthier root system and better long-term results in your homescape.

You won''t find the same healthy fruit variety, with the promise of fast fruiting, at your local nursery or big box store.

These bushes are selling fast. Order today for home-grown berries from your very own Sweetheart Blueberry Bush!', CAST(48.00 AS Decimal(10, 2)), CAST(33.00 AS Decimal(10, 2)), N'/images/blueberry.jpg', N'Plant', 2, 35, CAST(N'2023-06-07T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [BasePrice], [ImageURL], [TypeProduct], [DiscountId], [InventoryQuantity], [CreatedAt], [Size]) VALUES (15, N'Calamondin Tree', N'One of the Cold Hardiest Citrus Trees
Why Calamondin Trees?

There’s nothing like the Calamondin Tree: With fruit that’s a cross between a mandarin and a kumquat, this "mini-orange,” as it’s dubbed, is wonderfully delicious.

Even better? It can be grown in a container or as a hedge. In colder climates, grow the Calamondin Tree in a container and move it indoors during winter months, or cover your outdoor bushes for chilly weather.

Plus, the Calamondin delights with a heavenly scent and several flushes (harvests) of blossoms and fruit throughout the year. The yellow-orange fruit of the Calamondin Tree is about the size of a lime or a tangerine and has a thinner skin than most oranges, making it delectably delicate. It has a lightly tart taste that''s perfect for marmalades, juicing, cooking and baking.

Why FastGrowingTrees.com is Better

Best of all, year-round blooms give way to fruiting between June and November – and bounties after only one to two years. And younger bushes hold even more fruit.

But because we’ve planted, grown and nurtured your Calamondin from day one, you also get a guarantee: Easy, effortless performance and harvests that are second to none. We’ve done the hard work at our nursery so you get great results from your Calamondin.

With healthful, home-grown flavor and heightened looks, the Calamondin Tree is second to none. Order yours today!', CAST(340.00 AS Decimal(10, 2)), CAST(290.00 AS Decimal(10, 2)), N'/images/calamondin ', N'Plant', 1, 13, CAST(N'2023-07-04T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [BasePrice], [ImageURL], [TypeProduct], [DiscountId], [InventoryQuantity], [CreatedAt], [Size]) VALUES (16, N'Fittonia plant', N'Fittonia or nerve plants are fairly easy to care for they can tolerate a range of lighting conditions. The Fittonia plant leaves have deep veins that run across the leaves to form a pattern and so comes the name - nerve plant. They prefer humid environments, well-drained moist soil, but not too wet. water moderately and let the plants dry out between waterings. 
These little beauties do well in terrariums, hanging baskets, dish gardens, or even as a ground cover in the right climate.', CAST(23.00 AS Decimal(10, 2)), CAST(20.00 AS Decimal(10, 2)), N'/images/fittonia.jpg', N'Plant', 2, 10, CAST(N'2023-04-03T00:00:00.000' AS DateTime), 3)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [BasePrice], [ImageURL], [TypeProduct], [DiscountId], [InventoryQuantity], [CreatedAt], [Size]) VALUES (17, N'Buckle Tree tie', N'Rubber buckle tree tie (separates the tree from the stake).They will stretch as the tree grows and are easily adjustable. They are supplied with a hoop collar spacer.  Our buckle ties are size 45cm in length.', CAST(12.00 AS Decimal(10, 2)), CAST(10.00 AS Decimal(10, 2)), N'/images/BuckleTreeTie.jpg', N'Accessories', 1, 133, CAST(N'2023-04-12T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [BasePrice], [ImageURL], [TypeProduct], [DiscountId], [InventoryQuantity], [CreatedAt], [Size]) VALUES (18, N'Bamboo Canes', N'Bamboo Canes available in size  90cm in length Protecting a new or young tree by staking it enables it too grow the strong root system it needs to secure itself in the ground. Can be used in a variety of purposes including; decorative Fencing; Plant supports; Tomatoes; Orchids; Vines; Fruits; Trees; Peas; Beans.', CAST(28.00 AS Decimal(10, 2)), CAST(25.00 AS Decimal(10, 2)), N'/images/BambooCanes.jpg', N'Accessories', 1, 20, CAST(N'2023-06-24T00:00:00.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO


CREATE TRIGGER insertUserRoles
ON Users
AFTER INSERT
AS
BEGIN
    INSERT INTO UserRoles (UserId, RoleId)
    SELECT i.id,'R02'
    FROM inserted i;
END;

