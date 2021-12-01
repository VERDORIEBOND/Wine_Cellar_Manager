CREATE PROCEDURE [dbo].[spWine_Insert]
	@Name NVARCHAR(50),
	@Buy DECIMAL(10,2),
	@Sell DECIMAL(10,2),
	@Type INT,
	@Country INT,
	@Picture VARCHAR(1024),
	@Year INT,
	@Content INT,
	@Alcohol DECIMAL(10,2),
	@Rating INT,
	@Description NVARCHAR(256)
AS
BEGIN
	INSERT
	INTO [dbo].[Wine] ([Name], [Buy], [Sell], [TypeId], [CountryId], [Picture], [Year], [Content], [Alcohol], [Rating], [Description])
	OUTPUT INSERTED.Id
	VALUES (@Name, @Buy, @Sell, @Type, @Country, @Picture, @Year, @Content, @Alcohol, @Rating, @Description);
END
