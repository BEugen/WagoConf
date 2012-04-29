CREATE PROCEDURE [dbo].[SetErrorDownloadToPlc]
	@rtpid int = 0, 
	@type int,
	@value int,
	@replication int = 1
AS
 IF @type = 1
   BEGIN
	UPDATE RtpName
	SET RtpName.changehardware = @value
	WHERE RtpName.rtpid = @rtpid
   END

 IF @type = 2
  BEGIN
	UPDATE RtpName
	SET RtpName.changegroupconfig = @value
	WHERE RtpName.rtpid = @rtpid
  END

 IF @type = 3
 BEGIN
   UPDATE RtpName
	SET RtpName.changesingleconfig = @value
	WHERE RtpName.rtpid = @rtpid
 END

  IF @type = 4
  BEGIN
   UPDATE RtpName
	SET RtpName.changeshiberconfig = @value
	WHERE RtpName.rtpid = @rtpid
 END

 exec  dbo.UpdateShangeStore
	   IF @replication = 1 AND EXISTS (SELECT srv.name FROM sys.servers srv WHERE srv.server_id != 0 AND srv.name Like'$(RtpConfigRemote)')
	   BEGIN TRY
	      exec [$(RtpConfigRemote)].[$(RtpConfig)].[dbo].[SetErrorDownloadToPlc] @rtpid, @type, @value, 0
	   END TRY
	   BEGIN CATCH
	   END CATCH;
RETURN 0