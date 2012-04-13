CREATE PROCEDURE [dbo].[DeleteModule]
	@rtpid int = 0,
	@modulnumber int
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
RETURN 0