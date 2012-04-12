CREATE PROCEDURE [dbo].[GetAllModuleChannel]
   @rtpid int = 0
AS
	SELECT ChannelType.descript, RtpChannel.modulnumber, RtpChannel.channelnumber, RtpSignalsGroup.signalgroupdescription, RtpSignals.signaldescription
	   FROM (((( RtpChannel LEFT OUTER JOIN RtpModuls ON RtpChannel.modulnumber = RtpModuls.modulnumber)
	   LEFT OUTER JOIN RtpShibers ON RtpChannel.shiberid = RtpShibers.id) LEFT OUTER JOIN
	   RtpSignalsGroup ON RtpShibers.signalgroupid = RtpSignalsGroup.id) LEFT OUTER JOIN
	   RtpSignals ON RtpShibers.signaltype = RtpSignals.id)LEFT OUTER JOIN ChannelType ON RtpChannel.channeltype = ChannelType.typeid
	  WHERE RtpChannel.rtpid = @rtpid
	  order by RtpChannel.modulnumber, RtpChannel.channelnumber
RETURN 0