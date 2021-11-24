CREATE PROCEDURE [dbo].[spType_Update]
	@Id INT,
	@Name NVARCHAR(50)
AS
BEGIN
	UPDATE [dbo].[Type] SET
	[Name] = @Name
	WHERE [Id] = @Id;
END
