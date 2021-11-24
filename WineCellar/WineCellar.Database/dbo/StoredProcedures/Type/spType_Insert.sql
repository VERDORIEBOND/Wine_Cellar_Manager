CREATE PROCEDURE [dbo].[spType_Insert]
	@Name NVARCHAR(50)
AS
BEGIN
	INSERT
	INTO [dbo].[Type] ([Name])
	OUTPUT INSERTED.Id
	VALUES (@Name);
END
