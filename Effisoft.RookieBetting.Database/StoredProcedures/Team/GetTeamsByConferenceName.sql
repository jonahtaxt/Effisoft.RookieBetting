CREATE PROCEDURE [dbo].[GetTeamsByConferenceName]
	@ConferenceName VARCHAR(8)
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
	INNER JOIN
		   dbo.Division b
	ON     b.DivisionId = a.DivisionId
	INNER JOIN
		   dbo.Conference c
	ON     c.ConferenceId = b.ConferenceId
	WHERE  c.ConferenceName = @ConferenceName
