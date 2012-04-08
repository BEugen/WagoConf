CREATE PROCEDURE [dbo].[GetModule]
	@rtpid int = 0 
AS
	SELECT RtpModuls.id, RtpModuls.modulnumber, ChannelType.descript
	from RtpModuls Left Join ChannelType ON RtpModuls.modultype = ChannelType.typeid
	where RtpModuls.rtpid = @rtpid
	order by RtpModuls.modulnumber