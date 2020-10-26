CREATE TABLE [dbo].[Movie] (
    [MovieId]         INT            IDENTITY (1, 1) NOT NULL,
    [GenreId]         TINYINT        NOT NULL,
    [Year]            INT            NOT NULL,
    [Title]           VARCHAR (64)   NOT NULL,
    [Link]            VARCHAR (1024) NOT NULL,
    [CreatedByUserId] INT            NOT NULL,
    [CreatedDate]     DATE           NOT NULL,
    CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED ([MovieId] ASC),
    CONSTRAINT [FK_Movie_Genre] FOREIGN KEY ([GenreId]) REFERENCES [dbo].[Genre] ([GenreId])
);



