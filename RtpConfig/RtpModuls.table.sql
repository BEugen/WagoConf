CREATE TABLE [dbo].[RtpModuls]
(
	id int identity (1,1) not null, 
	modulnumber int not null,
	modultype int not null,
	rtpid int not null,
CONSTRAINT [PK_RtpModuls] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
