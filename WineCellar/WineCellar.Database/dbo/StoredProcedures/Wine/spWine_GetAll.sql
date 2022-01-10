CREATE PROCEDURE [dbo].[spWine_GetAll]
AS
BEGIN
	SELECT W.[Id], W.[Name], [Buy], [Sell], [TypeId], T.[Name] AS [Type], [CountryId], C.[Name] AS [Country], [Picture], [Year], [Content], [Alcohol], [Rating], [Description], [Latitude], [Longitude]
	FROM [dbo].[Wine] as W
	LEFT JOIN [Country] as C ON (W.CountryId = C.Id)
	LEFT JOIN [Type] as T ON (W.TypeId = T.Id);
END