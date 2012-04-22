CREATE PROCEDURE [dbo].[SaveSingleSequence]
	@rtpid int = 0, 
	@sequencenumber int,
	@shibernumber int
AS
	UPDATE SingleSequence
	SET SingleSequence.shibernumber = @shibernumber
	WHERE SingleSequence.rtpid  = @rtpid AND SingleSequence.sequencenumber = @sequencenumber
RETURN 0