--+SqlPlusRoutine
    --&Author=Alan Hyneman
    --&Comment=Retrieves password details and lock information for the user for users email.
    --&SelectType=SingleRow
--+SqlPlusRoutine
CREATE PROCEDURE [ext].[UserForForgotPassword]
(
	--+Required
	--+Comment=Email
	--+Email
    @Email varchar(64),

	--+Required
	--+Comment=Phone
	--+Phone
	@Phone varchar(16)
)
AS
BEGIN
 
    SET NOCOUNT ON;
 
    SELECT
        [UserId],
		[Name],
        [LockDate],
        [LockReason]
    FROM
        [dbo].[User]
    WHERE
        Email = @Email AND
		Phone = @Phone

    IF @@ROWCOUNT = 0
    BEGIN
        --+Return=NotFound
        RETURN 0;
    END;
 
    --+Return=Ok
    RETURN 1;
 
END;