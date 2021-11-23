CREATE PROCEDURE [dbo].[spWine_Update]
	@Id INT,
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
	UPDATE [dbo].[Wine] SET
	[Name] = @Name,
	[Buy] = @Buy,
	[Sell] = @Sell,
	[Type] = @Type,
	[Country] = @Country,
	[Picture] = @Picture,
	[Year] = @Year,
	[Content] = @Content,
	[Alcohol] = @Alcohol,
	[Rating] = @Rating,
	[Description] = @Description
	WHERE [Id] = @Id;
END
