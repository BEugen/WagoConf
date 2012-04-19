CREATE PROCEDURE [dbo].[SaveGroupConfig]
	@rtpid int = 0, 
	@groupnumber int,
	@shibernumber1 int,
	@shibernumber2 int,
	@timeBetwenLoadGroup int
AS
	UPDATE ShiberGroup
	SET ShiberGroup.shibernumber1 = @shibernumber1, ShiberGroup.shibernumber2 = @shibernumber2,
	   ShiberGroup.timeBetwenGroupLoad = @timeBetwenLoadGroup
	WHERE ShiberGroup.rtpid = @rtpid AND ShiberGroup.groupnumber = @groupnumber

RETURN 0