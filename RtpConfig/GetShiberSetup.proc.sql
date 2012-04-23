CREATE PROCEDURE [dbo].[GetShiberSetup]
	@rtpid int = 0
AS
	SELECT ShiberSetup.id, ShiberSetup.shibernumber, RtpSignalsGroup.signalgroupdescription, ShiberSetup.timeOpen, ShiberSetup.timeClose,  ShiberSetup.timeBetwenShiber, ShiberSetup.timeAOpen, ShiberSetup.timeAClose, ShiberSetup.reopenCountMax
	FROM ShiberSetup LEFT OUTER JOIN RtpSignalsGroup ON ShiberSetup.shibernumber = RtpSignalsGroup.signalattrnumber
	WHERE ShiberSetup.rtpid = @rtpid
	order by  ShiberSetup.shibernumber
RETURN 0