CREATE PROCEDURE [dbo].[SaveShiberSetup]
	@rtpid int = 0, 
	@shibernumber int,
	@timeOpen int,
	@timeClose int,
	@timeAOpen int,
	@timeAClose int,
	@timeBetwenShiber int,
	@reopenCountMax int,
	@replication int = 1
AS
	UPDATE ShiberSetup
	SET timeOpen = @timeOpen, timeClose = @timeClose, timeAOpen = @timeAOpen, timeAClose = @timeAClose,
	timeBetwenShiber = @timeBetwenShiber, reopenCountMax = @reopenCountMax
	WHERE ShiberSetup.rtpid = @rtpid AND ShiberSetup.shibernumber = @shibernumber

		exec  dbo.UpdateShangeStore
	   --IF @replication = 1
	   -- [RemoteDB]..[SaveShiberSetup] @rtpid,  @shibernumber, @timeOpen, @timeClose, @timeAOpen, @timeAClose, @timeBetwenShiber, @reopenCountMax, 0
RETURN 0