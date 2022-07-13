USE [EasyPay]
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

CREATE TABLE [dbo].[TDBalance](
	[num_card] [int] NOT NULL,
	[balance] money NOT NULL,	
)
GO

ALTER TABLE [dbo].[TDBalance]  WITH CHECK ADD  CONSTRAINT [FK_Cards_TDBalance] FOREIGN KEY([num_card])
REFERENCES [dbo].[Cards] ([num_card])
GO

CREATE TABLE [dbo].[TCBalance](
	[num_card] [int] NOT NULL,
	[credit_limit] money NOT NULL,	
	[balance] money NOT NULL,	
)
GO

ALTER TABLE [dbo].[TCBalance]  WITH CHECK ADD  CONSTRAINT [FK_Cards_TCBalance] FOREIGN KEY([num_card])
REFERENCES [dbo].[Cards] ([num_card])
GO

CREATE TABLE [dbo].[Transaction](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[num_card] [int] NOT NULL,
	[amount] money NOT NULL,
	[transaction_date] datetime NOT NULL,
	[status] [varchar](2) NOT NULL,	
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Transaction] WITH CHECK ADD  CONSTRAINT [FK_Cards_Transaction] FOREIGN KEY([num_card])
REFERENCES [dbo].[Cards] ([num_card])
GO

CREATE PROCEDURE PurchaseTransaction 
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
end



