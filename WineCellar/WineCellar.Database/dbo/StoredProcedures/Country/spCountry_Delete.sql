CREATE PROCEDURE [dbo].[spCountry_Delete]
	@Id int
AS
BEGIN
	DELETE
	FROM [dbo].[Country]
	WHERE [Id] = @Id;
END