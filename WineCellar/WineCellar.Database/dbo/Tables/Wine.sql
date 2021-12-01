CREATE TABLE [dbo].[Wine]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50),
	[Buy] DECIMAL(10,2),
	[Sell] DECIMAL(10,2),
	[TypeId] INT,
	[CountryId] INT,
	[Picture] VARCHAR(1024),
	[Year] INT,
	[Content] INT,
	[Alcohol] DECIMAL(10,2),
	[Rating] INT,
	[Description] NVARCHAR(256), 
    CONSTRAINT [FK_Wine_ToCountry] FOREIGN KEY ([CountryId]) REFERENCES [Country]([Id]),
    CONSTRAINT [FK_Wine_ToType] FOREIGN KEY ([TypeId]) REFERENCES [Type]([Id])
)
