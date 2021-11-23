CREATE PROCEDURE [dbo].[spWine_Insert]
	@Name NVARCHAR(50),
	@Buy DECIMAL,
	@Sell DECIMAL,
	@Type INT,
	@Country INT,
	@Picture VARCHAR(1024),
	@Year INT,
	@Content INT,
	@Alcohol DECIMAL,
	@Rating INT,
	@Description NVARCHAR(256)
AS
BEGIN
	INSERT
	INTO [dbo].[Wine] ([Name], [Buy], [Sell], [Type], [Country], [Picture], [Year], [Content], [Alcohol], [Rating], [Description])
	OUTPUT INSERTED.Id
	VALUES (@Name, @Buy, @Sell, @Type, @Country, @Picture, @Year, @Content, @Alcohol, @Rating, @Description);
END
