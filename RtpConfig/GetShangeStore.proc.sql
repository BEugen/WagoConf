CREATE PROCEDURE [dbo].[GetShangeStore]

AS
	SELECT ShangeStore.datetimestore, ShangeStore.countchange
	FROM ShangeStore
RETURN 0