CREATE TABLE [dbo].[ChannelType]
(
	id int identity (1,1) not null, 
	descript nvarchar(2) not null,
	typeid int not null
CONSTRAINT [PK_ChannelType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]