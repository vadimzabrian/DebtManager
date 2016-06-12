ALTER TABLE Users
ADD Username nvarchar(64) NOT NULL Default('username')
Go

update Users
set Username = 'adrian.moroi'
where Name = 'Adrian'

update Users
set Username = 'bogdan.dolhascu'
where Name = 'Bogdan'

update Users
set Username = 'ovidiu.petrache'
where Name = 'Ovidiu'

update Users
set Username = 'vadim.zabrian'
where Name = 'Vadim'