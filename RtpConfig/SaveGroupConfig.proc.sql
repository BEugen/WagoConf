CREATE PROCEDURE [dbo].[SaveGroupConfig]
	@rtpid int = 0, 
	@groupnumber int,
	@shibernumber1 int,
	@shibernumber2 int,
	@timeBetwenLoadGroup int,
	@replication int = 1
AS
	UPDATE ShiberGroup
	SET ShiberGroup.shibernumber1 = @shibernumber1, ShiberGroup.shibernumber2 = @shibernumber2,
	   ShiberGroup.timeBetwenGroupLoad = @timeBetwenLoadGroup
	WHERE ShiberGroup.rtpid = @rtpid AND ShiberGroup.groupnumber = @groupnumber

	exec  dbo.UpdateShangeStore
	   IF @replication = 1 AND EXISTS (SELECT srv.name FROM sys.servers srv WHERE srv.server_id != 0 AND srv.name Like'$(RtpConfigRemote)')
	   BEGIN TRY
	      exec [$(RtpConfigRemote)].[$(RtpConfig)].[dbo].[SaveGroupConfig] @rtpid, @groupnumber, @shibernumber1, @shibernumber2, @timeBetwenLoadGroup, 0
	   END TRY
	   BEGIN CATCH
	   END CATCH;
RETURN 0