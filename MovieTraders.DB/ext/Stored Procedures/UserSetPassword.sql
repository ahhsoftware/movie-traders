--+SqlPlusRoutine
    --&Author=Alan Hyneman
    --&Comment=Updates password information for given UserId.
    --&SelectType=NonQuery
--+SqlPlusRoutine
CREATE PROCEDURE [ext].[UserSetPassword]
(
    --+Required
    @UserId int,
 
    --+Required
    @PasswordIterations int,
 
    --+Required
    --+MaxLength=16
    @PasswordSalt binary(16),
 
    --+Required
    --+MaxLength=16
    @PasswordHash binary(16)
)
AS
BEGIN
 
    SET NOCOUNT ON;
 
	UPDATE dbo.[User] SET
			PasswordIterations = @PasswordIterations,
			PasswordSalt = @PasswordSalt,
			PasswordHash = @PasswordHash
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