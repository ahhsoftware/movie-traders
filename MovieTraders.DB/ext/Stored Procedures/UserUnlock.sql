--+SqlPlusRoutine
    --&Author=Alan Hyneman
    --&Comment=Clears the lock reason and lock date for given UserId.
    --&SelectType=NonQuery
--+SqlPlusRoutine
CREATE PROCEDURE [ext].[UserUnlock]
(
    --+Required
    @UserId int
)
AS
BEGIN
 
    SET NOCOUNT ON;
 
    UPDATE dbo.[User] SET
        LockDate = NULL,
        LockReason = NULL
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