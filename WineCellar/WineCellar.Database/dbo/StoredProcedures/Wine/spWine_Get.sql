CREATE PROCEDURE [dbo].[spWine_Get]
	@Id int
AS
BEGIN
	SELECT [Id], [Name], [Buy], [Sell], [Type], [Country], [Picture], [Year], [Content], [Alcohol], [Rating], [Description]
	FROM [dbo].[Wine]
	WHERE [Id] = @Id;
END
