--+SqlPlusRoutine
    --&Author=Alan Hyneman
    --&Comment=Merges dbo.UserMovie table.
    --&SelectType=NonQuery
--+SqlPlusRoutine
CREATE PROCEDURE [crud].[UserMovieMerge]
(
    --+Required
    --+Comment=UserId
    @UserId int,
 
    --+Required
    --+Comment=MovieId
    @MovieId int,
 
    --+Required
    @Formats dbo.TinyInts readonly
)
AS
BEGIN
 
    SET NOCOUNT ON;
 
    MERGE INTO [dbo].[UserMovie] target USING
	(SELECT
		@UserId AS UserId,
		@MovieId AS MovieId,
		Id AS FormatId
	FROM
		@Formats
	) AS source ([UserId], [MovieId], [FormatId])
	ON
		source.UserId = target.UserId AND
		source.MovieId = target.MovieId AND
		source.FormatId = target.FormatId
	WHEN NOT MATCHED BY TARGET THEN
	INSERT
	(
		[UserId], [MovieId], [FormatId]
	)
	VALUES
	(
		source.UserId,
		source.MovieId,
		source.FormatId
	)
	WHEN NOT MATCHED BY source AND 
		target.UserId = @UserId AND
		target.MovieId = @MovieId
	THEN DELETE;
 
    --+Return=Ok
    RETURN 1;
 
END;