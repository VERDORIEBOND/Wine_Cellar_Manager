CREATE PROCEDURE [dbo].[spWine_Insert]
	@Name NVARCHAR(50),
	@Buy DECIMAL(10,2),
	@Sell DECIMAL(10,2),
	@TypeId INT,
	@CountryId INT,
	@Picture VARBINARY(MAX),
	@Year INT,
	@Content INT,
	@Alcohol DECIMAL(10,2),
	@Rating INT,
	@Description NVARCHAR(256),
	@Latitude FLOAT,
	@Longitude FLOAT
AS
BEGIN
	INSERT
	INTO [dbo].[Wine] ([Name], [Buy], [Sell], [TypeId], [CountryId], [Picture], [Year], [Content], [Alcohol], [Rating], [Description], [Latitude], [Longitude])
	OUTPUT INSERTED.Id
	VALUES (@Name, @Buy, @Sell, @TypeId, @CountryId, @Picture, @Year, @Content, @Alcohol, @Rating, @Description, @Latitude, @Longitude);
END
