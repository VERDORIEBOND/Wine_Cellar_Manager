CREATE PROCEDURE [dbo].[spNote_Delete]
	@Id int
AS
BEGIN
	DELETE
	FROM [dbo].[Note]
	WHERE [Id] = @Id;
END