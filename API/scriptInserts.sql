USE [dbAcheoOnibus]
GO
SET IDENTITY_INSERT [dbo].[tblOnibus] ON 

INSERT [dbo].[tblOnibus] ([idOnibus], [placa], [latitude], [longitude], [numeroOnibus], [velocidade], [sentidoViagem]) VALUES (1, N'JKG-3050', N'-15.793457', N'-47.883468', 123456, 62.1, 1)
INSERT [dbo].[tblOnibus] ([idOnibus], [placa], [latitude], [longitude], [numeroOnibus], [velocidade], [sentidoViagem]) VALUES (3, N'GTR-4031', N'-15.793857', N'-47.883468', 456789, 80, 2)
INSERT [dbo].[tblOnibus] ([idOnibus], [placa], [latitude], [longitude], [numeroOnibus], [velocidade], [sentidoViagem]) VALUES (4, N'RTE-8070', N'-15.793257', N'-47.883268', 147852, 40, 1)
SET IDENTITY_INSERT [dbo].[tblOnibus] OFF
SET IDENTITY_INSERT [dbo].[tblItinerario] ON 

INSERT [dbo].[tblItinerario] ([idItinerario], [tarifa], [numero]) VALUES (1, 3.0000, N'0.809')
INSERT [dbo].[tblItinerario] ([idItinerario], [tarifa], [numero]) VALUES (2, 2.0000, N'0.107')
INSERT [dbo].[tblItinerario] ([idItinerario], [tarifa], [numero]) VALUES (3, 2.0000, N'0.172')
INSERT [dbo].[tblItinerario] ([idItinerario], [tarifa], [numero]) VALUES (5, 3.0000, N'0.301')
SET IDENTITY_INSERT [dbo].[tblItinerario] OFF
SET IDENTITY_INSERT [dbo].[tblOnibusItinerario] ON 

INSERT [dbo].[tblOnibusItinerario] ([idOnibusItinerario], [data], [idOnibusFK], [idItinerarioFK]) VALUES (1, CAST(0x133A0B00 AS Date), 1, 1)
INSERT [dbo].[tblOnibusItinerario] ([idOnibusItinerario], [data], [idOnibusFK], [idItinerarioFK]) VALUES (8, CAST(0x133A0B00 AS Date), 3, 1)
INSERT [dbo].[tblOnibusItinerario] ([idOnibusItinerario], [data], [idOnibusFK], [idItinerarioFK]) VALUES (9, CAST(0x133A0B00 AS Date), 4, 2)
SET IDENTITY_INSERT [dbo].[tblOnibusItinerario] OFF
SET IDENTITY_INSERT [dbo].[tblViagem] ON 

INSERT [dbo].[tblViagem] ([idViagem], [sentidoViagem], [destino], [origem], [idItinerarioFK]) VALUES (1, 1, N'Rodoviária', N'Recanto das Emas', 1)
INSERT [dbo].[tblViagem] ([idViagem], [sentidoViagem], [destino], [origem], [idItinerarioFK]) VALUES (2, 2, N'Recanto das Emas', N'Rodoviária', 1)
SET IDENTITY_INSERT [dbo].[tblViagem] OFF

UPDATE tblOnibusItinerario SET data = GETDATE() + 2;