CREATE PROCEDURE [dbo].[UpdateShangeStore]
	
AS
	IF NOT EXISTS(select * from ShangeStore )
	  BEGIN
	  insert into ShangeStore([datetimestore], [countchange])
	  values(GETDATE(), 1)
	  END
	ELSE
	  BEGIN
	    UPDATE ShangeStore
		SET [countchange] = [countchange] + 1, [datetimestore] = GETDATE()
	  END
RETURN 0