CREATE PROCEDURE [dbo].[spCountry_Update]
	@Id INT,
	@Name NVARCHAR(50)
AS
BEGIN
	UPDATE [dbo].[Country] SET
	[Name] = @Name
	WHERE [Id] = @Id;
END
