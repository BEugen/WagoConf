CREATE PROCEDURE [dbo].[DeleteModule]
	@rtpid int = 0,
	@modulnumber int,
	@replication int = 1
AS
	BEGIN TRANSACTION DELMODUL
	   DELETE RtpShibers
			WHERE RtpShibers.rtpid = @rtpid and RtpShibers.id = 
			(select RtpChannel.shiberid from RtpChannel where RtpChannel.shiberid = RtpShibers.id AND RtpChannel.modulnumber = @modulnumber)

	   DELETE RtpChannel
	   Where RtpChannel.rtpid = @rtpid AND RtpChannel.modulnumber = @modulnumber

	   DELETE RtpModuls
	   Where RtpModuls.rtpid = @rtpid AND RtpModuls.modulnumber = @modulnumber

	   UPDATE RtpChannel
	   SET RtpChannel.modulnumber = (RtpChannel.modulnumber - 1)
	   WHERE RtpChannel.rtpid = 0 AND RtpChannel.modulnumber > @modulnumber

	   UPDATE RtpModuls
	   SET RtpModuls.modulnumber = (RtpModuls.modulnumber - 1)
	   WHERE RtpModuls.rtpid = @rtpid AND RtpModuls.modulnumber > @modulnumber



	UPDATE RtpName
	  SET changehardware = 1

	   IF @@ERROR <> 0
		 BEGIN
			ROLLBACK TRANSACTION DELMODUL
			 RETURN -1 
		 END
	   ELSE
		 BEGIN
			 COMMIT TRANSACTION CDELMODUL
		END

		exec  dbo.UpdateShangeStore
	   IF @replication = 1 AND EXISTS (SELECT srv.name FROM sys.servers srv WHERE srv.server_id != 0 AND srv.name Like'$(RtpConfigRemote)')
	   BEGIN TRY
	      exec [$(RtpConfigRemote)].[$(RtpConfig)].[dbo].[DeleteModule] @rtpid, @modulnumber, 0
	   END TRY
	   BEGIN CATCH
	   END CATCH;
RETURN 0