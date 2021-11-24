CREATE PROCEDURE [dbo].[spStorageLocation_GetByWine]
	@IdWine int
AS
BEGIN
	SELECT [IdWine], [Shelf], [Row], [Col]
	FROM [dbo].[StorageLocation]
	WHERE [IdWine] = @IdWine;
END
