USE [programacion_5]
GO

/****** Object:  StoredProcedure [dbo].[SaveAirline]    Script Date: 28/6/2022 21:07:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_save_airline] 
	@name VARCHAR(50),
	@phone VARCHAR(50),
	@photo VARCHAR(50)
AS
BEGIN
	INSERT INTO [dbo].[Airlines]
           ([Photo]
           ,[Name]
           ,[Phone])
     VALUES
           (@photo
           ,@name
           ,@phone)
END
GO


