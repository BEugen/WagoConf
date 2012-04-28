CREATE PROCEDURE [dbo].[SaveSingleSequence]
	@rtpid int = 0, 
	@sequencenumber int,
	@shibernumber int,
	@replication int = 1
AS
	UPDATE SingleSequence
	SET SingleSequence.shibernumber = @shibernumber
	WHERE SingleSequence.rtpid  = @rtpid AND SingleSequence.sequencenumber = @sequencenumber

	exec  dbo.UpdateShangeStore
	   --IF @replication = 1
	   -- [RemoteDB]..[SaveSingleSequence] @rtpid,  @sequencenumber, @shibernumber, 0
RETURN 0