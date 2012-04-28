CREATE PROCEDURE [dbo].[SavePlcInfo]
	@rtpid int = 0, 
	@plcName nvarchar(20),
	@plcType nvarchar(20),
	@plcNumber int,
	@replication int = 1
AS
	UPDATE RtpName
	SET plcName = @plcName, plcType =@plcType, plcNumber = @plcNumber
	WHERE RtpName.rtpid = @rtpid

	exec  dbo.UpdateShangeStore
	   --IF @replication = 1
	   -- [RemoteDB]..[SavePlcInfo] @rtpid,  @plcName, @plcType, @plcNumber, 0
RETURN 0