USE [master]
GO
/****** Object:  Database [LoginDB]    Script Date: 2021. 12. 06. 18:10:24 ******/
CREATE DATABASE [LoginDB]
GO
USE [LoginDB]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2021. 12. 06. 18:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](33) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[LastLoginDate] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[Insert_User]    Script Date: 2021. 12. 06. 18:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--Insert_User
CREATE PROCEDURE [dbo].[Insert_User]
	@Username NVARCHAR(20),
	@Password NVARCHAR(33)
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT UserId FROM Users WHERE Username = @Username)
	BEGIN
		SELECT -1 -- Username exists.
	END
	ELSE
	BEGIN
		INSERT INTO [Users]
			   ([Username]
			   ,[Password]
			   ,[CreatedDate])
		VALUES
			   (@Username
			   ,CONVERT(NVARCHAR(33), HashBytes('MD5', @Password), 2)
			   ,GETDATE())
		
		SELECT SCOPE_IDENTITY() -- UserId			   
     END
END

GO
/****** Object:  StoredProcedure [dbo].[Validate_User]    Script Date: 2021. 12. 06. 18:10:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[Validate_User]
	@Username NVARCHAR(20),
	@Password NVARCHAR(33)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @UserId INT, @LastLoginDate DATETIME
	
	SELECT @UserId = UserId, @LastLoginDate = LastLoginDate 
	FROM Users WHERE Username = @Username AND [Password] = CONVERT(NVARCHAR(33), HashBytes('MD5', @Password), 2)
	
	IF @UserId IS NOT NULL
	BEGIN
		UPDATE Users
		SET LastLoginDate =  GETDATE()
		WHERE UserId = @UserId
		SELECT @UserId [UserId] -- User Valid
	END
	ELSE
	BEGIN
		SELECT -1 -- User invalid.
	END
END
GO
USE [master]
GO
ALTER DATABASE [LoginDB] SET  READ_WRITE 
GO
