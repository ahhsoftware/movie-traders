--+SqlPlusRoutine
    --&Author=Alan Hyneman
    --&Comment=Sets lock date and lock reason for given UserId - lock date is automatically assigned.
    --&SelectType=NonQuery
--+SqlPlusRoutine
CREATE PROCEDURE [ext].[UserLock]
(
    --+Required
    @UserId int,
 
	--+Required
    --+MaxLength=256
	--+Display=Lock Reason, Lock Reason
    @LockReason varchar(256)
)
AS
BEGIN
 
    SET NOCOUNT ON;
 
    UPDATE dbo.[User] SET
        LockDate = GetDate(),
        LockReason = @LockReason
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