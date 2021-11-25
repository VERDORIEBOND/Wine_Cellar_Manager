CREATE PROCEDURE [dbo].[spCountry_Get]
	@Id int
AS
BEGIN
	SELECT [Id], [Name]
	FROM [dbo].[Country]
	WHERE [Id] = @Id;
END
