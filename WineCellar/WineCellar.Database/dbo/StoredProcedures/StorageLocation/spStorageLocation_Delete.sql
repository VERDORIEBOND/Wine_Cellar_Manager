CREATE PROCEDURE [dbo].[spStorageLocation_Delete]
	@IdWine int,
	@Shelf NVARCHAR(50),
	@Row INT,
	@Col INT
AS
BEGIN
	DELETE
	FROM [dbo].[StorageLocation]
	WHERE [IdWine] = @IdWine
	AND [Shelf] = @Shelf
	AND [Row] = @Row
	AND [Col] = @Col;
END