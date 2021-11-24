CREATE PROCEDURE [dbo].[spCountry_GetAll]
AS
BEGIN
	SELECT [Id], [Name]
	FROM [dbo].[Country];
END