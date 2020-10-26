MERGE [dbo].[TradeStatus] as target
USING (SELECT * FROM ( VALUES
(1, N'Initiated', N'Initiated', N'Requesting user created a trade'),
(2, N'Notified', N'Notification Sent', N'Notification sent to response user'),
(3, N'Accepted', N'Accepted', N'Response user selected movies and accepted trade'),
(4, N'Rejected', N'Rejected', N'Response user rejected trade'),
(5, N'Completed', N'Completed', N'The trade was completed and movies exchanged'),
(6, N'Reversed', N'Reversed', N'The movies have been returned to each party')
)AS s ([TradeStatusId], [InternalName], [Name], [Description])) AS source
ON target.[TradeStatusId] = source.[TradeStatusId]
WHEN NOT MATCHED THEN
INSERT
(
	[TradeStatusId],
	[InternalName],
	[Name],
	[Description]
)
VALUES
(
	source.[TradeStatusId],
	source.[InternalName],
	source.[Name],
	source.[Description]
)
WHEN MATCHED THEN
UPDATE SET
	target.[InternalName] = source.[InternalName],
	target.[Name] = source.[Name],
	target.[Description] = source.[Description];