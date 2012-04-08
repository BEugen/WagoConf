CREATE PROCEDURE [dbo].[GetRtpSignalGroups]

AS
	SELECT RtpSignalsGroup.id, RtpSignalsGroup.signalattrnumber,
	       RtpSignalsGroup.signalgroup, RtpSignalsGroup.signalgroupdescription
	from RtpSignalsGroup
	order by RtpSignalsGroup.id
RETURN 0