USE [EasyPay]
GO
/****** Object:  StoredProcedure [dbo].[ExecutePayment]    Script Date: 30/9/2022 08:53:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[ExecutePayment] 
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

