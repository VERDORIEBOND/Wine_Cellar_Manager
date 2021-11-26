CREATE PROCEDURE [dbo].[spNote_GetByWine]
	@Id int
AS
BEGIN
	SELECT A.[Id], A.[Name]
	FROM [dbo].[Note] as A INNER JOIN [dbo].[Wine_Note] as B ON A.Id = B.IdNote
	WHERE B.[IdWine] = @Id;
END
