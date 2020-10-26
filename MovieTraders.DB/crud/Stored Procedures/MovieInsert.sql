--+SqlPlusRoutine
    --&Author=Alan Hyneman
    --&Comment=Inserts a new record into the dbo.Movie table.
    --&SelectType=NonQuery
--+SqlPlusRoutine
CREATE PROCEDURE [crud].[MovieInsert]
(
    --+Comment=MovieId
    @MovieId int out,
 
    --+Required
    --+Comment=GenreId
    @GenreId tinyint,
 
    --+Required
    --+Comment=Year
	--+Range=1900,2050
    @Year int,
 
    --+Required
    --+MaxLength=64
	--+StringLength=1,64
    --+Comment=Title
    @Title varchar(64),
 
    --+Required
    --+StringLength=1,1024
    --+Comment=Link
	--+Url
    @Link varchar(1024),
 
    --+Required
    --+Comment=CreatedByUserId
    @CreatedByUserId int
)
AS
BEGIN
 
    SET NOCOUNT ON;
 
    INSERT INTO [dbo].[Movie]
    (
        [GenreId],
        [Year],
        [Title],
        [Link],
        [CreatedByUserId],
        [CreatedDate]
    )
    VALUES
    (
        @GenreId,
        @Year,
        @Title,
        @Link,
        @CreatedByUserId,
        GETDATE()
    );
 
    SET @MovieId = scope_identity();
 
    --+Return=Ok
    RETURN 1;
 
END;