CREATE PROCEDURE [dbo].[spNote_Get]
	@Id int
AS
BEGIN
	SELECT [Id], [Name]
	FROM [dbo].[Note]
	WHERE [Id] = @Id;
END
