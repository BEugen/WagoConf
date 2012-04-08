CREATE PROCEDURE [dbo].[GetRtpSignals]
	@groupid int = 1,
	@channeltype int
AS
	SELECT RtpSignals.id, RtpSignals.signaldescription, RtpSignals.signalattribute
	from RtpSignals
	where RtpSignals.signalgroup = @groupid and RtpSignals.signalcontrain = @channeltype
	order by RtpSignals.id
RETURN 0 