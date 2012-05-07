CREATE PROCEDURE [dbo].[GetGroupForGroupLoad]
	@rtpid int = 0
AS
	SELECT ShiberGroup.groupnumber
	From ShiberGroup
	where ShiberGroup.rtpid = @rtpid
RETURN 0