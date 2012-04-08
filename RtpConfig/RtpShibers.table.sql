CREATE TABLE [dbo].[RtpShibers]
(
	id int not null identity(1,1),
	shibernumber int not null,
	signalgroupid int,
	signaltype int not null,
	rtpid int  not null,
CONSTRAINT [PK_RtpShibers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]