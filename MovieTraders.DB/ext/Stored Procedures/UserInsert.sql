--+SqlPlusRoutine
    --&Author=Alan Hyneman
    --&Comment=Inserts a new record into the dbo.User table.
    --&SelectType=NonQuery
--+SqlPlusRoutine
CREATE PROCEDURE [ext].[UserInsert]
(
    --+Comment=UserId
    @UserId int out,
 
    --+Required
    --+Comment=CountyId
    @CountyId smallint,
 
    --+Required
    --+Comment=PasswordIterations
    @PasswordIterations int,
 
    --+Required
    --+MaxLength=16
    --+Comment=PasswordHash
    @PasswordHash binary(16),
 
    --+Required
    --+MaxLength=16
    --+Comment=PasswordSalt
    @PasswordSalt binary(16),
 
    --+Required
	--+StringLength=0,16
    --+Comment=Phone
    @Phone varchar(16),
 
    --+Required
    --+StringLength=0,64
    --+Comment=Email
    @Email varchar(64),
 
    --+Required
    --+StringLength=0,64
    --+Comment=Name
    @Name varchar(64),
 
    --+Comment=LockDate
    @LockDate datetime,
 
    --+StringLength=0,256
    --+Comment=LockReason
    @LockReason varchar(256)
)
AS
BEGIN
 
    SET NOCOUNT ON;
 
	BEGIN TRY

		INSERT INTO [dbo].[User]
		(
			[CountyId],
			[PasswordIterations],
			[PasswordHash],
			[PasswordSalt],
			[Phone],
			[Email],
			[Name],
			[LockDate],
			[LockReason]
		)
		VALUES
		(
			@CountyId,
			@PasswordIterations,
			@PasswordHash,
			@PasswordSalt,
			@Phone,
			@Email,
			@Name,
			@LockDate,
			@LockReason
		);
 
		SET @UserId = scope_identity();
 
		--+Return=Ok
		RETURN 1;

	END TRY
	BEGIN CATCH
		IF ERROR_NUMBER() = 2601
		BEGIN
			--+Return=Conflict
			RETURN 2;
		END;
		THROW;
	END CATCH;
 
END;