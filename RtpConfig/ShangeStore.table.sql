CREATE TABLE [dbo].[ShangeStore]
(
	id int identity (1,1) not null, 
	datetimestore datetime,
	countchange int
CONSTRAINT [PK_ShangeStore] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
