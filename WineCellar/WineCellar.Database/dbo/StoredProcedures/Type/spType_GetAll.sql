CREATE PROCEDURE [dbo].[spType_GetAll]
AS
BEGIN
	SELECT [Id], [Name]
	FROM [dbo].[Type];
END