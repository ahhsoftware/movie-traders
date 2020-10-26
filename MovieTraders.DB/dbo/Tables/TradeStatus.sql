CREATE TABLE [dbo].[TradeStatus] (
    [TradeStatusId] TINYINT       NOT NULL,
    [InternalName]  VARCHAR (16)  NOT NULL,
    [Name]          VARCHAR (32)  NOT NULL,
    [Description]   VARCHAR (128) NOT NULL,
    CONSTRAINT [PK_TradeStatus] PRIMARY KEY CLUSTERED ([TradeStatusId] ASC)
);

