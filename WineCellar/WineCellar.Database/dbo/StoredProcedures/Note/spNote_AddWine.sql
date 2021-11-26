CREATE PROCEDURE [dbo].[spNote_AddWine]
	@IdWine INT,
	@IdNote INT
AS
BEGIN
	INSERT
	INTO [dbo].[Wine_Note] ([IdWine], [IdNote])
	VALUES (@IdWine, @IdNote);
END
