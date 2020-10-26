CREATE TABLE [dbo].[TradeResponse] (
    [TradeResponseId] INT NOT NULL,
    [TradeId]         INT NOT NULL,
    [UserMovieId]     INT NOT NULL,
    CONSTRAINT [PK_TradeRespond] PRIMARY KEY CLUSTERED ([TradeResponseId] ASC),
    CONSTRAINT [FK_TradeResponse_Trade] FOREIGN KEY ([TradeId]) REFERENCES [dbo].[Trade] ([TradeId])
);



