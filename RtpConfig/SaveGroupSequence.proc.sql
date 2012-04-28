CREATE PROCEDURE [dbo].[SaveGroupSequence]
	@rtpid int = 0, 
	@sequencenumber int,
	@groupnumber int,
	@replication int = 1
AS
	UPDATE GroupSequence
	SET GroupSequence.groupnumber = @groupnumber
	WHERE GroupSequence.rtpid = @rtpid AND GroupSequence.sequencenumber = @sequencenumber

	exec  dbo.UpdateShangeStore
	   --IF @replication = 1
	   -- [RemoteDB]..[SaveGroupSequence] @rtpid,  @sequencenumber, @groupnumber, 0
RETURN 0