CREATE TABLE [dbo].[RtpSignals]
(
	id int identity (1,1) not null, 
	signaldescription nvarchar(50) not null,
	signalcontrain int not null,
	signalgroup int not null,
	signaltype int not null,
	signalattribute int not null,
CONSTRAINT [PK_RtpSignals] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
