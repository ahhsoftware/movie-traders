--+SqlPlusRoutine
    --&Author=Alan Hyneman
    --&Comment=Queries dbo.Movie table.
    --&SelectType=MultiSet
--+SqlPlusRoutine
CREATE PROCEDURE [crud].[UserMovieQuery]
(
	--+Required
	@UserId int,

	@MovieId int
)
AS
BEGIN
 
    SET NOCOUNT ON;

	--+QueryStart=UserMovies,MultiRow
	SELECT
		[MovieId],
		[FormatId]
	FROM
		[dbo].[UserMovie]
	WHERE
		[UserId] = @UserId AND
		(@MovieId IS NULL OR [MovieId] = @MovieId);
	--+QueryEnd

	--+QueryStart=Movies,MultiRow
    SELECT DISTINCT
        m.[MovieId],
        m.[GenreId],
        m.[Year],
        m.[Title],
        m.[Link]
    FROM
        [dbo].[Movie] m INNER JOIN
		[dbo].[UserMovie] u ON u.MovieId = m.MovieId
	WHERE
		u.UserId = @UserId AND
		(@MovieId IS NULL OR m.[MovieId] = @MovieId)
    --+QueryEnd

    IF @@ROWCOUNT = 0
    BEGIN
        --+Return=NotFound
        RETURN 0;
    END;
 
    --+Return=Ok
    RETURN 1;
 
END;