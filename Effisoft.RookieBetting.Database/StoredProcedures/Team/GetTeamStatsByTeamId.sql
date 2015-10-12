CREATE PROCEDURE [dbo].[GetTeamStatsByTeamId]
	@TeamId INT
AS
	SELECT a.TeamId,
		   a.TeamName,
		   a.TeamCode,
		   a.Season,
		   a.Wins,
		   a.Losses,
		   a.Ties
	FROM   dbo.vStats a
	WHERE  a.TeamId = @TeamId;