USE [ProjetB]
GO

/*DROP TABLE dbo.DataEstimator
GO*/

/****** Object:  Table [dbo].[DataEstimator]    Script Date: 11/24/2014 19:50:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DataEstimator](
	[Date] [datetime] NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Ticker] [varchar](255) NOT NULL,
	[Value] [varchar](255) NULL,
	CONSTRAINT PK_DataEstimator PRIMARY KEY (Date,Name, Ticker)
)

GO

SET ANSI_PADDING OFF
GO


