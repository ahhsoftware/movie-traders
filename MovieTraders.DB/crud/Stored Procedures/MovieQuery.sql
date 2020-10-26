--+SqlPlusRoutine
    --&Author=Alan Hyneman
    --&Comment=Queries dbo.Movie table.
    --&SelectType=MultiRow
--+SqlPlusRoutine
CREATE PROCEDURE [crud].[MovieQuery]
(
    --+Comment=Title
	--+Required
	--+StringLength=1,64
    @Title varchar(64)
 
) 
AS
BEGIN
 
    SET NOCOUNT ON;
 
    SELECT
        [MovieId],
        [GenreId],
        [Year],
        [Title],
        [Link],
        [CreatedByUserId],
        [CreatedDate]
    FROM
        [dbo].[Movie]
    WHERE
        [Title] = @Title;

    IF @@ROWCOUNT = 0
    BEGIN
        --+Return=NotFound
        RETURN 0;
    END;
 
    --+Return=Ok
    RETURN 1;
 
END;