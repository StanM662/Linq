# LINQ Oefenproject

## Over dit project

Dit project is een oefenomgeving voor het leren van LINQ (Language Integrated Query) en Entity Framework Core in .NET 9. Het bevat een database met voorbeelden van LINQ-queries en uitwerkingen van studenten.

## Database structuur

Het project gebruikt een SQL Server database met twee hoofdtabellen:

- **Voorbeelden**: Bevat 100 voorbeeldopdrachten met LINQ/Entity Framework concepten
  - Id, Name, Description, Count, Role
- **Uitwerkingen**: Bevat 200 uitwerkingen van studenten
  - Id, Owner, Tries, VoorbeeldId (relatie naar Voorbeeld)

## Setup

1. Zorg dat SQL Server actief is
2. Pas indien nodig de connection string aan in `VoorbeeldDBContext.cs`
3. Voer de migraties uit in package manager console:

update-database

## Naslag
Zie : https://learn.microsoft.com/en-us/ef/core/querying/


## LINQ Oefeningen

Maak de volgende queries in `Program.cs` om te oefenen met basis LINQ-functionaliteit. Test je queries door ze uit te voeren en de resultaten te tonen.

### Oefening 1: Alle voorbeelden ophalen
Haal alle voorbeelden op uit de database en toon de naam en beschrijving.

### Oefening 2: Filteren op rol
Haal alle voorbeelden op die de rol `Administrator` hebben.

### Oefening 3: Sorteren
Haal alle voorbeelden op en sorteer ze op naam (alfabetisch).

### Oefening 4: Count gebruiken
Tel hoeveel uitwerkingen er in totaal zijn in de database.

### Oefening 5: Filteren met Where
Haal alle uitwerkingen op waar het aantal pogingen (Tries) groter is dan 10.

### Oefening 6: First of FirstOrDefault
Haal het eerste voorbeeld op met de naam "LINQ Select Query".

### Oefening 7: Aggregatie - Average
Bereken het gemiddelde aantal pogingen (Tries) van alle uitwerkingen.

### Oefening 8: Aggregatie - Max en Min
Vind het hoogste en laagste aantal pogingen (Tries) bij uitwerkingen.

### Oefening 9: Aggregatie - Sum
Bereken de totale som van het Count veld van alle voorbeelden.

### Oefening 10: GroupBy
Groepeer alle voorbeelden op Role en tel hoeveel voorbeelden er per rol zijn.

### Oefening 11: Include - Related data
Haal alle voorbeelden op en include de bijbehorende uitwerkingen (navigation property).

### Oefening 12: Include met filter
Haal alle voorbeelden op met Role `User` en include de bijbehorende uitwerkingen.

### Oefening 13: Select - Projectie
Haal alle voorbeelden op maar selecteer alleen de Id en Name (anoniem type of nieuwe klasse).

### Oefening 14: Where met meerdere condities
Haal alle voorbeelden op waar Count groter is dan 50 EN Role is `Moderator` of `Administrator`.

### Oefening 15: OrderBy en ThenBy
Sorteer alle voorbeelden eerst op Role en dan op Name.

### Oefening 16: Any
Controleer of er uitwerkingen zijn van de eigenaar "John Smith".

### Oefening 17: All
Controleer of alle uitwerkingen minimaal 1 poging (Tries) hebben.

### Oefening 18: Distinct eigenaren
Haal een lijst op met alle unieke eigenaren (Owner) uit de Uitwerkingen tabel.

### Oefening 19: GroupBy met aggregatie
Groepeer uitwerkingen op Owner en toon per eigenaar hoeveel uitwerkingen ze hebben gemaakt.

### Oefening 20: Where met Contains
Haal alle voorbeelden op waarvan de Description het woord "query" bevat (case-insensitive).

### Oefening 21: Complex query
Haal alle voorbeelden op met Role `Administrator` of `SuperAdministrator`, sorteer op Count (aflopend), en include de uitwerkingen waarvan Tries groter is dan 5.



## Tips

- Gebruik `using` statement voor je DbContext
- Test queries met `.ToList()` of `await .ToListAsync()` voor async queries
- Console.WriteLine of breakpoints gebruiken om resultaten te inspecteren
- Gebruik LINQ method syntax of query syntax (beide zijn geldig)

## Technologie

- .NET 9
- Entity Framework Core
- SQL Server
- C# 13.0


