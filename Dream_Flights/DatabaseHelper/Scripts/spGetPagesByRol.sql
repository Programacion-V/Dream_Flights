USE [programacion_5]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_user]    Script Date: 11/7/2022 16:06:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].GetPagesByRol
	@id_rol INT
AS
BEGIN
	SELECT * FROM page_role
	WHERE id_rol= @id_rol
END
