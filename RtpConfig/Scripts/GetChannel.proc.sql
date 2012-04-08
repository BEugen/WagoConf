CREATE PROCEDURE [dbo].[GetChannel]
	@rtpid int = 0, 
	@modulnumber int
AS
	select RtpChannel.id, RtpChannel.channelnumber, RtpChannel.channeltype, RtpChannel.shiberid, RtpSignalsGroup.id as groupid, RtpSignals.id as signalid,
	RtpSignalsGroup.signalgroupdescription, RtpSignals.signaldescription
	from ((RtpChannel Left OUTER Join RtpShibers ON RtpChannel.shiberid  =  RtpShibers.id) Left OUTER Join
	        RtpSignalsGroup ON RtpShibers.signalgroupid = RtpSignalsGroup.id) LEFT JOIN RtpSignals ON
			RtpShibers.signaltype = RtpSignals.id
	where RtpChannel.rtpid = @rtpid and RtpChannel.modulnumber = @modulnumber
RETURN 0