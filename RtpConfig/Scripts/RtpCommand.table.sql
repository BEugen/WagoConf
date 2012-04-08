CREATE TABLE [dbo].[RtpCommand]
(
	id int identity (1,1) not null, 
	signalgroup int not null,
	commandid int not null,
CONSTRAINT [PK_RtpCommand] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
