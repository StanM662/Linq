using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Linq.Database;
using Linq.Models;
using Microsoft.EntityFrameworkCore;

namespace Linq
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Oefenen met Linq");

            // ### Oefening 1: Alle voorbeelden ophalen
            // Haal alle voorbeelden op uit de database en toon de naam en beschrijving.
            Console.WriteLine("Oefening 1: Alle voorbeelden ophalen");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeelden = context.Voorbeelden.ToList();

                foreach (var voorbeeld in voorbeelden)
                {
                    Console.WriteLine($"Naam: {voorbeeld.Name}; Beschrijving: {voorbeeld.Description}");                    
                }

            }
            Console.WriteLine("");
            // ### Oefening 2: Filteren op rol
            // Haal alle voorbeelden op die de rol `Administrator` hebben.
            Console.WriteLine("Oefening 2: Filteren op rol");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeelden = context.Voorbeelden
                    .Where(v => v.Role == Role.Administrator).ToList();
                foreach (var voorbeeld in voorbeelden)
                {
                    Console.WriteLine($"Naam: {voorbeeld.Name}; Beschrijving: {voorbeeld.Description}; Rol: {voorbeeld.Role}");
                }
            }
            Console.WriteLine("");
            // ### Oefening 3: Sorteren op naam
            // Haal alle voorbeelden op en sorteer ze op naam (alfabetisch).
            Console.WriteLine("Oefening 3: Sorteren op naam");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeelden = context.Voorbeelden
                    .OrderBy(v => v.Name).ToList();
                foreach (var voorbeeld in voorbeelden)
                {
                    Console.WriteLine($"Naam: {voorbeeld.Name}; Beschrijving: {voorbeeld.Description};");
                }
            }
            Console.WriteLine("");
            // ### Oefening 4: Count gebruiken
            // Tel hoeveel uitwerkingen er in totaal zijn in de database.
            Console.WriteLine("Oefening 4: Count gebruiken");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeelden = context.Voorbeelden
                    .Count();
                Console.WriteLine($"Er zijn {voorbeelden} voorbeelden in de database");
            }
            Console.WriteLine("");
            // ### Oefening 5: Filteren met where
            // Haal alle uitwerkingen op waar het aantal pogingen(Tries) groter is dan 10.
            Console.WriteLine("Oefening 5: Filteren met where");
            using (var context = new VoorbeeldDBContext())
            {
                var uitwerkingen = context.Uitwerkingen
                    .Where(u => u.Tries > 10).ToList();
                foreach (var uitwerking in uitwerkingen)
                {
                    Console.WriteLine($"Eigenaar: {uitwerking.Owner} Pogingen: {uitwerking.Tries}");
                }
            }
            Console.WriteLine("");
            // ### Oefening 6: First of FirstOrDefault
            // Haal het eerste voorbeeld op met de naam "LINQ Select Query".
            Console.WriteLine("Oefening 6: First of FirstOrDefault");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeeld = context.Voorbeelden
                    .First(v => v.Name == "LINQ Select Query");
                Console.WriteLine($"Naam: {voorbeeld.Name}; Id: {voorbeeld.Id}");
            }
            Console.WriteLine("");
            // ### Oefening 7: Aggregatie - Average
            // Bereken het gemiddelde aantal pogingen (Tries) van alle uitwerkingen.
            Console.WriteLine("Oefening 7: Aggregatie - Average");
            using (var context = new VoorbeeldDBContext())
            {
                var uitwerking = context.Uitwerkingen
                    .Average(u => u.Tries);
                Console.WriteLine($"het gemiddelde aantal pogingen van alle uitwerkingen is {uitwerking}");   
            }
            Console.WriteLine("");
            // ### Oefening 8: Aggregatie - Max en Min
            // Vind het hoogste en laagste aantal pogingen (Tries) bij uitwerkingen.
            Console.WriteLine("Oefening 8: Aggregatie - Max en Min");
            using (var context = new VoorbeeldDBContext())
            {
                var uitwerkingLaag = context.Uitwerkingen
                    .Min(u => u.Tries);
                var uitwerkingHoog = context.Uitwerkingen
                    .Max(u => u.Tries);
                Console.WriteLine($"het laagste aantal pogingen is {uitwerkingLaag} en het hoogste aantal pogingen is {uitwerkingHoog}");
            }
            Console.WriteLine("");
            // ### Oefening 9: Aggregatie - Sum
            // Bereken de totale som van het Count veld van alle voorbeelden.
            Console.WriteLine("Oefening 9: Aggregatie - Sum");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeeld = context.Voorbeelden
                    .Sum(v => v.Count);
                Console.WriteLine($"De som van het veld count van alle voorbeelden is {voorbeeld}");
            }
            Console.WriteLine("");
            // ### Oefening 10: GroupBy
            // Groepeer alle voorbeelden op Role en tel hoeveel voorbeelden er per rol zijn.
            Console.WriteLine("Oefening 10: GroupBy");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeeldenPerRole = context.Voorbeelden
                    .GroupBy(v => v.Role)
                    .Select(g => new
                    {
                        Role = g.Key,
                        Aantal = g.Count()
                    })
                    .ToList();
                foreach (var voorbeeld in voorbeeldenPerRole)
                {
                    Console.WriteLine($"Role: {voorbeeld.Role}, Aantal: {voorbeeld.Aantal}");
                }
            }
            Console.WriteLine("");
            // ### Oefening 11: Include - Related Data
            // Haal alle voorbeelden op en include de bijbehorende uitwerkingen (navigation property).
            Console.WriteLine("Oefening 11: Include - Related Data");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeelden = context.Voorbeelden
                    .Include(v => v.Uitwerkingen)
                    .ToList();

                foreach (var voorbeeld in voorbeelden)
                {
                    Console.WriteLine($"Naam: {voorbeeld.Name}");
                    Console.WriteLine($"Uitwerkingen:");
                    foreach (var uitwerking in voorbeeld.Uitwerkingen)
                    {
                        Console.WriteLine($"id: {uitwerking.Id}");
                        Console.WriteLine($"Eigenaar: {uitwerking.Owner}");
                        Console.WriteLine("");
                    }
                    Console.WriteLine("");
                }
            }
            Console.WriteLine("");
            // ### Oefening 12: Include met filter
            // Haal alle voorbeelden op met Role User en include de bijbehorende uitwerkingen.
            Console.WriteLine("Oefening 12: Include met filter");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeelden = context.Voorbeelden
                    .Where(v => v.Role == Role.User)
                    .Include(v => v.Uitwerkingen)
                    .ToList();

                foreach (var voorbeeld in voorbeelden)
                {
                    Console.WriteLine($"Naam: {voorbeeld.Name}");
                    Console.WriteLine($"Uitwerkingen:");
                    foreach (var uitwerking in voorbeeld.Uitwerkingen)
                    {
                        Console.WriteLine($"id: {uitwerking.Id}");
                        Console.WriteLine($"Eigenaar: {uitwerking.Owner}");
                        Console.WriteLine("");
                    }
                    Console.WriteLine("");
                }
            }
            Console.WriteLine("");
            // ### Oefening 13: Select - Projectie
            // Haal alle voorbeelden op maar selecteer alleen de Id en Name (anoniem type of nieuwe klasse).
            Console.WriteLine("Oefening 13: Select - Projectie");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeelden = context.Voorbeelden
                    .Select(v => new
                    {
                        v.Id,
                        v.Name
                    })
                    .ToList();
                foreach (var voorbeeld in voorbeelden)
                {
                    Console.WriteLine($"Naam: {voorbeeld.Name}; Id: {voorbeeld.Id};");
                }
            }
            Console.WriteLine("");
            // ### Oefening 14: Where met meerdere condities
            // Haal alle voorbeelden op waar Count groter is dan 50 EN Role is Moderator of Administrator.
            Console.WriteLine("Oefening 14: Where met meerdere condities");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeelden = context.Voorbeelden
                    .Where(v => v.Count > 50)
                    .Where(v => v.Role == Role.Administrator)
                    .ToList();

                foreach (var voorbeeld in voorbeelden)
                {
                    Console.WriteLine($"Count: {voorbeeld.Count}, Rol: {voorbeeld.Role}");
                }
            }
            Console.WriteLine("");
            // ### Oefening 15: OrderBy en ThenBy
            // Sorteer alle voorbeelden eerst op Role en dan op Name.
            Console.WriteLine("Oefening 15: OrderBy en ThenBy");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeelden = context.Voorbeelden
                    .OrderBy(v => v.Role)
                    .ThenBy(v => v.Name)
                    .ToList();

                foreach (var voorbeeld in voorbeelden)
                {
                    Console.WriteLine($"Rol: {voorbeeld.Role}, Naam: {voorbeeld.Name}");
                }
            }
            Console.WriteLine("");
            // ### Oefening 16: Any
            // Controleer of er uitwerkingen zijn van de eigenaar "John Smith".
            Console.WriteLine("Oefening 16: Any");
            using (var context = new VoorbeeldDBContext())
            {
                var uitwerkingen = context.Uitwerkingen
                    .Any(u => u.Owner == "John Smith");
                if (uitwerkingen )
                {
                    Console.WriteLine($"Er zijn uitwerkingen met de eigenaar 'John Smith'.");
                }
                else
                {
                    Console.WriteLine("Er zijn geen uitwerkingen met de eigenaar 'John Smith'.");
                }
            }
            Console.WriteLine("");
            // ### Oefening 17: All
            // Controleer of alle uitwerkingen minimaal 1 poging (Tries) hebben.
            Console.WriteLine("Oefening 17: All");
            using (var context = new VoorbeeldDBContext())
            {
                var uitwerkingen = context.Uitwerkingen
                    .All(u => u.Tries >= 1);
                if (uitwerkingen)
                {
                    Console.WriteLine($"Alle uitwerkingen hebben minimaal 1 poging.");
                }
                else
                {
                    Console.WriteLine("Alle uitwerkingen hebben minimaal 1 poging.");
                }
            }
            Console.WriteLine("");
            // ### Oefening 18: Distinct eigenaren
            // Haal een lijst op met alle unieke eigenaren (Owner) uit de Uitwerkingen tabel.
            Console.WriteLine("Oefening 18: Distinct eigenaren");
            using (var context = new VoorbeeldDBContext())
            {
                var uitwerkingen = context.Uitwerkingen
                    .Select(u => u.Owner)
                    .Distinct()
                    .ToList();

                foreach (var uitwerking in uitwerkingen)
                {
                    Console.WriteLine($"Eigenaar: {uitwerking}");
                }
            }
            Console.WriteLine("");
            // ### Oefening 19: GroupBy met aggregatie
            // Groepeer uitwerkingen op Owner en toon per eigenaar hoeveel uitwerkingen ze hebben gemaakt.
            Console.WriteLine("Oefening 19: GroupBy met aggregatie");
            using (var context = new VoorbeeldDBContext())
            {
                var uitwerkingen = context.Uitwerkingen
                    .GroupBy(u => u.Owner)
                    .Select(g => new
                    {
                        Owner = g.Key,
                        Aantal = g.Count()
                    })
                    .ToList();

                foreach (var uitwerking in uitwerkingen)
                {
                    Console.WriteLine($"Eigenaar: {uitwerking.Owner}, Aantal uitwerkingen: {uitwerking.Aantal}");
                }
            }
            Console.WriteLine("");
            // ### Oefening 20: Where met Contains
            // Haal alle voorbeelden op waarvan de Description het woord "query" bevat (case-insensitive).
            Console.WriteLine("Oefening 20: Where met Contains");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeelden = context.Voorbeelden
                    .Where(v => v.Description.Contains("query"))
                    .ToList();

                foreach (var voorbeeld in voorbeelden)
                {
                    Console.WriteLine($"Description: {voorbeeld.Description}, Naam: {voorbeeld.Name}");
                }
            }
            Console.WriteLine("");
            // ### Oefening 21: Complex query
            // Haal alle voorbeelden op met Role Administrator of SuperAdministrator,
            // sorteer op Count (aflopend), en include de uitwerkingen waarvan Tries groter is dan 5.
            Console.WriteLine("Oefening 21: Complex query");
            using (var context = new VoorbeeldDBContext())
            {
                var voorbeelden = context.Voorbeelden
                    .Where(v => v.Role == Role.Administrator || v.Role == Role.SuperAdministrator)
                    .OrderByDescending(v => v.Count)
                    .Include(v => v.Uitwerkingen
                        .Where(u => u.Tries > 5))
                    .ToList();

                foreach (var voorbeeld in voorbeelden)
                {
                    Console.WriteLine(
                        $"Voorbeeld: {voorbeeld.Name}, Count: {voorbeeld.Count}"
                    );

                    foreach (var uitwerking in voorbeeld.Uitwerkingen!)
                    {
                        Console.WriteLine($"  - Owner: {uitwerking.Owner}, Tries: {uitwerking.Tries}");
                    }
                }
            }
            Console.WriteLine("");

        }
    }
}
