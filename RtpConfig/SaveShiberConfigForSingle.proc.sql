CREATE PROCEDURE [dbo].[SaveShiberConfigForSingle]
	@rtpid int = 0, 
	@shibernumber int,
	@timeOpen int,
	@timeClose int,
	@timeBetwen int
AS
	UPDATE ShiberSetup
	SET ShiberSetup.timeOpen = @timeOpen, ShiberSetup.timeClose = @timeClose, ShiberSetup.timeBetwenShiber = @timeBetwen
	WHERE ShiberSetup.rtpid = @rtpid AND  ShiberSetup.shibernumber = @shibernumber
RETURN 0