USE [ProjetB]
GO

/*DROP TABLE dbo.Estimator
GO*/

/****** Object:  Table [dbo].[Estimator]    Script Date: 11/24/2014 19:49:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Estimator](
	[Name] [varchar](255) CONSTRAINT PK_Estimator PRIMARY KEY NONCLUSTERED NOT NULL,
	[Full Name] [varchar](255) NULL,
	[Assembly] [varchar](255) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


