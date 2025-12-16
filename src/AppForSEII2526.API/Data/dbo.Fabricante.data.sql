
USE [aspnet-AppForSEII2526.Web-660902f3-55b0-42b4-a4c6-7893d47fb56a];
GO

SET DATEFORMAT dmy;


INSERT INTO [dbo].[AspNetUsers]
    ([Id], [Nombre], [Apellido], [Telefono], [CorreoElectronico], [UserName], [NormalizedUserName],
     [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp],
     [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount])
VALUES
    (N'1', N'Carlos',   N'Gomez',     N'612345678', N'carlos.gomez@example.com', N'carlos.gomez',   N'CARLOS.GOMEZ',
     N'carlos.gomez@example.com', N'CARLOS.GOMEZ@EXAMPLE.COM', 1, NULL,
     N'DEDFC06E-3671-4B33-B513-D06998ED90DC', N'95D91088-4F78-46C0-8A11-03A5CB486F7E',
     N'612345678', 1, 0, NULL, 0, 0);

INSERT INTO [dbo].[AspNetUsers]
    ([Id], [Nombre], [Apellido], [Telefono], [CorreoElectronico], [UserName], [NormalizedUserName],
     [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp],
     [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount])
VALUES
    (N'2', N'Lucía',    N'Martínez',  N'623456789', N'lucia.martinez@example.com', N'lucia.martinez', N'LUCIA.MARTINEZ',
     N'lucia.martinez@example.com', N'LUCIA.MARTINEZ@EXAMPLE.COM', 1, NULL,
     N'6DF3314C-0B4A-4A71-BF85-8219B9ABA8A1', N'78F0A079-4B96-476F-ABE2-203D83467830',
     N'623456789', 1, 0, NULL, 0, 0);

INSERT INTO [dbo].[AspNetUsers]
    ([Id], [Nombre], [Apellido], [Telefono], [CorreoElectronico], [UserName], [NormalizedUserName],
     [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp],
     [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount])
VALUES
    (N'3', N'Andrés',   N'Ruiz',      N'634567890', N'andres.ruiz@example.com', N'andres.ruiz',    N'ANDRES.RUIZ',
     N'andres.ruiz@example.com', N'ANDRES.RUIZ@EXAMPLE.COM', 1, NULL,
     N'A2468C89-BEF7-478B-B540-D74CCCED32AE', N'206FD32A-9AEE-4E40-A34B-86E65851A3A1',
     N'634567890', 1, 0, NULL, 0, 0);

INSERT INTO [dbo].[AspNetUsers]
    ([Id], [Nombre], [Apellido], [Telefono], [CorreoElectronico], [UserName], [NormalizedUserName],
     [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp],
     [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount])
VALUES
    (N'4', N'María',    N'Fernández', N'645678901', N'maria.fernandez@example.com', N'maria.fernandez', N'MARIA.FERNANDEZ',
     N'maria.fernandez@example.com', N'MARIA.FERNANDEZ@EXAMPLE.COM', 1, NULL,
     N'6A842C22-4147-4762-94B8-D261A3889C28', N'6CB7D2F1-F02B-4465-AE90-C0A008F90714',
     N'645678901', 1, 0, NULL, 0, 0);

INSERT INTO [dbo].[AspNetUsers]
    ([Id], [Nombre], [Apellido], [Telefono], [CorreoElectronico], [UserName], [NormalizedUserName],
     [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp],
     [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount])
VALUES
    (N'5', N'Javier',   N'López',     N'656789012', N'javier.lopez@example.com', N'javier.lopez',  N'JAVIER.LOPEZ',
     N'javier.lopez@example.com', N'JAVIER.LOPEZ@EXAMPLE.COM', 1, NULL,
     N'7182935D-C65B-4A0B-9D51-85DFB386B186', N'A6CDF47C-4190-4139-A1C0-71E9BD250E27',
     N'656789012', 1, 0, NULL, 0, 0);


