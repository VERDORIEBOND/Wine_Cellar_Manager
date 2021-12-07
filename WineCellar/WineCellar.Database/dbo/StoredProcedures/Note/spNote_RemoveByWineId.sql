CREATE PROCEDURE [dbo].[spNote_RemoveByWineId]
	@IdWine INT
AS
BEGIN
	DELETE
	FROM [dbo].[Wine_Note]
	WHERE [IdWine] = @IdWine;
END
