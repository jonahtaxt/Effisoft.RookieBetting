CREATE PROCEDURE [dbo].[GetGameWeeks]
	@Season INT
AS
	SELECT DISTINCT [Week],
	       @Season
	FROM   dbo.Game
	WHERE  Season = @Season
