
CREATE PROCEDURE [dbo].[ChangeCountChannel]
	@rtpid int = 0, 
	@modulnumber int,
	@channelcount int,
	@replication int = 1
AS
 DECLARE @oldchannelcount int
 DECLARE @channeltype int
 
 select @oldchannelcount = COUNT(RtpChannel.channelnumber)
 from RtpChannel
 where RtpChannel.rtpid = @rtpid and RtpChannel.modulnumber = @modulnumber
 
 select @channeltype = RtpChannel.channeltype
 from RtpChannel
 where RtpChannel.rtpid = @rtpid and RtpChannel.modulnumber = @modulnumber
 
 
		 BEGIN TRANSACTION DELMOUNT
		 
		    DELETE RtpShibers
			WHERE RtpShibers.rtpid = @rtpid and RtpShibers.id = 
			(select RtpChannel.shiberid from RtpChannel where RtpChannel.shiberid = RtpShibers.id)
			
			
			UPDATE RtpChannel
			set RtpChannel.shiberid = NULL
			WHERE RtpChannel.modulnumber = @modulnumber
			   and RtpChannel.rtpid = @rtpid

			if @oldchannelcount > @channelcount
			  BEGIN
			   DELETE RtpChannel
			   where RtpChannel.modulnumber = @modulnumber
			   and RtpChannel.rtpid = @rtpid and RtpChannel.channelnumber > (@oldchannelcount - @channelcount)
			  END
			
			if @oldchannelcount < @channelcount
			 BEGIN
			 WHILE @oldchannelcount < @channelcount
			  BEGIN
			    INSERt INTO RtpChannel(shiberid, channeltype, rtpid, channelnumber, modulnumber)
			    values (null, @channeltype, @rtpid , @oldchannelcount + 1, @modulnumber)
			    set @oldchannelcount = @oldchannelcount + 1
			  END
			 END

			   UPDATE RtpName
	           SET changehardware = 1
					   
		    IF @@ERROR <> 0
			  BEGIN
			    ROLLBACK TRANSACTION DELMOUNT
				RETURN -1
		      END
			  ELSE
			   BEGIN
			    COMMIT TRANSACTION DELMOUNT 
				
			  END

		exec  dbo.UpdateShangeStore
	    IF @replication = 1 AND EXISTS (SELECT srv.name FROM sys.servers srv WHERE srv.server_id != 0 AND srv.name Like'$(RtpConfigRemote)')
	   BEGIN TRY
	      exec [$(RtpConfigRemote)].[$(RtpConfig)].[dbo].[ChangeCountChannel] @rtpid, @modulnumber, @channelcount, 0
	   END TRY
	   BEGIN CATCH
	   END CATCH;

RETURN 0
