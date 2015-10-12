CREATE PROCEDURE [dbo].[GetTeamStatsByTeamName]
	@TeamName VARCHAR(50)
AS
	SELECT a.TeamId,
		   a.TeamName,
		   a.TeamCode,
		   a.Season,
		   a.Wins,
		   a.Losses,
		   a.Ties
	FROM   dbo.vStats a
	WHERE  a.TeamName = @TeamName;