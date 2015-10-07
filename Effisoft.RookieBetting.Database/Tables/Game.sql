CREATE TABLE [dbo].[Game]
(
	[GameId] INT NOT NULL PRIMARY KEY, 
    [GameDate] DATETIME NOT NULL, 
    [HomeTeamId] INT NOT NULL, 
    [AwayTeamId] INT NOT NULL, 
    [ScheduleId] INT NOT NULL, 
    [MondayNight] BIT NOT NULL DEFAULT 0,
	CONSTRAINT [FK_Game_Schedule] FOREIGN KEY ([ScheduleId]) REFERENCES [dbo].[Schedule]([ScheduleId]),
	CONSTRAINT [FK_Game_HomeTeam] FOREIGN KEY ([HomeTeamId]) REFERENCES [dbo].[Team]([TeamId]),
	CONSTRAINT [FK_Game_AwayTeam] FOREIGN KEY ([AwayTeamId]) REFERENCES [dbo].[Team]([TeamId])
)
