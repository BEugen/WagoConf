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
	   IF @replication = 1 AND EXISTS (SELECT srv.name FROM sys.servers srv WHERE srv.server_id != 0 AND srv.name Like'$(RtpConfigRemote)')
	   BEGIN TRY
	      exec [$(RtpConfigRemote)].[$(RtpConfig)].[dbo].[SaveShiberConfigForSingle] @rtpid,  @shibernumber, @timeOpen, @timeClose, @timeBetwen, 0
	   END TRY
	   BEGIN CATCH
	   END CATCH;
RETURN 0