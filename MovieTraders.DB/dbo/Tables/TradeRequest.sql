CREATE TABLE [dbo].[TradeRequest] (
    [TradeRequestId] INT NOT NULL,
    [TradeId]        INT NOT NULL,
    [UserMovieId]    INT NOT NULL,
    CONSTRAINT [PK_TradeRequest] PRIMARY KEY CLUSTERED ([TradeRequestId] ASC),
    CONSTRAINT [FK_TradeRequest_Trade] FOREIGN KEY ([TradeRequestId]) REFERENCES [dbo].[Trade] ([TradeId])
);



