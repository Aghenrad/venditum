# venditum

TMP DB:
NuGet Package Manager > Package Manager Console (PMC):
Add-Migration InitialCreate
Update-Database

Aby wrzuciło bazę trzeba usunąć folder Migrations, w konsoli PMC użyć komendy "Add-Migration InitialCreate". Po zakończeniu podmienić wszystkie (CTRL+H) Cascade na NoAction. Na koniec "Update-Database" i poczekać na stworznie bazy.