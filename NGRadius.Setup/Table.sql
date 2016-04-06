SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Sys_Log](
	[RecId] [bigint] IDENTITY(1,1) NOT NULL,
	[AddTime] [datetime] NULL,
	[Msg] [nvarchar](max) NULL,
	[LogType] [nvarchar](20) NULL,
	[CodeSoure] [nvarchar](200) NULL,
	[Flag] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sys_Log] PRIMARY KEY CLUSTERED 
(
	[RecId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[tbUsers]    Script Date: 04/06/2016 10:33:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tbUsers](
	[username] [varchar](25) NULL,
	[password] [varchar](25) NULL,
	[groups] [varchar](25) NULL,
	[addr] [varchar](255) NULL,
	[cash] [float] NULL,
	[expiry] [varchar](25) NULL,
	[others] [varchar](255) NULL,
	[method] [varchar](255) NULL,
	[billtype] [varchar](255) NULL,
	[EmpNo] [nvarchar](50) NULL,
	[EmpName] [nvarchar](50) NULL,
	[Detp] [nvarchar](50) NULL,
	[RecId] [bigint] IDENTITY(1,1) NOT NULL,
	[EQType] [nvarchar](50) NULL,
 CONSTRAINT [PK_tbUsers] PRIMARY KEY CLUSTERED 
(
	[RecId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO
