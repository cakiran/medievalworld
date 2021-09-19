USE [MedievalWorld]
GO

--Note: Create an empty database named MedievalWorld before running this script. johndoe user's password is password

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Characters](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[HitPoints] [int] NOT NULL,
	[Strength] [int] NOT NULL,
	[Defense] [int] NOT NULL,
	[Intelligence] [int] NOT NULL,
	[Class] [int] NOT NULL,
	[UserId] [int] NULL,
	[WeaponId] [int] NULL,
	[Fights] [int] NOT NULL,
	[Victories] [int] NOT NULL,
	[Defeats] [int] NOT NULL,
 CONSTRAINT [PK_Characters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CharacterSkills](
	[CharacterId] [int] NOT NULL,
	[SkillId] [int] NOT NULL,
 CONSTRAINT [PK_CharacterSkills] PRIMARY KEY CLUSTERED 
(
	[CharacterId] ASC,
	[SkillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FightAttackDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OpponentId] [int] NOT NULL,
	[OpponentName] [nvarchar](max) NULL,
	[OpponentHP] [int] NOT NULL,
	[AttckerHP] [int] NOT NULL,
	[LogText] [nvarchar](max) NULL,
	[Winner] [nvarchar](max) NULL,
	[Loser] [nvarchar](max) NULL,
	[WinnerId] [int] NOT NULL,
 CONSTRAINT [PK_FightAttackDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skills](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Damage] [int] NOT NULL,
 CONSTRAINT [PK_Skills] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NULL,
	[PasswordHash] [varbinary](max) NULL,
	[PasswordSalt] [varbinary](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Weapons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Damage] [int] NOT NULL,
 CONSTRAINT [PK_Weapons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Characters] ON 
GO
INSERT [dbo].[Characters] ([Id], [Name], [HitPoints], [Strength], [Defense], [Intelligence], [Class], [UserId], [WeaponId], [Fights], [Victories], [Defeats]) VALUES (13, N'Legolas', 100, 20, 20, 15, 1, 12, 1, 2, 2, 0)
GO
INSERT [dbo].[Characters] ([Id], [Name], [HitPoints], [Strength], [Defense], [Intelligence], [Class], [UserId], [WeaponId], [Fights], [Victories], [Defeats]) VALUES (14, N'Gandalf', 100, 20, 20, 15, 2, 12, 2, 2, 1, 1)
GO
INSERT [dbo].[Characters] ([Id], [Name], [HitPoints], [Strength], [Defense], [Intelligence], [Class], [UserId], [WeaponId], [Fights], [Victories], [Defeats]) VALUES (15, N'Aragorn', 100, 20, 20, 15, 3, 12, 1, 1, 0, 1)
GO
INSERT [dbo].[Characters] ([Id], [Name], [HitPoints], [Strength], [Defense], [Intelligence], [Class], [UserId], [WeaponId], [Fights], [Victories], [Defeats]) VALUES (16, N'Frodo', 100, 20, 20, 15, 4, 12, 2, 1, 0, 1)
GO
SET IDENTITY_INSERT [dbo].[Characters] OFF
GO
INSERT [dbo].[CharacterSkills] ([CharacterId], [SkillId]) VALUES (13, 1)
GO
INSERT [dbo].[CharacterSkills] ([CharacterId], [SkillId]) VALUES (14, 2)
GO
INSERT [dbo].[CharacterSkills] ([CharacterId], [SkillId]) VALUES (15, 3)
GO
INSERT [dbo].[CharacterSkills] ([CharacterId], [SkillId]) VALUES (16, 4)
GO
SET IDENTITY_INSERT [dbo].[Skills] ON 
GO
INSERT [dbo].[Skills] ([Id], [Name], [Damage]) VALUES (1, N'Fire ball', 10)
GO
INSERT [dbo].[Skills] ([Id], [Name], [Damage]) VALUES (2, N'Ice ball', 10)
GO
INSERT [dbo].[Skills] ([Id], [Name], [Damage]) VALUES (3, N'Thunder Strike', 10)
GO
INSERT [dbo].[Skills] ([Id], [Name], [Damage]) VALUES (4, N'Lightning Flasher', 10)
GO
SET IDENTITY_INSERT [dbo].[Skills] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([Id], [Username], [PasswordHash], [PasswordSalt]) VALUES (12, N'johndoe', 0x36709B7262D09FED37B55972E8796EECFB44D930354BDDBBFDE53A2061B64749E55E606589F8C2028124228BACBB98FF2AC3D4571AD617262531261BC3094C06, 0xE7404BFD8FACD88023BDB61126C19D7A3B5143391A74C239FBD7D00190FAFB006EC81DCC820F3DA4330C3EDB01D182A40EDC557BBD35DC70AD4D151EA06BA0BC06600E5620F7D3A12E6DC885A5B62FC294037A15B76E438611060B00BC6A325327AC07C0E4CD0314C32FBDEA041AB6C7646F71ECFE0C1187B5D0964E24AD556D)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[Weapons] ON 
GO
INSERT [dbo].[Weapons] ([Id], [Name], [Damage]) VALUES (1, N'Sword', 20)
GO
INSERT [dbo].[Weapons] ([Id], [Name], [Damage]) VALUES (2, N'Spear', 20)
GO
INSERT [dbo].[Weapons] ([Id], [Name], [Damage]) VALUES (3, N'Double Sword', 10)
GO
INSERT [dbo].[Weapons] ([Id], [Name], [Damage]) VALUES (4, N'Bow & Arrow', 9)
GO
INSERT [dbo].[Weapons] ([Id], [Name], [Damage]) VALUES (5, N'Maze', 9)
GO
INSERT [dbo].[Weapons] ([Id], [Name], [Damage]) VALUES (6, N'Wand', 9)
GO
INSERT [dbo].[Weapons] ([Id], [Name], [Damage]) VALUES (7, N'Battle Axe', 9)
GO
SET IDENTITY_INSERT [dbo].[Weapons] OFF
GO
ALTER TABLE [dbo].[Characters]  WITH CHECK ADD  CONSTRAINT [FK_Characters_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Characters] CHECK CONSTRAINT [FK_Characters_Users_UserId]
GO
ALTER TABLE [dbo].[Characters]  WITH CHECK ADD  CONSTRAINT [FK_Characters_Weapons_WeaponId] FOREIGN KEY([WeaponId])
REFERENCES [dbo].[Weapons] ([Id])
GO
ALTER TABLE [dbo].[Characters] CHECK CONSTRAINT [FK_Characters_Weapons_WeaponId]
GO
ALTER TABLE [dbo].[CharacterSkills]  WITH CHECK ADD  CONSTRAINT [FK_CharacterSkills_Characters_CharacterId] FOREIGN KEY([CharacterId])
REFERENCES [dbo].[Characters] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CharacterSkills] CHECK CONSTRAINT [FK_CharacterSkills_Characters_CharacterId]
GO
ALTER TABLE [dbo].[CharacterSkills]  WITH CHECK ADD  CONSTRAINT [FK_CharacterSkills_Skills_SkillId] FOREIGN KEY([SkillId])
REFERENCES [dbo].[Skills] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CharacterSkills] CHECK CONSTRAINT [FK_CharacterSkills_Skills_SkillId]
GO
