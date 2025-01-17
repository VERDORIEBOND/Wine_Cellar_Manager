﻿CREATE PROCEDURE [dbo].[spWine_Update]
	@Id INT,
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
	[Description] = @Description,
	[Latitude] = @Latitude,
	[Longitude] = @Longitude
	WHERE [Id] = @Id;
END
