CREATE TABLE [dbo].[Wine]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50),
	[Buy] DECIMAL,
	[Sell] DECIMAL,
	[Type] INT,
	[Country] INT,
	[Picture] VARCHAR(1024),
	[Year] INT,
	[Content] INT,
	[Alcohol] DECIMAL,
	[Rating] INT,
	[Description] NVARCHAR(256), 
    CONSTRAINT [FK_Wine_ToCountry] FOREIGN KEY ([Country]) REFERENCES [Country]([Id]),
    CONSTRAINT [FK_Wine_ToType] FOREIGN KEY ([Type]) REFERENCES [Type]([Id])
)
