CREATE TABLE [dbo].[User]
(
	[UserId]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (150) NOT NULL,
    [LastName]        NVARCHAR (150) NOT NULL,
    [SurName]         NVARCHAR (150) NULL,
    [Email]           NVARCHAR (100) NOT NULL,
    [Password]        VARCHAR (100)  NULL,
    [CreationDate]    DATETIME       NOT NULL,
    [UpdateDate]      DATETIME       NOT NULL,
    [Active]    BIT       NOT NULL DEFAULT 1,
    [RoleId]          SMALLINT       NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_User_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([RoleId]),
    CONSTRAINT [UQ_User_Email] UNIQUE NONCLUSTERED ([Email] ASC)
)
