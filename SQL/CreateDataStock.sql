USE [ProjetB]
GO

/*DROP TABLE dbo.DataStock
GO*/

/****** Object:  Table [dbo].[DataStock]    Script Date: 11/24/2014 19:49:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DataStock](
	[Date] [datetime] NOT NULL,
	[Ticker] [varchar](255) NOT NULL,
	[Open] [varchar](255) NULL,
	[Close] [varchar](255) NULL,
	[High] [varchar](255) NULL,
	[Low] [varchar](255) NULL,
	[Volume] [varchar](255) NULL,
	[Adjusted_close] [varchar](255) NULL,
	CONSTRAINT PK_DataStock PRIMARY KEY (Date, Ticker)	
)

GO

SET ANSI_PADDING OFF
GO


