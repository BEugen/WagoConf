CREATE PROCEDURE [dbo].[GetErrorDownloadToPlc]
	@rtpid int = 0
AS
	Select  ISNULL(RtpName.changehardware, 0) as changehardware,
	ISNULL(RtpName.changegroupconfig, 0) as changegroupconfig,
	ISNULL(RtpName.changesingleconfig, 0) as changesingleconfig,
	ISNULL(RtpName.changeshiberconfig, 0) as changeshiberconfig
	FROM RtpName
	WHERE RtpName.rtpid = @rtpid
RETURN 0