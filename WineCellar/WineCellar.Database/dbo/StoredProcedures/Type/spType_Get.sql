CREATE PROCEDURE [dbo].[spType_Get]
	@Id int
AS
BEGIN
	SELECT [Id], [Name]
	FROM [dbo].[Type]
	WHERE [Id] = @Id;
END
