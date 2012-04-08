CREATE PROCEDURE [dbo].[GetModulType]

AS
	SELECT ChannelType.id, ChannelType.descript
	from ChannelType
	order by ChannelType.typeid
RETURN 0