CREATE PROCEDURE [dbo].[spType_Delete]
	@Id int
AS
BEGIN
	DELETE
	FROM [dbo].[Type]
	WHERE [Id] = @Id;
END