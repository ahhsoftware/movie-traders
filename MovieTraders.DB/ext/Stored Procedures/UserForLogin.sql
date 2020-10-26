--+SqlPlusRoutine
    --&Author=Alan Hyneman
    --&Comment=Retrieves password details and lock information for the user for users email.
    --&SelectType=SingleRow
--+SqlPlusRoutine
CREATE PROCEDURE [ext].[UserForLogin]
(
	--+Required
	--+Comment=Email
    @Email varchar(64)
)
AS
BEGIN
 
    SET NOCOUNT ON;
 
    SELECT
        [UserId],
        [PasswordIterations],
        [PasswordHash],
        [PasswordSalt],
        [LockDate],
        [LockReason]
    FROM
        [dbo].[User]
    WHERE
        Email = @Email;

    IF @@ROWCOUNT = 0
    BEGIN
        --+Return=NotFound
        RETURN 0;
    END;
 
    --+Return=Ok
    RETURN 1;
 
END;