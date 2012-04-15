CREATE PROCEDURE [dbo].[GetCurrentShiberConfigByShiberNumber]
	@rtpid int = 0, 
	@shibernumber int
AS
	SELECT ShiberSetup.timeOpen as timeOpen, ShiberSetup.timeClose as timeClose, ShiberSetup.timeBetwenShiber as timeBetwenShiber,
		  ShiberSetup.reopenCountMax as reopenCountMax
	FROM ShiberSetup
	WHERE ShiberSetup.rtpid = @rtpid AND ShiberSetup.shibernumber = @shibernumber
RETURN 0