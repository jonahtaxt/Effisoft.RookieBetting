CREATE PROCEDURE [dbo].[GetGameResultsByWeek]
	@Week INT
AS
	SELECT CONVERT(VARCHAR(11), g.GameDate) as GameDate,
		   ta.TeamLogoUrl AS 'AwayTeamLogoUrl',
		   ta.TeamName AS 'AwayTeamName',
		   g.AwayScore,
		   th.TeamLogoUrl AS 'HomeTeamLogoUrl',
		   th.TeamName AS 'HomeTeamName',
		   g.HomeScore
	FROM dbo.Game g
	INNER JOIN dbo.Team ta
	ON ta.TeamId = g.AwayTeamId
	INNER JOIN dbo.Team th
	ON th.TeamId = g.HomeTeamId
	WHERE g.[week] = @Week
