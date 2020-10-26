MERGE [dbo].[Genre] as target
USING (SELECT * FROM ( VALUES
	(1, N'Action'),
	(2, N'Adventure'),
	(3, N'Animation'),
	(4, N'Biography'),
	(5, N'Comedy'),
	(6, N'Crime'),
	(7, N'Documentary'),
	(8, N'Drama'),
	(9, N'Family'),
	(10, N'Fantasy'),
	(11, N'Film Noir'),
	(12, N'History'),
	(13, N'Horror'),
	(14, N'Music'),
	(15, N'Musical'),
	(16, N'Mystery'),
	(17, N'Romance'),
	(18, N'Sci-Fi'),
	(19, N'Short Film'),
	(20, N'Sport'),
	(21, N'Superhero'),
	(22, N'Thriller'),
	(23, N'War'),
	(24, N'Western')
)
AS s ([GenreId], [Name])) AS source
ON target.[GenreId] = source.[GenreId]
WHEN NOT MATCHED THEN
INSERT
(
	[GenreId],
	[Name]
)
VALUES
(
	source.[GenreId],
	source.[Name]
)
WHEN MATCHED THEN
UPDATE SET
	target.[Name] = source.[Name];