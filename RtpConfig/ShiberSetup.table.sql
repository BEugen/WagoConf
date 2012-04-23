CREATE TABLE [dbo].[ShiberSetup]
(
	id int identity (1,1) not null, 
	rtpid int not null,
	shibernumber int not null,	
	reopenCountMax int not null,
	timeOpen int not null,
	timeClose int not null,
	timeAOpen int not null,
	timeAClose int not null,
	timeBetwenShiber int not null
CONSTRAINT [PK_ShiberSetup] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
