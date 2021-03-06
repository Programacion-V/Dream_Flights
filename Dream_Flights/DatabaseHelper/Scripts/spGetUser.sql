USE [programacion_5]
GO
/****** Object:  StoredProcedure [dbo].[sp_get_user]    Script Date: 5/7/2022 21:11:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sp_get_user]
	@email varchar(50),
	@password varchar(50)
AS
BEGIN
	SELECT p.id_person,p.per_name,p.per_first_name,p.per_last_name,p.per_email,p.per_img,r.rol_name,r.id_rol FROM dbo.person p
	INNER JOIN dbo.users u  ON u.id_user = p.id_user
	INNER JOIN dbo.roles r ON r.id_rol = u.id_rol
	WHERE p.per_email = @email AND u.user_password = @password
END
