CREATE PROCEDURE [dbo].[GetErrorDownloadToPlc]
	@rtpid int = 0
AS
	Select  ISNULL(RtpName.changehardware, 0) as changehardware
	FROM RtpName
	WHERE RtpName.rtpid = @rtpid
RETURN 0