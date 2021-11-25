CREATE PROCEDURE [dbo].[spNote_Insert]
	@Name NVARCHAR(50)
AS
BEGIN
	INSERT
	INTO [dbo].[Note] ([Name])
	OUTPUT INSERTED.Id
	VALUES (@Name);
END