SET IDENTITY_INSERT [dbo].[Fabricante] ON;
INSERT INTO [dbo].[Fabricante] ([Id], [Nombre]) VALUES (1, N'Bosch');
INSERT INTO [dbo].[Fabricante] ([Id], [Nombre]) VALUES (2, N'Makita');
INSERT INTO [dbo].[Fabricante] ([Id], [Nombre]) VALUES (3, N'Stanley');
INSERT INTO [dbo].[Fabricante] ([Id], [Nombre]) VALUES (4, N'Bellota');
INSERT INTO [dbo].[Fabricante] ([Id], [Nombre]) VALUES (5, N'Black+Decker');
SET IDENTITY_INSERT [dbo].[Fabricante] OFF;



SET IDENTITY_INSERT [dbo].[Herramienta] ON;
INSERT INTO [dbo].[Herramienta]
    ([Id], [Nombre], [Material], [Precio], [TiempoReparacion], [FabricanteId])
VALUES
    (1, N'Taladro percutor Bosch GSB 13 RE', N'Acero y plástico',      89.99, 5, 1),
    (2, N'Sierra circular Makita HS7601',    N'Aluminio y acero',      120.50, 7, 2),
    (3, N'Llave inglesa Stanley 300mm',      N'Acero al cromo-vanadio', 25.75, 2, 3),
    (4, N'Martillo de carpintero Bellota',   N'Madera y acero',        15.90, 1, 4),
    (5, N'Destornillador eléctrico Black+Decker', N'Plástico y acero', 45.30, 3, 5);
SET IDENTITY_INSERT [dbo].[Herramienta] OFF;



SET IDENTITY_INSERT [dbo].[Alquiler] ON;
INSERT INTO [dbo].[Alquiler]
    ([Id], [DireccionEnvio], [FechaAlquiler], [FechaFin], [FechaInicio], [PrecioTotal], [UsuarioId], [TipoMetodoPago])
VALUES
    (1, N'Calle Mayor 12, Madrid',         '2025-10-14 00:00:00', '2025-10-20 00:00:00', '2025-10-15 00:00:00', 120.50, N'1', 1),
    (2, N'Avenida Andalucía 45, Sevilla',  '2025-11-10 00:00:00', '2025-11-17 00:00:00', '2025-11-11 00:00:00',  98.75, N'2', 0),
    (3, N'Calle del Carmen 33, Valencia',  '2025-11-25 00:00:00', '2025-11-30 00:00:00', '2025-11-26 00:00:00', 150.00, N'3', 2),
    (4, N'Paseo de Gracia 21, Barcelona',  '2025-12-05 00:00:00', '2025-12-12 00:00:00', '2025-12-06 00:00:00', 175.80, N'4', 1),
    (5, N'Plaza Mayor 9, Valladolid',      '2025-12-20 00:00:00', '2025-12-28 00:00:00', '2025-12-21 00:00:00', 210.40, N'5', 2);
SET IDENTITY_INSERT [dbo].[Alquiler] OFF;


SET IDENTITY_INSERT [dbo].[Compra] ON;
INSERT INTO [dbo].[Compra]
    ([Id], [DireccionEnvio], [FechaCompra], [PrecioTotal], [UsuarioId], [TipoMetodoPago])
VALUES
    (1, N'Avenida España, 17, 4A',   '2025-10-14 10:00:00', 100.00, N'1', 2),
    (2, N'Calle Mayor, 12, 3B',      '2025-10-15 09:30:00',  89.90, N'2', 1),
    (3, N'Paseo de la Castellana, 89','2025-10-20 16:45:00', 320.00, N'3', 0),
    (4, N'Calle Alcalá, 200',        '2025-11-05 12:10:00',  24.99, N'4', 2),
    (5, N'Avenida de América, 22, 1D','2025-11-28 18:00:00', 149.00, N'5', 1);
SET IDENTITY_INSERT [dbo].[Compra] OFF;


