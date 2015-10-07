CREATE TABLE [dbo].[Team]
(
	[TeamId] INT NOT NULL PRIMARY KEY, 
    [TeamName] VARCHAR(50) NOT NULL, 
    [DivisionId] INT NOT NULL, 
    [TeamWins] SMALLINT NOT NULL DEFAULT 0, 
    [TeamLoss] SMALLINT NOT NULL DEFAULT 0, 
    [TeamTies] SMALLINT NOT NULL DEFAULT 0,
    [TeamLogoUrl] VARCHAR(300) NOT NULL, 
    [TeamCode] VARCHAR(3) NOT NULL,
	CONSTRAINT [FK_Team_Division] FOREIGN KEY ([DivisionId]) REFERENCES [dbo].[Division]([DivisionId]),
	CONSTRAINT [UQ_Team_TeamCode] UNIQUE NONCLUSTERED ([TeamCode] ASC)
)
