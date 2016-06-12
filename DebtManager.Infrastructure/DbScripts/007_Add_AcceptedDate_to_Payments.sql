ALTER TABLE Payments
ADD AcceptedDate datetime NULL
Go

update Payments
set AcceptedDate = [Date]