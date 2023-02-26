USE [MoneyMeDB]
GO

ALTER TABLE [dbo].[Loan] DROP CONSTRAINT [FK__Loan__CustomerID__778AC167]
GO

/****** Object:  Table [dbo].[Loan]    Script Date: 2/26/2023 6:13:01 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Loan]') AND type in (N'U'))
DROP TABLE [dbo].[Loan]
GO

/****** Object:  Table [dbo].[Loan]    Script Date: 2/26/2023 6:13:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Loan](
	[LoanID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NULL,
	[FinanceAmount] [decimal](10, 2) NULL,
	[Term] [int] NULL,
	[RepaymentsFrom] [decimal](10, 2) NULL,
	[PaymentType] [varchar](50) NULL,
	[TotalRepayments] [decimal](10, 2) NULL,
	[EstablishmentFee] [decimal](10, 2) NULL,
	[Interest] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[LoanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Loan]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO

USE [MoneyMeDB]
GO

ALTER TABLE [dbo].[Quote] DROP CONSTRAINT [FK__Quote__CustomerI__71D1E811]
GO

/****** Object:  Table [dbo].[Quote]    Script Date: 2/26/2023 6:13:48 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Quote]') AND type in (N'U'))
DROP TABLE [dbo].[Quote]
GO

/****** Object:  Table [dbo].[Quote]    Script Date: 2/26/2023 6:13:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Quote](
	[QuoteID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NULL,
	[AmountRequired] [decimal](10, 2) NULL,
	[Term] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[QuoteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Quote]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO


