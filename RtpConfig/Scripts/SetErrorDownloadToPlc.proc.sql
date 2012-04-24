CREATE PROCEDURE [dbo].[SetErrorDownloadToPlc]
	@rtpid int = 0, 
	@flag int
AS
 IF @flag = 1
   BEGIN
	UPDATE RtpName
	SET RtpName.changehardware = @flag
	WHERE RtpName.rtpid = @rtpid
   END

 IF @flag = 2
  BEGIN
	UPDATE RtpName
	SET RtpName.changegroupconfig = @flag
	WHERE RtpName.rtpid = @rtpid
  END

 IF @flag = 3
 BEGIN
   UPDATE RtpName
	SET RtpName.changesingleconfig = @flag
	WHERE RtpName.rtpid = @rtpid
 END

  IF @flag = 4
  BEGIN
   UPDATE RtpName
	SET RtpName.changeshiberconfig = @flag
	WHERE RtpName.rtpid = @rtpid
 END


RETURN 0