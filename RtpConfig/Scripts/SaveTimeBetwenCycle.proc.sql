CREATE PROCEDURE [dbo].[SaveTimeBetwenCycle]
	@rtpid int = 0, 
	@timeBetwenCycle int
AS
	UPDATE CommonSetup
	SET timeBetwenCycle = @timeBetwenCycle
	WHERE CommonSetup.rtpid = @rtpid
RETURN 0