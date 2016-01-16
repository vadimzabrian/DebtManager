/****** Object:  Table [dbo].[Users]    Script Date: 3/12/2015 1:54:16 PM ******/

CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO




/****** Object:  Table [dbo].[Payments]    Script Date: 3/12/2015 1:54:23 PM ******/
CREATE TABLE [dbo].[Payments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Reason] [nvarchar](max) NULL,
	[Payer_Id] [int] NULL,
	[Receiver_Id] [int] NULL,
 CONSTRAINT [PK_dbo.Payments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Payments_dbo.Users_Payer_Id] FOREIGN KEY([Payer_Id])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_dbo.Payments_dbo.Users_Payer_Id]
GO

ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Payments_dbo.Users_Receiver_Id] FOREIGN KEY([Receiver_Id])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_dbo.Payments_dbo.Users_Receiver_Id]
GO