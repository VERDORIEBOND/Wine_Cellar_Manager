CREATE PROCEDURE [dbo].[spCountry_Insert]
	@Name NVARCHAR(50)
AS
BEGIN
	INSERT
	INTO [dbo].[Country] ([Name])
	OUTPUT INSERTED.Id
	VALUES (@Name);
END
