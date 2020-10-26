CREATE TABLE [dbo].[Trade] (
    [TradeId]         INT           IDENTITY (1, 1) NOT NULL,
    [TradeStatusId]   TINYINT       NOT NULL,
    [RequestUserId]   INT           NOT NULL,
    [ResponseUserId]  INT           NOT NULL,
    [RequestDate]     DATE          NOT NULL,
    [RequestDuration] INT           NOT NULL,
    [Notes]           VARCHAR (256) NOT NULL,
    CONSTRAINT [PK_Trade] PRIMARY KEY CLUSTERED ([TradeId] ASC),
    CONSTRAINT [FK_Trade_RequestUser] FOREIGN KEY ([RequestUserId]) REFERENCES [dbo].[User] ([UserId]),
    CONSTRAINT [FK_Trade_ResponseUser] FOREIGN KEY ([ResponseUserId]) REFERENCES [dbo].[User] ([UserId]),
    CONSTRAINT [FK_Trade_TradeStatus] FOREIGN KEY ([TradeStatusId]) REFERENCES [dbo].[TradeStatus] ([TradeStatusId])
);

