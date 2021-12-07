CREATE PROCEDURE [dbo].[spStorageLocation_DeleteAllByWine]
	@IdWine int
AS
BEGIN
	DELETE
	FROM [dbo].[StorageLocation]
	WHERE [IdWine] = @IdWine;
END