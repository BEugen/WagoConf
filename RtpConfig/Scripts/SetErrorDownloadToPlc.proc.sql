CREATE PROCEDURE [dbo].[SetErrorDownloadToPlc]
	@rtpid int = 0, 
	@flag int
AS
	UPDATE RtpName
	SET RtpName.changehardware = @flag
	WHERE RtpName.rtpid = @rtpid
RETURN 0