SET IDENTITY_INSERT [dbo].[Compra] ON
INSERT INTO [dbo].[Compra] ([Id], [DireccionEnvio], [FechaCompra], [PrecioTotal], [UsuarioId], [TipoMetodoPago]) VALUES (1, N'Avenida Espana, 17, 4A', N'2025-10-14 10:00:00', 100, N'1', 2)
INSERT INTO [dbo].[Compra] ([Id], [DireccionEnvio], [FechaCompra], [PrecioTotal], [UsuarioId], [TipoMetodoPago]) VALUES (2, N'Calle Mayor, 12, 3B', N'2025-10-15 09:30:00', 89.9, N'2', 1)
INSERT INTO [dbo].[Compra] ([Id], [DireccionEnvio], [FechaCompra], [PrecioTotal], [UsuarioId], [TipoMetodoPago]) VALUES (3, N'Paseo de la Castellana, 89', N'2025-10-20 16:45:00', 320, N'3', 0)
INSERT INTO [dbo].[Compra] ([Id], [DireccionEnvio], [FechaCompra], [PrecioTotal], [UsuarioId], [TipoMetodoPago]) VALUES (4, N'Calle Alcala, 200', N'2025-11-05 12:10:00', 24.99, N'4', 2)
INSERT INTO [dbo].[Compra] ([Id], [DireccionEnvio], [FechaCompra], [PrecioTotal], [UsuarioId], [TipoMetodoPago]) VALUES (5, N'Avenida de America, 22, 1D', N'2025-11-28 18:00:00', 149, N'5', 1)
SET IDENTITY_INSERT [dbo].[Compra] OFF
