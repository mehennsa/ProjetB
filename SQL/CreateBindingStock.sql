USE [ProjetB]
GO

/*DROP TABLE dbo.BindingStock
GO*/

/****** Object:  Table [dbo].[BindingStock]    Script Date: 11/24/2014 19:49:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[BindingStock](
	[Ticker] [varchar](255) CONSTRAINT PK_BindingStock PRIMARY KEY NONCLUSTERED NOT NULL,
	[Name] [varchar](255) NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


