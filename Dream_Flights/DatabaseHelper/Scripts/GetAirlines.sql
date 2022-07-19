USE [programacion_5]
GO

/****** Object:  StoredProcedure [dbo].[GetAirlines]    Script Date: 28/6/2022 21:07:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetAirlines]
AS
BEGIN
	SELECT [Id]
      ,[Photo]
      ,[Name]
      ,[Phone]
	FROM [dbo].[Airlines]
END
GO

/*
Id	Name	Photo	Phone
29	Aeroméxico	/Images/Aeroméxico.PNG	2505-8090
30	Delta	/Images/Delta.PNG	2505-8090
31	Volaris	/Images/Volaris.PNG	2505-8090
32	JetBlue	/Images/JetBlue.PNG	2505-8090
*/


