CREATE PROCEDURE [dbo].[GetMountForSignalsGroup]
	@rtpid int = 0, 
	@shibernumber int,
	@signalgroup int
AS
	 select RtpSignals.signaltype, RtpChannel.modulnumber, RtpChannel.channelnumber, RtpCommand.commandid, RtpSignals.signalcontrain, @shibernumber as shibernumber,
		  dbo.GetOffsetChannel(@rtpid, RtpChannel.modulnumber, RtpChannel.channelnumber, RtpSignals.signalcontrain) as offsetChannel,
		  dbo.GetOffsetModule(@rtpid, RtpChannel.modulnumber, RtpChannel.channelnumber, RtpSignals.signalcontrain) as offsetModul
		  from ((RtpSignals LEFT OUTER JOIN (select  RtpShibers.id, RtpShibers.shibernumber, 
		  RtpShibers.signalgroupid,  RtpShibers.signaltype, RtpShibers.rtpid FROM RtpShibers 
		  where RtpShibers.rtpid = @rtpid and RtpShibers.shibernumber = @shibernumber) as t ON RtpSignals.id = t.signaltype) LEFT OUTER JOIN RtpChannel ON t.id = RtpChannel.shiberid)
			LEFT OUTER JOIN RtpCommand ON RtpSignals.signalgroup = RtpCommand.signalgroup
		  where RtpSignals.signalgroup = @signalgroup
		 order by RtpSignals.signaltype
RETURN 0