CREATE VIEW [dbo].[vStats]
	AS  SELECT t.TeamId,
	           t.TeamName,
			   t.TeamCode,
			   d.DivisionId,
			   d.DivisionName,
			   g.Season,
			   SUM(CASE WHEN (CASE (CASE t.TeamId
						WHEN g.HomeTeamId THEN 1
						WHEN g.AwayTeamId THEN 2
					 END)
				  WHEN 1 THEN g.HomeScore
				  WHEN 2 THEN g.AwayScore
			   END) > 
			   (CASE (CASE t.TeamId
						WHEN g.HomeTeamId THEN 1
						WHEN g.AwayTeamId THEN 2
					 END)
				  WHEN 1 THEN g.AwayScore
				  WHEN 2 THEN g.HomeScore
			   END) THEN 1
			   ELSE 0 END) AS Wins,
			   SUM(CASE WHEN (CASE (CASE t.TeamId
						WHEN g.HomeTeamId THEN 1
						WHEN g.AwayTeamId THEN 2
					 END)
				  WHEN 1 THEN g.HomeScore
				  WHEN 2 THEN g.AwayScore
			   END) < 
			   (CASE (CASE t.TeamId
						WHEN g.HomeTeamId THEN 1
						WHEN g.AwayTeamId THEN 2
					 END)
				  WHEN 1 THEN g.AwayScore
				  WHEN 2 THEN g.HomeScore
			   END) THEN 1
			   ELSE 0 END) AS Losses,
			   SUM(CASE WHEN (CASE (CASE t.TeamId
						WHEN g.HomeTeamId THEN 1
						WHEN g.AwayTeamId THEN 2
					 END)
				  WHEN 1 THEN g.HomeScore
				  WHEN 2 THEN g.AwayScore
			   END) = 
			   (CASE (CASE t.TeamId
						WHEN g.HomeTeamId THEN 1
						WHEN g.AwayTeamId THEN 2
					 END)
				  WHEN 1 THEN g.AwayScore
				  WHEN 2 THEN g.HomeScore
			   END) THEN 1
			   ELSE 0 END) AS Ties
		FROM   dbo.Team t
		INNER JOIN
			dbo.Game g
		ON     (g.HomeTeamId = t.TeamId OR g.AwayTeamId = t.TeamId)
		INNER JOIN
			dbo.Division d
		ON     (d.DivisionId = t.DivisionId)
		WHERE g.Season = 2015
		AND   g.GameDate <= GETDATE()
		GROUP BY t.TeamId,
		         t.TeamName,
				 t.TeamCode,
				 d.DivisionId,
				 d.DivisionName,
				 g.Season
