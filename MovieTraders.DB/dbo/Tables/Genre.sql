CREATE TABLE [dbo].[Genre] (
    [GenreId] TINYINT      NOT NULL,
    [Name]    VARCHAR (32) NOT NULL,
    CONSTRAINT [PK_Genre] PRIMARY KEY CLUSTERED ([GenreId] ASC)
);

