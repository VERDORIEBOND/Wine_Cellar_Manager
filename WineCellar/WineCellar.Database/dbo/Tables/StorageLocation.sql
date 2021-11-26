CREATE TABLE [dbo].[StorageLocation]
(
	[IdWine] INT NOT NULL,
	[Shelf] NVARCHAR(50) NOT NULL,
	[Row] INT NOT NULL,
	[Col] INT NOT NULL, 
    CONSTRAINT [FK_Location_ToWine] FOREIGN KEY ([IdWine]) REFERENCES [Wine]([Id]), 
    CONSTRAINT [PK_Location] PRIMARY KEY ([IdWine], [Col], [Shelf], [Row])
)
