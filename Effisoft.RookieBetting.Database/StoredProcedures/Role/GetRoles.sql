CREATE PROCEDURE [dbo].[GetRoles]
	
AS
	SELECT r.RoleId,
	       r.Name
	FROM   dbo.[Role] r;