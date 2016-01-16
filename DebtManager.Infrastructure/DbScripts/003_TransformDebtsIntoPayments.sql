DECLARE @PayerId int
DECLARE @ReceiverId int
DECLARE @Amount int
DECLARE @Reason nvarchar(max)
DECLARE @Date datetime

DECLARE @MyCursor CURSOR
SET @MyCursor = CURSOR FAST_FORWARD
				FOR
				SELECT Payer_Id, Receiver_Id, Amount, Reason, [Date]
				FROM   Debts 

OPEN @MyCursor
	FETCH NEXT FROM @MyCursor
	INTO @PayerId, @ReceiverId, @Amount, @Reason, @Date


	WHILE @@FETCH_STATUS = 0

	BEGIN


		insert into Payments(Payer_Id, Receiver_Id, Amount, Reason, [Date]) 
		values(@ReceiverId, @PayerId, @Amount, @Reason, @Date)

 
	FETCH NEXT FROM @MyCursor
	INTO @PayerId, @ReceiverId, @Amount, @Reason, @Date

End