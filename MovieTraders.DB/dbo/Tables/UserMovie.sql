CREATE TABLE [dbo].[UserMovie] (
    [UserId]   INT     NOT NULL,
    [MovieId]  INT     NOT NULL,
    [FormatId] TINYINT NOT NULL,
    CONSTRAINT [FK_UserMovie_Format] FOREIGN KEY ([FormatId]) REFERENCES [dbo].[Format] ([FormatId]),
    CONSTRAINT [FK_UserMovie_Movie] FOREIGN KEY ([MovieId]) REFERENCES [dbo].[Movie] ([MovieId]),
    CONSTRAINT [FK_UserMovie_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([UserId])
);



