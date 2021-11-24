CREATE PROCEDURE [dbo].[spNote_Update]
	@Id INT,
	@Name NVARCHAR(50)
AS
BEGIN
	UPDATE [dbo].[Note] SET
	[Name] = @Name
	WHERE [Id] = @Id;
END
