CREATE PROCEDURE [dbo].[ChangeModulType]
	@rtpid int = 0, 
	@modulnumber int,
	@modultype int,
	@replication int = 1
AS
	 BEGIN TRANSACTION CHANGEMODUL
	  DELETE RtpShibers
	  WHERE RtpShibers.rtpid = @rtpid and RtpShibers.id = 
			(select RtpChannel.shiberid from RtpChannel where RtpChannel.shiberid = RtpShibers.id)

	  update RtpChannel
	  set RtpChannel.shiberid = NULL, RtpChannel.channeltype = @modultype
	  where RtpChannel.modulnumber = @modulnumber and RtpChannel.rtpid = @rtpid
	  
	  update RtpModuls
	  set RtpModuls.modultype = @modultype
	  where RtpModuls.rtpid = @rtpid and RtpModuls.modulnumber = @modulnumber

	  UPDATE RtpName
	  SET changehardware = 1

	   IF @@ERROR <> 0
		 BEGIN
			ROLLBACK TRANSACTION CHANGEMODUL
		     RETURN -1 
		 END
	   ELSE
		 BEGIN
			 COMMIT TRANSACTION CHANGEMODUL
		END

		exec  dbo.UpdateShangeStore
	    IF @replication = 1 AND EXISTS (SELECT srv.name FROM sys.servers srv WHERE srv.server_id != 0 AND srv.name Like'$(RtpConfigRemote)')
	   BEGIN TRY
	      exec [$(RtpConfigRemote)].[$(RtpConfig)].[dbo].[ChangeModulType] @rtpid, @modulnumber, @modultype, 0
	   END TRY
	   BEGIN CATCH
	   END CATCH;
RETURN 0