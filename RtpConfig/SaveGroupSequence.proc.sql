CREATE PROCEDURE [dbo].[SaveGroupSequence]
	@rtpid int = 0, 
	@sequencenumber int,
	@groupnumber int
AS
	UPDATE GroupSequence
	SET GroupSequence.groupnumber = @groupnumber
	WHERE GroupSequence.rtpid = @rtpid AND GroupSequence.sequencenumber = @sequencenumber
RETURN 0