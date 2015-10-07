CREATE PROCEDURE [dbo].[GetTeamsByDivisionName]
	@DivisionName VARCHAR(9)
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
	WHERE  b.DivisionName = @DivisionName
