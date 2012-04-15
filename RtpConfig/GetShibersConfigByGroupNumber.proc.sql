CREATE PROCEDURE [dbo].[GetShibersConfigByGroupNumber]
	@rtpid int = 0, 
	@groupnumber int
AS
		Select GroupSequence.id, GroupSequence.sequencenumber, GroupSequence.groupnumber, ShiberGroup.timeBetwenGroupLoad,
gd1.signalgroupdescription as shiberdecription1, g1.timeOpen as timeOpen1, g1.timeClose as timeClose1, g1.timeBetwenShiber as timeBetwenShiber1,
g1.reopenCountMax as reopenCountMax1,
gd2.signalgroupdescription as shiberdecription2, g2.timeOpen as timeOpen2, g2.timeClose as timeClose2, g2.timeBetwenShiber  as timeBetwenShiber2,
g2.reopenCountMax as reopenCountMax2, CommonSetup.timeBetwenCycle, ShiberGroup.shibernumber1, ShiberGroup.shibernumber2

from (((((GroupSequence LEFT OUTER JOIN ShiberGroup ON GroupSequence.groupnumber = ShiberGroup.groupnumber)
LEFT OUTER JOIN ShiberSetup g1 ON ShiberGroup.shibernumber1 = g1.shibernumber)
LEFT OUTER JOIN ShiberSetup g2 ON ShiberGroup.shibernumber2 = g2.shibernumber)
LEFT OUTER JOIN CommonSetup ON GroupSequence.rtpid = CommonSetup.rtpid)
LEFT OUTER JOIN RtpSignalsGroup gd1 ON gd1.signalattrnumber = ShiberGroup.shibernumber1)
LEFT OUTER JOIN RtpSignalsGroup gd2 ON gd2.signalattrnumber = ShiberGroup.shibernumber2
WHERE GroupSequence.rtpid = @rtpid AND ShiberGroup.groupnumber = @groupnumber
order by GroupSequence.sequencenumber
RETURN 0