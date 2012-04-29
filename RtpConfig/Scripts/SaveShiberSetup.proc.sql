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
	   IF @replication = 1 AND EXISTS (SELECT srv.name FROM sys.servers srv WHERE srv.server_id != 0 AND srv.name Like'$(RtpConfigRemote)')
	   BEGIN TRY
	      exec [$(RtpConfigRemote)].[$(RtpConfig)].[dbo].[SaveShiberSetup] @rtpid,  @shibernumber, @timeOpen, @timeClose, @timeAOpen, @timeAClose, @timeBetwenShiber, @reopenCountMax, 0
	   END TRY
	   BEGIN CATCH
	   END CATCH;
RETURN 0