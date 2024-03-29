USE [master]
GO
/****** Object:  Database [EasyPay]    Script Date: 30/9/2022 08:56:48 ******/
CREATE DATABASE [EasyPay]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EasyPay', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\EasyPay.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EasyPay_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\EasyPay_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [EasyPay] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EasyPay].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EasyPay] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EasyPay] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EasyPay] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EasyPay] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EasyPay] SET ARITHABORT OFF 
GO
ALTER DATABASE [EasyPay] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EasyPay] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EasyPay] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EasyPay] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EasyPay] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EasyPay] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EasyPay] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EasyPay] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EasyPay] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EasyPay] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EasyPay] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EasyPay] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EasyPay] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EasyPay] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EasyPay] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EasyPay] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EasyPay] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EasyPay] SET RECOVERY FULL 
GO
ALTER DATABASE [EasyPay] SET  MULTI_USER 
GO
ALTER DATABASE [EasyPay] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EasyPay] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EasyPay] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EasyPay] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EasyPay] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EasyPay] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'EasyPay', N'ON'
GO
ALTER DATABASE [EasyPay] SET QUERY_STORE = OFF
GO
USE [EasyPay]
GO
/****** Object:  Table [dbo].[Cards]    Script Date: 30/9/2022 08:56:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cards](
	[num_card] [int] NOT NULL,
	[month_exp] [varchar](2) NOT NULL,
	[year_exp] [varchar](4) NOT NULL,
	[cvv] [varchar](4) NOT NULL,
	[type] [varchar](2) NOT NULL,
 CONSTRAINT [PK_Cards] PRIMARY KEY CLUSTERED 
(
	[num_card] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TCBalance]    Script Date: 30/9/2022 08:56:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TCBalance](
	[num_card] [int] NOT NULL,
	[credit_limit] [money] NOT NULL,
	[balance] [money] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TDBalance]    Script Date: 30/9/2022 08:56:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TDBalance](
	[num_card] [int] NOT NULL,
	[balance] [money] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 30/9/2022 08:56:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[num_card] [int] NOT NULL,
	[amount] [money] NOT NULL,
	[transaction_date] [datetime] NOT NULL,
	[status] [varchar](2) NOT NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TCBalance]  WITH CHECK ADD  CONSTRAINT [FK_Cards_TCBalance] FOREIGN KEY([num_card])
REFERENCES [dbo].[Cards] ([num_card])
GO
ALTER TABLE [dbo].[TCBalance] CHECK CONSTRAINT [FK_Cards_TCBalance]
GO
ALTER TABLE [dbo].[TDBalance]  WITH CHECK ADD  CONSTRAINT [FK_Cards_TDBalance] FOREIGN KEY([num_card])
REFERENCES [dbo].[Cards] ([num_card])
GO
ALTER TABLE [dbo].[TDBalance] CHECK CONSTRAINT [FK_Cards_TDBalance]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Cards_Transaction] FOREIGN KEY([num_card])
REFERENCES [dbo].[Cards] ([num_card])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Cards_Transaction]
GO
/****** Object:  StoredProcedure [dbo].[ExecutePayment]    Script Date: 30/9/2022 08:56:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ExecutePayment] 
	@num_card int,
	@month_exp varchar(2),
	@year_exp varchar(4),
	@cvv varchar(4),
	@amount money
AS
BEGIN
	declare @status varchar(2)
	declare @_type varchar(2)
	declare @_month_exp varchar(2)
	declare @_year_exp varchar(4)
	declare @_cvv varchar(4)
	set @_type = (select [type] from cards where num_card = @num_card)
	set @_month_exp = (select month_exp from cards where num_card = @num_card)
	set @_year_exp = (select year_exp from cards where num_card = @num_card)
	set @_cvv = (select cvv from cards where num_card = @num_card)

	if not exists (select num_card from cards where num_card = @num_card) 
	begin
		--print('Invalid card number')		
		select -1 result
		return
	end

	if @_month_exp <> @month_exp or @_year_exp <> @year_exp
	begin
		--print('Invalid expiration date')		
		set @status = -2	
		goto save_tran
		return
	end
		
	if @_cvv <> @cvv
	begin
		--print('Incorrect CVV')	
		set @status = -3		
		goto save_tran
		return
	end
		
	if @_type = 'TC'
		declare @limit money
		declare @balance money
		declare @availabe money
		set @limit = (select credit_limit from tcbalance where num_card = @num_card)
		set @balance = (select balance from tcbalance where num_card = @num_card)
		set @availabe = @limit - @balance

		if @amount <= @limit
			if @amount <= @availabe
				begin
					--print('valid purchase')						
					update [dbo].[TCBalance] set balance = (@balance+@amount)
					where num_card = @num_card
					
					set @status = -0	
					goto save_tran
					return
				end
			else
				begin
					--print('Insufficient available funds')				
					set @status = -4	
					goto save_tran
					return
				end
		else
			begin
				--print('Amount over credit limit')	
				set @status = -4	
				goto save_tran
				return
			end					

	if @_type = 'TD'
		print('TD')	

save_tran:
	begin
		insert into [dbo].[Transaction]						 
					 values
						   (@num_card
						   ,@amount
						   ,getdate()
						   ,@status)
	end

	select @status result
END

GO
USE [master]
GO
ALTER DATABASE [EasyPay] SET  READ_WRITE 
GO
