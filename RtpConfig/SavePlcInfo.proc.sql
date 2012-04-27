CREATE PROCEDURE [dbo].[SavePlcInfo]
	@rtpid int = 0, 
	@plcName nvarchar(20),
	@plcType nvarchar(20),
	@plcNumber int
AS
	UPDATE RtpName
	SET plcName = @plcName, plcType =@plcType, plcNumber = @plcNumber
	WHERE RtpName.rtpid = @rtpid
RETURN 0