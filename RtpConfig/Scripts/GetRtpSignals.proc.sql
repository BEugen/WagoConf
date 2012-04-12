CREATE PROCEDURE [dbo].[GetRtpSignals]
	@signalgroupid int = 1, 
	@channeltype int
AS
    declare @groupid int
	select @groupid = RtpSignalsGroup.signalgroup
	from RtpSignalsGroup
	where RtpSignalsGroup.id = @signalgroupid

	SELECT RtpSignals.id, RtpSignals.signaldescription, RtpSignals.signalattribute
	from RtpSignals
	where RtpSignals.signalgroup = @groupid and RtpSignals.signalcontrain = @channeltype
	order by RtpSignals.id
RETURN 0