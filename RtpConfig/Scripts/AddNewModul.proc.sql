CREATE PROCEDURE [dbo].[AddNewModul]
	@rtpid int = 0, 
	@channelcount int,
	@modultype int,
	@replication int = 1
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


	   exec  dbo.UpdateShangeStore

	   IF @replication = 1 AND EXISTS (SELECT srv.name FROM sys.servers srv WHERE srv.server_id != 0 AND srv.name Like'$(RtpConfigRemote)')
	   BEGIN TRY
	      exec [$(RtpConfigRemote)].[$(RtpConfig)].[dbo].[AddNewModul] @rtpid, @channelcount, @modultype, 0
	   END TRY
	   BEGIN CATCH
	   END CATCH;
	   
RETURN 0