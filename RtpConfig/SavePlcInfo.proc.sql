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
	   IF @replication = 1 AND EXISTS (SELECT srv.name FROM sys.servers srv WHERE srv.server_id != 0 AND srv.name Like'$(RtpConfigRemote)')
	   BEGIN TRY
	      exec [$(RtpConfigRemote)].[$(RtpConfig)].[dbo].[SavePlcInfo] @rtpid,  @plcName, @plcType, @plcNumber, 0
	   END TRY
	   BEGIN CATCH
	   END CATCH;
RETURN 0