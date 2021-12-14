USE [LoginDB]
GO
/****** Object:  Table [dbo].[saved_jewelry]    Script Date: 2021. 12. 07. 17:29:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[saved_jewelry](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](20) NULL,
	[model] [nvarchar](20) NULL,
	[color1-r] [float] NULL,
	[color1-g] [float] NULL,
	[color1-b] [float] NULL,
	[color2-r] [float] NULL,
	[color2-g] [float] NULL,
	[color2-b] [float] NULL,
	[username] [nvarchar](20) NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[Delete_Saved_Jewelry]    Script Date: 2021. 12. 07. 17:29:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Delete_Saved_Jewelry]
	@Id int,
	@Username NVARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT id FROM saved_jewelry WHERE username = @Username AND id = @Id)
	BEGIN
		DELETE FROM saved_jewelry WHERE id = @Id;
		SELECT 1 -- Removed
	END
	ELSE
	BEGIN
		SELECT -1 -- Username & ID doesnt exists
	END
END

GO
/****** Object:  StoredProcedure [dbo].[Insert_Saved_Jewelry]    Script Date: 2021. 12. 07. 17:29:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Insert_Saved_Jewelry]
	@Name NVARCHAR(20),
	@Model NVARCHAR(20),
	@Color1r FLOAT, @Color1g FLOAT, @Color1b FLOAT,
	@Color2r FLOAT, @Color2g FLOAT, @Color2b FLOAT,
	@Username NVARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT UserId FROM Users WHERE Username = @Username)
	BEGIN
		INSERT INTO [saved_jewelry]
			   ([name]
			   ,[model]
			   ,[color1-r],[color1-g],[color1-b]
			   ,[color2-r],[color2-g],[color2-b]
			   ,[username])
		VALUES
			   (@Name
			   ,@Model
			   ,@Color1r,@Color1g,@Color1b
			   ,@Color2r,@Color2g,@Color2b
			   ,@Username)
		
		SELECT SCOPE_IDENTITY() -- Id
	END
	ELSE
	BEGIN
		SELECT -1 -- Username doesnt exists
	END
END

GO
