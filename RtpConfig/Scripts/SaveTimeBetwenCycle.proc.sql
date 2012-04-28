CREATE PROCEDURE [dbo].[SaveTimeBetwenCycle]
	@rtpid int = 0, 
	@timeBetwenCycle int,
	@replication int = 1
AS
	UPDATE CommonSetup
	SET timeBetwenCycle = @timeBetwenCycle
	WHERE CommonSetup.rtpid = @rtpid

	exec  dbo.UpdateShangeStore
	   --IF @replication = 1
	   -- [RemoteDB]..[SaveTimeBetwenCycle] @rtpid,  @timeBetwenCycle, 0
RETURN 0