--+SqlPlusRoutine
    --&Author=Alan Hyneman
    --&Comment=Retrieves lock information for the user based on user id.
    --&SelectType=SingleRow
--+SqlPlusRoutine
CREATE PROCEDURE [ext].[UserLockInfo]
(
	--+Required
	--+Comment=UserId
    @UserId int
)
AS
BEGIN
 
    SET NOCOUNT ON;
 
    SELECT
        [LockDate],
        [LockReason]
    FROM
        [dbo].[User]
    WHERE
        UserId = @UserId;

    IF @@ROWCOUNT = 0
    BEGIN
        --+Return=NotFound
        RETURN 0;
    END;
 
    --+Return=Ok
    RETURN 1;
 
END;