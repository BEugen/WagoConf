CREATE TABLE [dbo].[RtpName]
(
	id int identity (1,1) not null, 
	rtpdescr nvarchar(50) not null,
	rtpid int not null,
CONSTRAINT [PK_RtpName] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
