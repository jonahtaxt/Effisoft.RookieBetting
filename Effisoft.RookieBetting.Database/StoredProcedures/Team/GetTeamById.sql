CREATE PROCEDURE [dbo].[GetTeamById]
	@TeamId INT
AS
	SELECT a.TeamId,
	       a.TeamName,
		   a.DivisionId,
		   a.TeamWins,
		   a.TeamLoss,
		   a.TeamTies,
		   a.TeamLogoUrl,
		   a.TeamCode
	FROM   dbo.Team a
	WHERE  a.TeamId = @TeamId