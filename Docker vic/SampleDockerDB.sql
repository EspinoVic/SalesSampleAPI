CREATE DATABASE SaleSampleAPI;
GO


USE [SaleSampleAPI]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 15/07/2023 11:20:17 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[id] [int] IDENTITY(1,1)  NOT NULL,
	[productName] [nvarchar](50) NOT NULL,
	[cost] [float] NOT NULL,
	[salesPrice] [float] NOT NULL,
	[inventoryAmount] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sale]    Script Date: 15/07/2023 11:20:17 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sale](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dtSale] [datetime] NOT NULL DEFAULT (getdate()),
	[taxesRegionId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sale_Product]    Script Date: 15/07/2023 11:20:17 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sale_Product](
	[idSale] [int] NOT NULL,
	[idProduct] [int] NOT NULL,
	[amount] [int] NOT NULL,
	[dtAdded] [datetime] NOT NULL DEFAULT (getdate())
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[TaxesRegion]    Script Date: 15/07/2023 11:20:17 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaxesRegion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[taxesRegion] [nvarchar](10) NOT NULL,
	[percentage] [int] NOT NULL
) ON [PRIMARY]
GO

--SEED DATA FOR Taxes Regions
INSERT INTO [dbo].[TaxesRegion]
           ([taxesRegion]
           ,[percentage])
     VALUES
           ('MX',15)
		   ,('US',17)
		   ,('CL',19)
GO

INSERT INTO [dbo].[Product]
           ([productName]
           ,[cost]
           ,[salesPrice]
           ,[inventoryAmount])
     VALUES
           ('maruchan',34,50,45),
		   ('Soja',52,72,67),
		   ('Halls god',76,90,89)
GO


