CREATE PROCEDURE [dbo].[SaveShiberSetup]
	@rtpid int = 0, 
	@shibernumber int,
	@timeOpen int,
	@timeClose int,
	@timeAOpen int,
	@timeAClose int,
	@timeBetwenShiber int,
	@reopenCountMax int
AS
	UPDATE ShiberSetup
	SET timeOpen = @timeOpen, timeClose = @timeClose, timeAOpen = @timeAOpen, timeAClose = @timeAClose,
	timeBetwenShiber = @timeBetwenShiber, reopenCountMax = @reopenCountMax
	WHERE ShiberSetup.rtpid = @rtpid AND ShiberSetup.shibernumber = @shibernumber
RETURN 0