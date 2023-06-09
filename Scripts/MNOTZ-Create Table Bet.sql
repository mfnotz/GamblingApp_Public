USE [GamblinAppDb]
GO

/****** Object:  Table [dbo].[Bet]    Script Date: 16/05/2023 15:27:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Bet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[BetAmount] [int] NOT NULL,
	[BetChoice] [int] NOT NULL,
	[BetActualResult] [int] NOT NULL,
	[BetDoneAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Bet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


