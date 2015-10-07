CREATE PROCEDURE [dbo].[GetTeams]
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
