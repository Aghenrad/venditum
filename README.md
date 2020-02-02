# venditum

TMP DB:
NuGet Package Manager > Package Manager Console (PMC):
Add-Migration InitialCreate
Update-Database

Aby wrzuciło bazę trzeba usunąć folder Migrations, w konsoli PMC użyć komendy "Add-Migration InitialCreate". Po zakończeniu podmienić wszystkie (CTRL+H) Cascade na NoAction. Na koniec "Update-Database" i poczekać na stworznie bazy.


Przykładowe, ale wymagane inserty na start:
INSERT INTO Positions (Name, Active) VALUES ('Developer', 1);
INSERT INTO Users (Login, Password, Firstname, LastName, PositionId, Active) VALUES ('Admin', 'Admin123', 'Admin', 'Admin', 1, 1);
INSERT INTO Statuses (Name, Active) VALUES ('Open', 1);
INSERT INTO Statuses (Name, Active) VALUES ('In Progress', 1);
INSERT INTO Statuses (Name, Active) VALUES ('Completed', 1);
INSERT INTO Statuses (Name, Active) VALUES ('Canceled', 1);
