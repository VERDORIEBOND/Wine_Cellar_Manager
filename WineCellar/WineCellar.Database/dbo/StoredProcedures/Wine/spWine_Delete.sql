CREATE PROCEDURE [dbo].[spWine_Delete]
	@Id int
AS
BEGIN
	DELETE
	FROM [dbo].[Wine]
	WHERE [Id] = @Id;
END