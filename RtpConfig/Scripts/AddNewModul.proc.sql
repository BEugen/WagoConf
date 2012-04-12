﻿CREATE PROCEDURE [dbo].[AddNewModul]
	@rtpid int = 0, 
	@channelcount int,
	@modultype int
AS
	declare @modulcount int
	declare @channelstart int

	select @modulcount = COUNT(modulnumber)
	from RtpModuls

	set @channelstart = 1
	set @modulcount = @modulcount + 1
	BEGIN Transaction InsertModule

	insert into RtpModuls( modulnumber, modultype, rtpid)
	values(@modulcount, @modultype, @rtpid)

	while @channelstart <= @channelcount
	  begin
		insert into RtpChannel(channelnumber, modulnumber, rtpid, channeltype)
		values(@channelstart, @modulcount, @rtpid, @modultype)
		set @channelstart = @channelstart + 1
	  end

	  UPDATE RtpName
	  SET changehardware = 1

	  IF @@ERROR <> 0
	   BEGIN
		 ROLLBACK TRANSACTION InsertModule
		 RETURN -1
	   END
	  ELSE
	   BEGIN
		 COMMIT TRANSACTION InsertModule	
	   END

RETURN 0