CREATE TABLE [dbo].[State] (
    [StateId]      TINYINT      NOT NULL,
    [Abbreviation] CHAR (2)     NOT NULL,
    [Name]         VARCHAR (64) NOT NULL,
    CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED ([StateId] ASC)
);

