CREATE PROCEDURE [dbo].[GetSingleShiberSetup]
	@rtpid int = 0
AS
	Select SingleSequence.id, SingleSequence.sequencenumber, SingleSequence.shibernumber, RtpSignalsGroup.signalgroupdescription, ShiberSetup.timeOpen, ShiberSetup.timeClose, ShiberSetup.timeBetwenShiber,
           ShiberSetup.reopenCountMax
    from (SingleSequence LEFT OUTER JOIN ShiberSetup ON SingleSequence.shibernumber = ShiberSetup.shibernumber)
          LEFT OUTER JOIN RtpSignalsGroup ON RtpSignalsGroup.signalattrnumber = SingleSequence.shibernumber
    WHERE SingleSequence.rtpid = @rtpid
    order by SingleSequence.sequencenumber
RETURN 0