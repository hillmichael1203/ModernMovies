/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
INSERT [dbo].[AccountType] ([TypeID], [TypeName]) VALUES (0, N'Customer')
GO
INSERT [dbo].[AccountType] ([TypeID], [TypeName]) VALUES (1, N'Employee')
GO
INSERT [dbo].[AccountType] ([TypeID], [TypeName]) VALUES (2, N'Administrator')
GO
SET IDENTITY_INSERT [dbo].[Person] ON 
GO
INSERT [dbo].[Person] ([UserID], [Name], [Email], [Password], [PhoneNumber], [TypeID], [LastLoginTime]) VALUES (1, N'Test', N'test@test.test', N'test', N'1234567890', 2, CAST(N'2024-03-27T14:28:27.000' AS DateTime))
GO
INSERT [dbo].[Person] ([UserID], [Name], [Email], [Password], [PhoneNumber], [TypeID], [LastLoginTime]) VALUES (2, N'bazinga', N'baz@ing.a', N'T*gJ3Kjv''-b$f6A', N'000-000-0000', 2, CAST(N'2024-03-27T14:55:36.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Person] OFF
GO
INSERT [dbo].[Rating] ([RatingID], [RatingName]) VALUES (0, N'G')
GO
INSERT [dbo].[Rating] ([RatingID], [RatingName]) VALUES (1, N'PG')
GO
INSERT [dbo].[Rating] ([RatingID], [RatingName]) VALUES (2, N'PG-13')
GO
INSERT [dbo].[Rating] ([RatingID], [RatingName]) VALUES (3, N'R')
GO
INSERT [dbo].[Rating] ([RatingID], [RatingName]) VALUES (4, N'NC-17')
GO
INSERT [dbo].[Genre] ([GenreID], [Genre]) VALUES (0, N'Action')
GO
INSERT [dbo].[Genre] ([GenreID], [Genre]) VALUES (1, N'Comedy')
GO
INSERT [dbo].[Genre] ([GenreID], [Genre]) VALUES (2, N'Documentary')
GO
INSERT [dbo].[Genre] ([GenreID], [Genre]) VALUES (3, N'Drama')
GO
INSERT [dbo].[Genre] ([GenreID], [Genre]) VALUES (4, N'Family')
GO
INSERT [dbo].[Genre] ([GenreID], [Genre]) VALUES (5, N'Fantasy')
GO
INSERT [dbo].[Genre] ([GenreID], [Genre]) VALUES (6, N'Horror')
GO
INSERT [dbo].[Genre] ([GenreID], [Genre]) VALUES (7, N'Musical')
GO
INSERT [dbo].[Genre] ([GenreID], [Genre]) VALUES (8, N'Romance')
GO
