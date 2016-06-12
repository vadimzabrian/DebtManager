ALTER TABLE Payments
ADD PayerBalance int NOT NULL DEFAULT(0)
Go

ALTER TABLE Payments
ADD ReceiverBalance int NOT NULL DEFAULT(0)
Go