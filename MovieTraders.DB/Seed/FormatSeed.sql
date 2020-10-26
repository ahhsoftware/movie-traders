MERGE [dbo].[Format] as target
USING (SELECT * FROM ( VALUES
(1, N'DVD'),
(2, N'Blue Ray'),
(3, N'VHS')
)AS s ([FormatId],[Name])) AS source
ON target.[FormatId] = source.[FormatId]
WHEN NOT MATCHED THEN
INSERT
(
	[FormatId],
	[Name]
)
VALUES
(
	source.[FormatId],
	source.[Name]
)
WHEN MATCHED THEN
UPDATE SET
	target.[Name] = source.[Name];