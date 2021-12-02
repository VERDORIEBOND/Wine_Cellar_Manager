CREATE PROCEDURE [dbo].[spWine_Update]
	@Id INT,
	@Name NVARCHAR(50),
	@Buy DECIMAL(10,2),
	@Sell DECIMAL(10,2),
	@TypeId INT,
	@CountryId INT,
	@Picture VARCHAR(1024),
	@Year INT,
	@Content INT,
	@Alcohol DECIMAL(10,2),
	@Rating INT,
	@Description NVARCHAR(256)
AS
BEGIN
	UPDATE [dbo].[Wine] SET
	[Name] = @Name,
	[Buy] = @Buy,
	[Sell] = @Sell,
	[TypeId] = @TypeId,
	[CountryId] = @CountryId,
	[Picture] = @Picture,
	[Year] = @Year,
	[Content] = @Content,
	[Alcohol] = @Alcohol,
	[Rating] = @Rating,
	[Description] = @Description
	WHERE [Id] = @Id;
END
