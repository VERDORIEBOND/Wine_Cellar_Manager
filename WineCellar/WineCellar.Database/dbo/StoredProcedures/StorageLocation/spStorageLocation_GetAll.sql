CREATE PROCEDURE [dbo].[spStorageLocation_GetAll]
AS
BEGIN
	SELECT [IdWine], [Shelf], [Row], [Col]
	FROM [dbo].[StorageLocation];
END