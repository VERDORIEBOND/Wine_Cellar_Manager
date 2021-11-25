CREATE PROCEDURE [dbo].[spWine_GetAll]
AS
BEGIN
	SELECT [Id], [Name], [Buy], [Sell], [Type], [Country], [Picture], [Year], [Content], [Alcohol], [Rating], [Description]
	FROM [dbo].[Wine];
END