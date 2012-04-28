CREATE PROCEDURE [dbo].[SaveShiberConfigForGroup]
	@rtpid int = 0, 
	@shibernumber1 int,
	@timeOpen1 int,
	@timeClose1 int,
	@shibernumber2 int,
	@timeOpen2 int,
	@timeClose2 int,
	@replication int = 1
AS
	UPDATE ShiberSetup
	SET ShiberSetup.timeOpen = @timeOpen1, ShiberSetup.timeClose = @timeClose1
	WHERE ShiberSetup.rtpid = @rtpid AND  ShiberSetup.shibernumber = @shibernumber1

	UPDATE ShiberSetup
	SET ShiberSetup.timeOpen = @timeOpen2, ShiberSetup.timeClose = @timeClose2
	WHERE ShiberSetup.rtpid = @rtpid AND  ShiberSetup.shibernumber = @shibernumber2


	exec  dbo.UpdateShangeStore
	   --IF @replication = 1
	   -- [RemoteDB]..[SaveShiberConfigForGroup] @rtpid,  @shibernumber1, @timeOpen1, @timeClose1, @shibernumber2, @timeOpen2, @timeClose2, 0
RETURN 0