USE [library]
GO
/****** Object:  Table [dbo].[authors]    Script Date: 3/2/2016 4:47:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[authors](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[book_authors]    Script Date: 3/2/2016 4:47:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[book_authors](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[author_id] [int] NULL,
	[book_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[books]    Script Date: 3/2/2016 4:47:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[books](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](255) NULL,
	[duedate] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[checkouts]    Script Date: 3/2/2016 4:47:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[checkouts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[patron_id] [int] NULL,
	[copies_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[copies]    Script Date: 3/2/2016 4:47:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[copies](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[number_of] [int] NULL,
	[book_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[patrons]    Script Date: 3/2/2016 4:47:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[patrons](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[book_authors] ON

INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (1, 1, 1)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (2, 3, 3)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (3, 3, 4)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (4, 4, 5)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (5, 6, 7)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (7, 7, 9)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (10, 10, 13)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (13, 13, 17)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (16, 16, 21)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (19, 20, 25)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (22, 25, 29)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (26, 32, 35)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (27, 32, 36)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (29, 39, 39)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (30, 39, 40)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (31, 44, 41)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (32, 46, 43)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (33, 46, 44)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (34, 51, 45)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (35, 53, 47)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (36, 53, 48)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (37, 58, 49)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (38, 60, 51)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (39, 60, 52)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (40, 65, 53)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (41, 67, 55)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (42, 67, 56)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (43, 72, 57)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (44, 74, 59)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (45, 74, 60)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (46, 79, 61)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (47, 81, 63)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (48, 81, 64)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (6, 6, 8)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (8, 9, 11)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (9, 9, 12)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (11, 12, 15)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (12, 12, 16)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (14, 15, 19)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (15, 15, 20)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (17, 18, 23)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (18, 18, 24)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (20, 22, 27)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (21, 22, 28)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (23, 27, 31)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (24, 27, 32)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (25, 30, 33)
INSERT [dbo].[book_authors] ([id], [author_id], [book_id]) VALUES (28, 37, 37)
SET IDENTITY_INSERT [dbo].[book_authors] OFF
