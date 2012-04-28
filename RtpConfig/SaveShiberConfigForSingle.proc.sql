CREATE PROCEDURE [dbo].[SaveShiberConfigForSingle]
	@rtpid int = 0, 
	@shibernumber int,
	@timeOpen int,
	@timeClose int,
	@timeBetwen int,
	@replication int = 1
AS
	UPDATE ShiberSetup
	SET ShiberSetup.timeOpen = @timeOpen, ShiberSetup.timeClose = @timeClose, ShiberSetup.timeBetwenShiber = @timeBetwen
	WHERE ShiberSetup.rtpid = @rtpid AND  ShiberSetup.shibernumber = @shibernumber

	exec  dbo.UpdateShangeStore
	   --IF @replication = 1
	   -- [RemoteDB]..[SaveShiberConfigForSingle] @rtpid,  @shibernumber, @timeOpen, @timeClose, @timeBetwen, 0
RETURN 0