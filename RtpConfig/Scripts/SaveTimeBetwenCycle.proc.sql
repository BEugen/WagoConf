CREATE PROCEDURE [dbo].[SaveTimeBetwenCycle]
	@rtpid int = 0, 
	@timeBetwenCycle int,
	@replication int = 1
AS
	UPDATE CommonSetup
	SET timeBetwenCycle = @timeBetwenCycle
	WHERE CommonSetup.rtpid = @rtpid

	exec  dbo.UpdateShangeStore
	   IF @replication = 1 AND EXISTS (SELECT srv.name FROM sys.servers srv WHERE srv.server_id != 0 AND srv.name Like'$(RtpConfigRemote)')
	   BEGIN TRY
	      exec [$(RtpConfigRemote)].[$(RtpConfig)].[dbo].[SaveTimeBetwenCycle] @rtpid,  @timeBetwenCycle, 0
	   END TRY
	   BEGIN CATCH
	   END CATCH;
RETURN 0