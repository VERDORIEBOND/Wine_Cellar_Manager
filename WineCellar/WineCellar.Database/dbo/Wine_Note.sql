CREATE TABLE [dbo].[Wine_Note]
(
	[IdWine] INT NOT NULL,
	[IdNote] INT NOT NULL, 
    CONSTRAINT [FK_Wine_Note_ToWine] FOREIGN KEY ([IdWine]) REFERENCES [Wine]([Id]),
    CONSTRAINT [FK_Wine_Note_ToNote] FOREIGN KEY ([IdNote]) REFERENCES [Note]([Id]), 
    CONSTRAINT [PK_Wine_Note] PRIMARY KEY ([IdWine], [IdNote])
)
