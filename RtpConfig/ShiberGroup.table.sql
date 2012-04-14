CREATE TABLE [dbo].[ShiberGroup]
(
	id int identity (1,1) not null, 
	rtpid int not null,
	groupnumber int not null,	
	shibernumber1 int not null,
	shibernumber2 int not null,
	timeBetwenGroupLoad int not null
CONSTRAINT [PK_ShiberGroup] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
