SET DATEFORMAT dmy;
SET IDENTITY_INSERT [dbo].[Reparacion] ON;

INSERT INTO [dbo].[Reparacion] ([Id], [FechaEntrega], [FechaRecogida], [PrecioTotal], [TipoMetodoPago], [UsuarioId]) VALUES
(1, '25/10/2025', '20/10/2025', 75.50, 1, 1),
(2, '05/11/2025', '30/10/2025', 120.00, 0, 2),
(3, '18/11/2025', '12/11/2025', 90.25, 2, 3),
(4, '02/12/2025', '26/11/2025', 45.00, 1, 4),
(5, '10/12/2025', '04/12/2025', 110.80, 1, 5);

SET IDENTITY_INSERT [dbo].[Reparacion] OFF;