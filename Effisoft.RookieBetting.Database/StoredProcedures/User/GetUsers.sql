CREATE PROCEDURE [dbo].[GetUsers]
AS
BEGIN

SELECT [UserId]
      ,U.[Active]
      ,U.[Name]
      ,[LastName]
      ,[SurName]
      ,[Email]
  FROM [dbo].[User] U

END