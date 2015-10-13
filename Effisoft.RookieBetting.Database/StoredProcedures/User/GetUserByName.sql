CREATE PROCEDURE [dbo].[GetUserByName]
	@Email NVARCHAR(100)	
AS
BEGIN

	SELECT u.*
	FROM   dbo.[User] u
	WHERE  u.Email = @Email;
END