CREATE PROCEDURE [dbo].[ChangeModulType]
	@rtpid int = 0, 
	@modulnumber int,
	@modultype int
AS
	 BEGIN TRANSACTION CHANGEMODUL
	  DELETE RtpShibers
	  where RtpShibers.id = (select RtpChannel.shiberid from RtpChannel where RtpChannel.modulnumber = @modulnumber and RtpChannel.rtpid = @rtpid)

	  update RtpChannel
	  set RtpChannel.shiberid = NULL, RtpChannel.channeltype = @modultype
	  where RtpChannel.modulnumber = @modulnumber and RtpChannel.rtpid = @rtpid
	  
	  update RtpModuls
	  set RtpModuls.modultype = @modultype
	  where RtpModuls.rtpid = @rtpid and RtpModuls.modulnumber = @modulnumber

	   IF @@ERROR <> 0
		 BEGIN
			ROLLBACK TRANSACTION CHANGEMODUL
		     RETURN -1 
		 END
	   ELSE
		 BEGIN
			 COMMIT TRANSACTION CHANGEMODUL
		END


RETURN 0