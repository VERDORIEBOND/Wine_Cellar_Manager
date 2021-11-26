CREATE PROCEDURE [dbo].[spStorageLocation_Insert]
	@IdWine int,
	@Shelf NVARCHAR(50),
	@Row INT,
	@Col INT
AS
BEGIN
	INSERT
	INTO [dbo].[StorageLocation] ([IdWine], [Shelf], [Row], [Col])
	VALUES (@IdWine, @Shelf, @Row, @Col);
END
