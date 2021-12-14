USE [LoginDB]
GO
ALTER TABLE Users ADD admin bit NOT NULL DEFAULT 0;
GO
/****** Object:  Table [dbo].[gems]    Script Date: 2021. 12. 08. 22:13:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[gems](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[colorr] [float] NULL,
	[colorg] [float] NULL,
	[colorb] [float] NULL,
	[hexcolor] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[materials]    Script Date: 2021. 12. 08. 22:13:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[materials](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[colorr] [float] NULL,
	[colorg] [float] NULL,
	[colorb] [float] NULL,
	[hexcolor] [nvarchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[models]    Script Date: 2021. 12. 08. 22:13:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[models](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](20) NULL,
	[img] [nvarchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[Delete_Gem]    Script Date: 2021. 12. 08. 22:13:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Delete_Gem]
	@Id int
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT id FROM gems WHERE id = @Id)
	BEGIN
		DELETE FROM gems WHERE id = @Id;
		SELECT 1 -- Removed
	END
	ELSE
	BEGIN
		SELECT -1 -- ID doesnt exists
	END
END

GO
/****** Object:  StoredProcedure [dbo].[Delete_Mat]    Script Date: 2021. 12. 08. 22:13:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Delete_Mat]
	@Id int
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT id FROM materials WHERE id = @Id)
	BEGIN
		DELETE FROM materials WHERE id = @Id;
		SELECT 1 -- Removed
	END
	ELSE
	BEGIN
		SELECT -1 -- ID doesnt exists
	END
END

GO
/****** Object:  StoredProcedure [dbo].[Delete_Model]    Script Date: 2021. 12. 08. 22:13:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Delete_Model]
	@Id int
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS(SELECT id FROM models WHERE id = @Id)
	BEGIN
		DELETE FROM models WHERE id = @Id;
		SELECT 1 -- Removed
	END
	ELSE
	BEGIN
		SELECT -1 -- ID doesnt exists
	END
END

GO
/****** Object:  StoredProcedure [dbo].[Insert_Gem]    Script Date: 2021. 12. 08. 22:13:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Insert_Gem]
	@Colorr Float,
	@Colorg Float,
	@Colorb Float,
	@Hexcolor NVARCHAR(10)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO [gems]
			([colorr]
			,[colorg]
			,[colorb]
			,[hexcolor])
	VALUES
			(@Colorr
			,@Colorg
			,@Colorb
			,@Hexcolor)
		
	SELECT SCOPE_IDENTITY() -- Id
END

GO
/****** Object:  StoredProcedure [dbo].[Insert_Mat]    Script Date: 2021. 12. 08. 22:13:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Insert_Mat]
	@Colorr Float,
	@Colorg Float,
	@Colorb Float,
	@Hexcolor NVARCHAR(10)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO [materials]
			([colorr]
			,[colorg]
			,[colorb]
			,[hexcolor])
	VALUES
			(@Colorr
			,@Colorg
			,@Colorb
			,@Hexcolor)
		
	SELECT SCOPE_IDENTITY() -- Id
END

GO
/****** Object:  StoredProcedure [dbo].[Insert_Model]    Script Date: 2021. 12. 08. 22:13:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Insert_Model]
	@Name NVARCHAR(20),
	@Img NVARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO [models]
			([name]
			,[img])
	VALUES
			(@Name
			,@Img)
		
	SELECT SCOPE_IDENTITY() -- Id
END

GO
