use [AddressBook_Service]

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE InsertData

	@ID int,
	@Firstname varchar(15),
	@Lastname varchar(15),
	@Address varchar(50),
	@City varchar(15),
	@State varchar(30),
	@Zip Bigint,
	@PhoneNumber Bigint,
	@Email varchar(50),
	@Type varchar(20)
AS
SET XACT_ABORT on;
SET NOCOUNT ON;
BEGIN
BEGIN TRY
BEGIN TRANSACTION;
	SET NOCOUNT ON;
	DECLARE @new_identity INTEGER = 0;
	DECLARE @result bit = 0;
	INSERT INTO employee_payroll(Firstname, Lastname, Address, City, State, Zip, PhoneNumber, Email, Type)
	VALUES (@Firstname, @Lastname, @Address, @City, @State, @Zip, @PhoneNumber, @Email, @Type)
	SELECT @new_identity = @@IDENTITY;
	COMMIT TRANSACTION
	SET @result = 1;
	RETURN @result;
	END TRY
	BEGIN CATCH

	IF(XACT_STATE()) = -1
		BEGIN
		PRINT
		'Transaction is uncommitable' + ' Rolling back transaction'
		ROLLBACK TRANSACTION;
		RETURN @result;		
		END
	ELSE IF(XACT_STATE()) = 1
		BEGIN
		PRINT
		'Transaction is commitable' + ' Commiting back transaction'
		COMMIT TRANSACTION;
		SET @result = 1;
	    RETURN @result;
	END;
	END CATCH
END
GO
