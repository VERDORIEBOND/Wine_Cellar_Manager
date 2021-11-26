CREATE PROCEDURE [dbo].[spNote_RemoveWine]
	@IdWine INT,
	@IdNote INT
AS
BEGIN
	DELETE
	FROM [dbo].[Wine_Note]
	WHERE [IdWine] = @IdWine
	AND [IdNote] = @IdNote;
END
