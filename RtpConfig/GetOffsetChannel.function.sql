CREATE FUNCTION [dbo].[GetOffsetChannel]
(
	@rtpid int, 
	@modulnumber int,
	@channelnumber int,
	@modultype int
)
RETURNS INT
AS
BEGIN
	DECLARE @byteuse int
	DECLARE @offsetch int
	SET @offsetch = 0

	IF @modultype = 3 OR @modultype = 4
	BEGIN 
	  SET @offsetch = (@channelnumber - 1) *2
	END

	IF @modultype = 0 OR @modultype = 1
	BEGIN
	  SELECT @offsetch = COUNT(a.channelnumber)
	  FROM RtpChannel a
	  Where a.rtpid = @rtpid AND a.channeltype = @modultype AND 
	  a.modulnumber <= @modulnumber AND a.id <> ISNULL((SELECT RtpChannel.id from
	  RtpChannel  where RtpChannel.channelnumber >= @channelnumber AND RtpChannel.modulnumber = @modulnumber
	    AND RtpChannel.id = a.id AND RtpChannel.rtpid = @rtpid), -1)

	  SET @byteuse = CAST(@offsetch/8 as int)
	  SET @offsetch = @offsetch - @byteuse*8
	END



 RETURN @offsetch
END