CREATE TABLE [dbo].[County] (
    [CountyId] SMALLINT     NOT NULL,
    [StateId]  TINYINT      NOT NULL,
    [Name]     VARCHAR (32) NOT NULL,
    CONSTRAINT [PK_County] PRIMARY KEY CLUSTERED ([CountyId] ASC),
    CONSTRAINT [FK_County_State] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([StateId])
);

