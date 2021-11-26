CREATE PROCEDURE [dbo].[spNote_GetAll]
AS
BEGIN
	SELECT [Id], [Name]
	FROM [dbo].[Note];
END