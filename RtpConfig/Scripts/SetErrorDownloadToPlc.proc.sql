CREATE PROCEDURE [dbo].[SetErrorDownloadToPlc]
	@rtpid int = 0, 
	@type int,
	@value int
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


RETURN 0