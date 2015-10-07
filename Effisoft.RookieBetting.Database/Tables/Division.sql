CREATE TABLE [dbo].[Division]
(
	[DivisionId] INT NOT NULL PRIMARY KEY,
	[DivisionName] VARCHAR(9) NOT NULL,
	[ConferenceId] INT NOT NULL,
	CONSTRAINT [FK_Division_Conference] FOREIGN KEY (ConferenceId) REFERENCES [dbo].[Conference]([ConferenceId])
)
