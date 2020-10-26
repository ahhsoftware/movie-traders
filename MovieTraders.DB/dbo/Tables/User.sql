CREATE TABLE [dbo].[User] (
    [UserId]             INT           IDENTITY (1, 1) NOT NULL,
    [CountyId]           SMALLINT      NOT NULL,
    [PasswordIterations] INT           NOT NULL,
    [PasswordHash]       BINARY (16)   NOT NULL,
    [PasswordSalt]       BINARY (16)   NOT NULL,
    [Phone]              VARCHAR (16)  NOT NULL,
    [Email]              VARCHAR (64)  NOT NULL,
    [Name]               VARCHAR (64)  NOT NULL,
    [LockDate]           DATETIME      NULL,
    [LockReason]         VARCHAR (256) NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_User_County] FOREIGN KEY ([CountyId]) REFERENCES [dbo].[County] ([CountyId])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_User_Phone]
    ON [dbo].[User]([Phone] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_User_Email]
    ON [dbo].[User]([Email] ASC);

