SET IDENTITY_INSERT [dbo].[User] ON;

MERGE [dbo].[User] as target
USING (SELECT * FROM ( VALUES
(0, 2250, 3192, 0x01B21DA2E3F7F52DBA2B7CE475D1FBAF, 0x5F00DB98282CCB44AC3FD1C1CDD36B92, '7033472038', 'alan.h.hyneman@gmail.com', 'Alan Hyneman', null, null),
(1, 2250, 3192, 0x01B21DA2E3F7F52DBA2B7CE475D1FBAF, 0x5F00DB98282CCB44AC3FD1C1CDD36B92, '5177199557', 'joekunk@gmail.com', 'Joe Kunk', null, null)
)AS s ([UserId], [CountyId], [PasswordIterations], [PasswordHash], [PasswordSalt], [Phone], [Email], [Name], [LockDate], [LockReason])) AS source
ON target.[UserId] = source.[UserId]
WHEN NOT MATCHED THEN
INSERT
(
	[UserId],
	[CountyId],
	[PasswordIterations],
	[PasswordHash],
	[PasswordSalt],
	[Phone],
	[Email],
	[Name],
	[LockDate],
	[LockReason]
)
VALUES
(
	source.[UserId],
	source.[CountyId],
	source.[PasswordIterations],
	source.[PasswordHash],
	source.[PasswordSalt],
	source.[Phone],
	source.[Email],
	source.[Name],
	source.[LockDate],
	source.[LockReason]
)
WHEN MATCHED THEN
UPDATE SET
	target.[CountyId] = source.[CountyId],
	target.[PasswordIterations] = source.[PasswordIterations],
	target.[PasswordHash] = source.[PasswordHash],
	target.[PasswordSalt] = source.[PasswordSalt],
	target.[Phone] = source.[Phone],
	target.[Email] = source.[Email],
	target.[Name] = source.[Name],
	target.[LockDate] = source.[LockDate],
	target.[LockReason] = source.[LockReason];

SET IDENTITY_INSERT [dbo].[User] OFF;