SET IDENTITY_INSERT [dbo].[Fabricante] ON
INSERT INTO [dbo].[Fabricante] ([Id], [Nombre]) VALUES (1, N'Bosch')
INSERT INTO [dbo].[Fabricante] ([Id], [Nombre]) VALUES (2, N'Makita')
INSERT INTO [dbo].[Fabricante] ([Id], [Nombre]) VALUES (3, N'Stanley')
INSERT INTO [dbo].[Fabricante] ([Id], [Nombre]) VALUES (4, N'Bellota')
INSERT INTO [dbo].[Fabricante] ([Id], [Nombre]) VALUES (5, N'Black+Decker')
SET IDENTITY_INSERT [dbo].[Fabricante] OFF


SET IDENTITY_INSERT [dbo].[Herramienta] ON
INSERT INTO [dbo].[Herramienta] ([Id], [Nombre], [Material], [Precio], [TiempoReparacion], [FabricanteId]) VALUES (1, N'Taladro percutor Bosch GSB 13 RE', N'Acero y plástico', 89.99, 5, 1)
INSERT INTO [dbo].[Herramienta] ([Id], [Nombre], [Material], [Precio], [TiempoReparacion], [FabricanteId]) VALUES (2, N'Sierra circular Makita HS7601', N'Aluminio y acero', 120.5, 7, 2)
INSERT INTO [dbo].[Herramienta] ([Id], [Nombre], [Material], [Precio], [TiempoReparacion], [FabricanteId]) VALUES (3, N'Llave inglesa Stanley 300mm', N'Acero al cromo-vanadio', 25.75, 2, 3)
INSERT INTO [dbo].[Herramienta] ([Id], [Nombre], [Material], [Precio], [TiempoReparacion], [FabricanteId]) VALUES (4, N'Martillo de carpintero Bellota', N'Madera y acero', 15.9, 1, 4)
INSERT INTO [dbo].[Herramienta] ([Id], [Nombre], [Material], [Precio], [TiempoReparacion], [FabricanteId]) VALUES (5, N'Destornillador eléctrico Black+Decker', N'Plástico y acero', 45.3, 3, 5)
SET IDENTITY_INSERT [dbo].[Herramienta] OFF
