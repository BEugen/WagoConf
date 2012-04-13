CREATE FUNCTION [dbo].[GetOffsetModule]
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
	DECLARE @offsetmod int
	DECLARE @offsetdmod int

	SET @offsetmod = 0
	SET @offsetdmod = 0


	IF @modultype = 3 OR @modultype = 4
	BEGIN 
	  SELECT @offsetmod = COUNT(RtpChannel.channelnumber)*2 -- Аналоговый канал занимают 2 байта
	  FROM RtpChannel
	  WHERE RtpChannel.rtpid = @rtpid AND RtpChannel.channeltype = @modultype AND RtpChannel.modulnumber < @modulnumber
	END

	IF @modultype = 0 OR @modultype = 1
	BEGIN

	  SELECT @offsetmod = COUNT(RtpChannel.channelnumber)*2 -- Аналоговый канал занимают 2 байта
	  FROM RtpChannel
	  WHERE RtpChannel.rtpid = @rtpid AND RtpChannel.channeltype = (@modultype + 2)
	  
	  SELECT @offsetdmod = COUNT(a.channelnumber)
	  FROM RtpChannel a
	  Where a.rtpid = @rtpid AND a.channeltype = @modultype AND 
	  a.modulnumber <= @modulnumber AND a.id <> ISNULL((SELECT RtpChannel.id from
	  RtpChannel  where RtpChannel.channelnumber >= @channelnumber AND RtpChannel.modulnumber = @modulnumber
	    AND RtpChannel.id = a.id AND RtpChannel.rtpid = @rtpid), -1)

	  SET @offsetmod= @offsetmod + CAST(@offsetdmod/8 as int)
	END



 RETURN @offsetmod
END