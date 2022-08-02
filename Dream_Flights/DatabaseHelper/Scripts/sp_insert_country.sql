USE [programacion_5]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_insert_country]
	@id_country INT,
	@cou_name VARCHAR(50),
	@cou_img VARCHAR(50)

AS
BEGIN
	DECLARE @id_countries INT = [dbo].[Fn_Crear_ID]('countries',0)
	INSERT INTO [dbo].[Countries]
           (id_country,cou_name,cou_img)
     VALUES
           (@id_country,@cou_name,@cou_img)
END