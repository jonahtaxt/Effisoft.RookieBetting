INSERT INTO dbo.Conference VALUES (1, 'National') 
INSERT INTO dbo.Conference VALUES (2, 'American') 

INSERT INTO dbo.Division VALUES (1,'AFC East', 2) 
INSERT INTO dbo.Division VALUES (2,'AFC North', 2) 
INSERT INTO dbo.Division VALUES (3,'AFC South', 2) 
INSERT INTO dbo.Division VALUES (4,'AFC West', 2) 

INSERT INTO dbo.Division VALUES (5,'NFC East', 1) 
INSERT INTO dbo.Division VALUES (6,'NFC North', 1) 
INSERT INTO dbo.Division VALUES (7,'NFC South', 1) 
INSERT INTO dbo.Division VALUES (8,'NFC West', 1) 

DECLARE @DivisionId INT
DECLARE @TeamId INT

SET @DivisionId = 1
SET @TeamId = 1

INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'New England Patriots', @DivisionId, 'NE.png', 'NE') 
SET @TeamId = @TeamId + 1
INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'New York Jets', @DivisionId, 'NYJ.png', 'NYJ') 
SET @TeamId = @TeamId + 1
INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'Bufallo Bills', @DivisionId, 'BUF.png', 'BUF') 
SET @TeamId = @TeamId + 1
INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'Miami Dolphins', @DivisionId, 'MIA.png', 'MIA') 
SET @TeamId = @TeamId + 1

SET @DivisionId = 2

INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'Cincinnati Bengals', @DivisionId, 'CIN.png', 'CIN') 
SET @TeamId = @TeamId + 1
INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'Pittsburgh Steelers', @DivisionId, 'PIT.png', 'PIT') 
SET @TeamId = @TeamId + 1
INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'Baltimore Ravens', @DivisionId, 'BAL.png', 'BAL') 
SET @TeamId = @TeamId + 1
INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'Cleveland Browns', @DivisionId, 'CLE.png', 'CLE') 
SET @TeamId = @TeamId + 1

SET @DivisionId = 3

INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'Indianapolis Colts', @DivisionId, 'IND.png', 'IND') 
SET @TeamId = @TeamId + 1
INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'Tennessee Titans', @DivisionId, 'TEN.png', 'TEN') 
SET @TeamId = @TeamId + 1
INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'Jacksonville Jaguars', @DivisionId, 'JAC.png', 'JAC') 
SET @TeamId = @TeamId + 1
INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'Houston Texans', @DivisionId, 'HOU.png', 'HOU') 
SET @TeamId = @TeamId + 1

SET @DivisionId = 4

INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'Denver Broncos', @DivisionId, 'DEN.png', 'DEN') 
SET @TeamId = @TeamId + 1
INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'Oakland Raiders', @DivisionId, 'OAK.png', 'OAK') 
SET @TeamId = @TeamId + 1
INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'San Die Chargers', @DivisionId, 'SD.png', 'SD') 
SET @TeamId = @TeamId + 1
INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'Kansas City Chiefs', @DivisionId, 'KC.png', 'KC') 
SET @TeamId = @TeamId + 1

SET @DivisionId = 5

INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'Dallas Cowboys', @DivisionId, 'DAL.png', 'DAL') 
SET @TeamId = @TeamId + 1
INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'New York Giants', @DivisionId, 'NYG.png', 'NYG') 
SET @TeamId = @TeamId + 1
INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'Washington Redskins', @DivisionId, 'WAS.png', 'WAS') 
SET @TeamId = @TeamId + 1
INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'Philadelphia Eagles', @DivisionId, 'PHI.png', 'PHI') 
SET @TeamId = @TeamId + 1

SET @DivisionId = 6

INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'Green Bay Packers', @DivisionId, 'GB.png', 'GB') 
SET @TeamId = @TeamId + 1
INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'Minnesota Vikings', @DivisionId, 'MIN.png', 'MIN') 
SET @TeamId = @TeamId + 1
INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'Chica Bears', @DivisionId, 'CHI.png', 'CHI') 
SET @TeamId = @TeamId + 1
INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'Detroit Lions', @DivisionId, 'DET.png', 'DET') 
SET @TeamId = @TeamId + 1

SET @DivisionId = 7

INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'Carolina Panthers', @DivisionId, 'CAR.png', 'CAR') 
SET @TeamId = @TeamId + 1
INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'Atlanta Falcons', @DivisionId, 'ATL.png', 'ATL') 
SET @TeamId = @TeamId + 1
INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'Tampa Bay Buccaneers', @DivisionId, 'TB.png', 'TB') 
SET @TeamId = @TeamId + 1
INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'New Orleans Saints', @DivisionId, 'NO.png', 'NO') 
SET @TeamId = @TeamId + 1

SET @DivisionId = 8

INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'Arizona Cardinals', @DivisionId, 'ARI.png', 'ARI') 
SET @TeamId = @TeamId + 1
INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'St. Louis Rams', @DivisionId, 'STL.png', 'STL') 
SET @TeamId = @TeamId + 1
INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'Seattle Seahawks', @DivisionId, 'SEA.png', 'SEA') 
SET @TeamId = @TeamId + 1
INSERT INTO dbo.Team (TeamId, TeamName, DivisionId, TeamLogoUrl, TeamCode) VALUES (@TeamId, 'San Francisco 49ers', @DivisionId, 'SF.png', 'SF') 