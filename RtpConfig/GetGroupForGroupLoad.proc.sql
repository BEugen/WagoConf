CREATE PROCEDURE [dbo].[GetGroupForGroupLoad]
	@rtpid int = 0
AS
	SELECT GroupSequence.groupnumber
	From GroupSequence
	where GroupSequence.rtpid = @rtpid
RETURN 0