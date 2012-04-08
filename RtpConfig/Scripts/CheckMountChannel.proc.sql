CREATE PROCEDURE [dbo].[CheckMountChannel]
	@rtpid int = 0, 
	@modulnumber int,
	@channelnumber int
AS

	 DECLARE @shibernumber int
	 DECLARE @signalgroup int




		  select @signalgroup = RtpSignalsGroup.signalgroup, @shibernumber = RtpShibers.shibernumber
		  from (RtpChannel LEFT JOIN RtpShibers ON RtpChannel.shiberid = RtpShibers.id)
		        LEFT JOIN RtpSignalsGroup ON RtpShibers.signalgroupid = RtpSignalsGroup.id
		  where RtpChannel.channelnumber = @channelnumber and RtpChannel.rtpid = @rtpid and RtpChannel.modulnumber = @modulnumber

		  select RtpSignals.signaltype, RtpChannel.modulnumber, RtpChannel.channelnumber, RtpCommand.commandid, RtpSignals.signalcontrain, @shibernumber as shibernumber
          from ((RtpSignals LEFT OUTER JOIN (select  RtpShibers.id, RtpShibers.shibernumber, 
          RtpShibers.signalgroupid,  RtpShibers.signaltype, RtpShibers.rtpid FROM RtpShibers 
          where RtpShibers.rtpid = @rtpid and RtpShibers.shibernumber = @shibernumber) as t ON RtpSignals.id = t.signaltype) LEFT OUTER JOIN RtpChannel ON t.id = RtpChannel.shiberid)
		    LEFT OUTER JOIN RtpCommand ON RtpSignals.signalgroup = RtpCommand.signalgroup
          where RtpSignals.signalgroup = @signalgroup
         order by RtpSignals.signaltype

RETURN 0