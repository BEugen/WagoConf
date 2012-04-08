CREATE TABLE [dbo].[RtpSignalsGroup]
(
	id int identity (1,1) not null, 
	signalgroupdescription nvarchar(50) not null,
	signalattrnumber int not null,
	signalgroup int not null,
CONSTRAINT [PK_RtpSignalsGroup] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
