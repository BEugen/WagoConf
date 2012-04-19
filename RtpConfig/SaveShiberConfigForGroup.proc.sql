CREATE PROCEDURE [dbo].[SaveShiberConfigForGroup]
	@rtpid int = 0, 
	@shibernumber1 int,
	@timeOpen1 int,
	@timeClose1 int,
	@shibernumber2 int,
	@timeOpen2 int,
	@timeClose2 int
AS
	UPDATE ShiberSetup
	SET ShiberSetup.timeOpen = @timeOpen1, ShiberSetup.timeClose = @timeClose1
	WHERE ShiberSetup.rtpid = @rtpid AND  ShiberSetup.shibernumber = @shibernumber1

	UPDATE ShiberSetup
	SET ShiberSetup.timeOpen = @timeOpen2, ShiberSetup.timeClose = @timeClose2
	WHERE ShiberSetup.rtpid = @rtpid AND  ShiberSetup.shibernumber = @shibernumber2
RETURN 0