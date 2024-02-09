# SchulprojektJERL

### Aufsetzen der Datenbank
- Datenbank script ausführen um die Datenbank aufzusetzen.
	- Database.sql im Root-Folder des Repos.
### WebApi einrichten
- WebApi starten.
	- Ausführen des RecipeApi-Projektes.
- Anmelden mit MariaDB-User welcher Zugriff auf die erstellte Datenbank hat.
	- Wenn Sie sich zum ersten mal anmelden wird eine secrets.json erstellt, die Ihre angegebenen Daten speichert, damit Sie sich bei bei wiederholtem Ausführen nicht erneut einloggen müssen (Die secrets.json ist in der .gitignore, wenn Sie Ihre Daten jedoch auch nicht lokal persistieren möchten, müssen Sie die secrets.json nach jedem erstellen löschen).
<img src=".\DokuRessources\DBLogin.jpg"/>
### Aufrufen des Clients
- In der Konsole angegebene URL öffnen (Dies sollte die Login-Seite des WebClients anzeigen).
- Gehen Sie auf die Registrierungsseite und registrieren Sie sich einen neuen Benutzer.
	- Wenn Sie angemeldet sind, gilt der Cookie für 30 Minuten bevor Sie sich erneut anmelden müssen.
- Die Unterseiten "Settings" und "Recipe" sind funktionsfähig.
	- "Settings" ermöglicht Ihnen, Ihre Nutzerdaten zu ändern.
	- "Recipe" listed die vorhandenen Rezepte auf. Klicken Sie auf ein Rezept, um die dazugehörigen Daten wie Zutaten, Zutatenmenge, ect anzuzeigen.
### Swagger API-Doku
- Um jedoch den vollen Ummfang der Endpoints betrachten zu können, rufen Sie, nachdem Sie sich über den Client eingeloggt haben, die Swagger-Doku auf https://localhost:7086/swagger/index.html