SET IDENTITY_INSERT [dbo].[Oferta] ON;
INSERT INTO [dbo].[Oferta]
    ([Id], [FechaFinal], [FechaInicio], [FechaOferta], [UsuarioId], [TipoMetodoPago], [TipoDirigidaOferta])
VALUES
    (1, '2025-10-31 00:00:00', '2025-10-15 00:00:00', '2025-10-20 00:00:00', N'1', 1, 1),
    (2, '2025-11-30 00:00:00', '2025-11-10 00:00:00', '2025-11-15 00:00:00', N'2', 1, 2),
    (3, '2025-12-24 00:00:00', '2025-12-05 00:00:00', '2025-12-10 00:00:00', N'2', 3, 2),
    (4, '2026-01-15 00:00:00', '2025-12-28 00:00:00', '2026-01-02 00:00:00', N'4', 3, 1),
    (5, '2026-02-06 00:00:00', '2026-01-20 00:00:00', '2026-01-25 00:00:00', N'3', 1, 2);
SET IDENTITY_INSERT [dbo].[Oferta] OFF;


SET IDENTITY_INSERT [dbo].[Reparacion] ON;
INSERT INTO [dbo].[Reparacion]
    ([Id], [FechaEntrega], [FechaRecogida], [PrecioTotal], [TipoMetodoPago], [UsuarioId])
VALUES
    (1, '25/10/2025', '20/10/2025',  75.50, 1, N'1'),
    (2, '05/11/2025', '30/10/2025', 120.00, 0, N'2'),
    (3, '18/11/2025', '12/11/2025',  90.25, 2, N'3'),
    (4, '02/12/2025', '26/11/2025',  45.00, 1, N'4'),
    (5, '10/12/2025', '04/12/2025', 110.80, 1, N'5');
SET IDENTITY_INSERT [dbo].[Reparacion] OFF;


INSERT INTO [dbo].[AlquilarItem]
    ([AlquilerId], [HerramientaId], [Cantidad], [Precio])
VALUES
    (1, 1, 2, 25.50),
    (1, 3, 1, 15.00),
    (2, 2, 3, 90.00),
    (3, 5, 1, 40.75),
    (4, 4, 2, 28.20);

INSERT INTO [dbo].[CompraItem]
    ([CompraId], [HerramientaId], [Cantidad], [Precio], [Descripcion])
VALUES
    (1, 1, 1,  89.99, N'Taladro percutor Bosch GSB 13 RE'),
    (2, 2, 1, 120.50, N'Sierra circular Makita HS7601'),
    (3, 3, 2,  25.75, N'Llave inglesa Stanley 300mm'),
    (4, 4, 3,  15.90, N'Martillo de carpintero Bellota'),
    (5, 5, 1,  45.30, N'Destornillador eléctrico Black+Decker');


INSERT INTO [dbo].[OfertaItem]
    ([OfertaId], [HerramientaId], [Porcentaje], [PrecioFinal])
VALUES
    (1, 1, 15, 76.49),
    (1, 2, 20, 96.40),
    (2, 3, 10, 23.18),
    (2, 4, 25, 11.93),
    (3, 5, 30, 31.71),
    (4, 1,  5, 85.49),
    (4, 3, 20, 20.60),
    (4, 5, 15, 38.51),
    (5, 2, 35, 78.33),
    (5, 4, 10, 14.31);
INSERT INTO [dbo].[ReparacionItem]
    ([ReparacionId], [HerramientaId], [Cantidad], [Descripcion], [Precio])
VALUES
    (1, 1, 1, N'Cambio de broca y revisión general del taladro', 25.50),
    (2, 2, 1, N'Sustitución de disco y ajuste de motor de sierra', 40.75),
    (3, 3, 2, N'Limpieza y calibración de la llave inglesa',      18.00),
    (4, 4, 1, N'Cambio de mango de martillo de carpintero',        12.90),
    (5, 5, 1, N'Sustitución de batería y comprobación eléctrica',  35.60);