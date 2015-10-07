CREATE TABLE [dbo].[GameBet]
(
	[GameBetId] INT NOT NULL PRIMARY KEY, 
    [UserId] INT NOT NULL, 
    [GameId] INT NOT NULL, 
    [HomeWins] BIT NOT NULL, 
    [HomeScore] SMALLINT NOT NULL DEFAULT 0, 
    [AwayScore] SMALLINT NOT NULL DEFAULT 0,
	CONSTRAINT [FK_GameBet_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([UserId]),
	CONSTRAINT [FK_GameBet_Game] FOREIGN KEY ([GameId]) REFERENCES [dbo].[Game]([GameId])
)
