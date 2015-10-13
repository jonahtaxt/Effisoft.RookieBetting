CREATE PROCEDURE [dbo].[GetRoleByUser]
	@UserId INT
AS
BEGIN
	SELECT [Role].*
	FROM [User]
	INNER JOIN [Role]
	ON [User].RoleId = [Role].RoleId
	WHERE UserId = @UserId
END
