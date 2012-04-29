CREATE PROCEDURE [dbo].[SaveSingleSequence]
	@rtpid int = 0, 
	@sequencenumber int,
	@shibernumber int,
	@replication int = 1
AS
	UPDATE SingleSequence
	SET SingleSequence.shibernumber = @shibernumber
	WHERE SingleSequence.rtpid  = @rtpid AND SingleSequence.sequencenumber = @sequencenumber

	exec  dbo.UpdateShangeStore
	   IF @replication = 1 AND EXISTS (SELECT srv.name FROM sys.servers srv WHERE srv.server_id != 0 AND srv.name Like'$(RtpConfigRemote)')
	   BEGIN TRY
	      exec [$(RtpConfigRemote)].[$(RtpConfig)].[dbo].[SaveSingleSequence] @rtpid,  @sequencenumber, @shibernumber, 0
	   END TRY
	   BEGIN CATCH
	   END CATCH;
RETURN 0