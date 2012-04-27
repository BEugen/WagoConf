CREATE PROCEDURE [dbo].[GetPlcInfo]
	@rtpid int = 0
AS
	SELECT RtpName.plcName, RtpName.plcType, RtpName.plcNumber
	FROM RtpName
	WHERE RtpName.rtpid = @rtpid
RETURN 0