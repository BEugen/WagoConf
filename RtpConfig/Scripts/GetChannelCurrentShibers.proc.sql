CREATE PROCEDURE [dbo].[GetChannelCurrentShibers]
	@rtpid int = 0,
	@chanelid int,
	@groupid int, 
	@signalid int,
	@replication int = 1
AS
	DECLARE @shiberid int
	DECLARE @shibernumber int
	DECLARE @signalgroup int

	IF @groupid = -1 AND @signalid = - 1 AND @chanelid IS NOT NULL
	 BEGIN
		select @shiberid= RtpChannel.shiberid
		from RtpChannel
		where RtpChannel.id = @chanelid and RtpChannel.rtpid = @rtpid
		IF  @shiberid IS NOT NULL
		  BEGIN
		   BEGIN TRANSACTION DELSHIBERSMOUNT
			UPDATE RtpChannel
			set RtpChannel.shiberid = NULL
			WHERE RtpChannel.shiberid = @shiberid

			select @shibernumber = RtpShibers.shibernumber, @groupid = RtpShibers.signalgroupid
			from RtpShibers
			where RtpShibers.id = @shiberid

			SELECT @signalgroup = RtpSignalsGroup.signalgroup
			from RtpSignalsGroup
			where RtpSignalsGroup.id = @groupid

			DELETE RtpShibers
			WHERE RtpShibers.id = @shiberid

		   
			IF @@ERROR <> 0
			  BEGIN
				ROLLBACK TRANSACTION DELSHIBERSMOUNT
				RETURN -1
			  END
			  ELSE
			   BEGIN
				COMMIT TRANSACTION DELSHIBERSMOUNT 
				
			  END
		 END
	 END
	 IF @groupid IS NOT NULL AND @signalid IS NOT NULL AND @groupid <> -1 AND @signalid <> - 1 AND @chanelid IS NOT NULL
	   BEGIN
		   

		  SELECT @shibernumber = RtpSignalsGroup.signalattrnumber, @signalgroup = RtpSignalsGroup.signalgroup
		  from RtpSignalsGroup
		  where RtpSignalsGroup.id = @groupid

		  select @shiberid= RtpShibers.id
		  from RtpShibers
		  where RtpShibers.shibernumber = @shibernumber and RtpShibers.rtpid = @rtpid and RtpShibers.signaltype = @signalid

			IF  @shiberid IS NOT NULL
			  BEGIN

			  BEGIN TRANSACTION DELSHIBERSMOUNT
				UPDATE RtpChannel
				SET shiberid = NULL
				WHERE RtpChannel.shiberid = @shiberid AND RtpChannel.rtpid = @rtpid
				
				UPDATE RtpChannel
				SET shiberid = @shiberid
				WHERE RtpChannel.id = @chanelid AND RtpChannel.rtpid = @rtpid
			  
				UPDATE RtpShibers
				SET RtpShibers.signalgroupid =@groupid,  RtpShibers.signaltype = @signalid,
					RtpShibers.shibernumber = @shibernumber
				where RtpShibers.id = @shiberid
				
				IF @@ERROR <> 0
				  BEGIN
				   ROLLBACK TRANSACTION DELSHIBERSMOUNT
				   RETURN -1
				  END
				ELSE
				  BEGIN
					COMMIT TRANSACTION DELSHIBERSMOUNT 
				  END
			  END
			ELSE
			  BEGIN
			   BEGIN TRANSACTION DELSHIBERSMOUNT
				INSERT INTO RtpShibers (shibernumber, signalgroupid, signaltype, rtpid)
				VALUES( @shibernumber, @groupid, @signalid, @rtpid)

				UPDATE RtpChannel
				set RtpChannel.shiberid = @@IDENTITY
				WHERE RtpChannel.id = @chanelid and RtpChannel.rtpid = @rtpid 
				IF @@ERROR <> 0
			  BEGIN
				ROLLBACK TRANSACTION DELSHIBERSMOUNT
				RETURN -2
			  END
			  ELSE
			   BEGIN
				COMMIT TRANSACTION DELSHIBERSMOUNT 				
			  END
			END 
	   END

IF @replication <> 0
  BEGIN
   select RtpSignals.signaltype, RtpChannel.modulnumber, RtpChannel.channelnumber, RtpCommand.commandid, RtpSignals.signalcontrain, @shibernumber as shibernumber,
		  dbo.GetOffsetChannel(@rtpid, RtpChannel.modulnumber, RtpChannel.channelnumber, RtpSignals.signalcontrain) as offsetChannel,
		  dbo.GetOffsetModule(@rtpid, RtpChannel.modulnumber, RtpChannel.channelnumber,  RtpSignals.signalcontrain) as offsetModul
   from ((RtpSignals LEFT OUTER JOIN (select  RtpShibers.id, RtpShibers.shibernumber, 
		RtpShibers.signalgroupid,  RtpShibers.signaltype, RtpShibers.rtpid FROM RtpShibers 
		where RtpShibers.rtpid = @rtpid and RtpShibers.shibernumber = @shibernumber) as t ON RtpSignals.id = t.signaltype) LEFT OUTER JOIN RtpChannel ON t.id = RtpChannel.shiberid)
		LEFT OUTER JOIN RtpCommand ON RtpSignals.signalgroup = RtpCommand.signalgroup
	where RtpSignals.signalgroup = @signalgroup
   order by RtpSignals.signaltype
 END

   exec  dbo.UpdateShangeStore
	   IF @replication = 1 AND EXISTS (SELECT srv.name FROM sys.servers srv WHERE srv.server_id != 0 AND srv.name Like'$(RtpConfigRemote)')
	   BEGIN TRY
	      exec [$(RtpConfigRemote)].[$(RtpConfig)].[dbo].[GetChannelCurrentShibers] @rtpid, @chanelid, @groupid,  @signalid, 0
	   END TRY
	   BEGIN CATCH
	   END CATCH;
RETURN 0

