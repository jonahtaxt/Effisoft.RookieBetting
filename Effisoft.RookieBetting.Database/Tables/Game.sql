CREATE TABLE [dbo].[Game]
(
	[GameId] INT NOT NULL PRIMARY KEY,
    [GameDate] DATETIME NOT NULL,
	[Week] INT NOT NULL,
    [HomeTeamId] INT NOT NULL,
	[HomeScore] INT NOT NULL DEFAULT 0,
    [AwayTeamId] INT NOT NULL,
	[AwayScore] INT NOT NULL DEFAULT 0,
    [Season] INT NOT NULL, 
    [MondayNight] BIT NOT NULL DEFAULT 0,
	CONSTRAINT [FK_Game_HomeTeam] FOREIGN KEY ([HomeTeamId]) REFERENCES [dbo].[Team]([TeamId]),
	CONSTRAINT [FK_Game_AwayTeam] FOREIGN KEY ([AwayTeamId]) REFERENCES [dbo].[Team]([TeamId])
)
