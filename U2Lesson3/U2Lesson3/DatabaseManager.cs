using System.Reflection;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using U2Lesson3.Models;

namespace U2Lesson3;

public static class DatabaseManager
{
    public static void DropDatabase(IDocumentStore store)
    {
        Console.WriteLine("Dropping database...");
        var operation = new DeleteDatabasesOperation(store.Database, true);
        store.Maintenance.Server.Send(operation);
    }

    public static void SeedDatabase(IDocumentStore store)
    {
        Console.WriteLine("Filling Database with seed data...");

        var databaseRecord = store.Maintenance.Server.Send(new GetDatabaseRecordOperation(store.Database));
        if (databaseRecord == null)
        {
            store.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(store.Database)));
            IndexCreation.CreateIndexes(Assembly.GetExecutingAssembly(), store);
        }

        using (var session = store.OpenSession())
        {
            var random = new Random();

            var firstNames = new[]
            {
                "Ivan", "John", "Anna", "Maria", "Peter", "Taras", "Olena", "Dmytro", "Sarah", "Michael", "Katarzyna",
                "Michal", "Piotr", "Agnieszka", "David"
            };
            var lastNames = new[]
            {
                "Tymoshchuk", "Doe", "Smith", "Johnson", "Kovalenko", "Shevchenko", "Brown", "Miller", "Davis",
                "Garcia", "Nowak", "Kowalski", "Wisniewski", "Wojcik", "Kaminski"
            };

            for (var i = 0; i < 50; i++)
            {
                var emp = new Employee
                {
                    FirstName = firstNames[random.Next(firstNames.Length)],
                    LastName = lastNames[random.Next(lastNames.Length)]
                };
                session.Store(emp);
            }

            var companyPrefixes = new[]
                { "Tech", "Global", "Web", "NextGen", "Cloud", "Data", "Smart", "Alpha", "Beta", "Omega" };
            var companySuffixes = new[]
            {
                "Solutions", "Hardware", "Innovations", "Systems", "Networks", "Corp", "Soft", "Logic", "Dynamics",
                "Enterprises"
            };

            for (var i = 0; i < 15; i++)
            {
                var companyName =
                    $"{companyPrefixes[random.Next(companyPrefixes.Length)]} {companySuffixes[random.Next(companySuffixes.Length)]}";
                var comp = new Company { Contact = new Contact { Name = companyName } };
                session.Store(comp);
            }

            var supplierAdjectives = new[]
                { "Fast", "Quality", "Prime", "Reliable", "Global", "Direct", "Eco", "Provisional", "Apex", "Elite" };
            var supplierNouns = new[]
            {
                "Delivery", "Parts", "Logistics", "Sources", "Supplies", "Imports", "Components", "Materials", "Goods",
                "Distributions"
            };

            for (var i = 0; i < 15; i++)
            {
                var supplierName =
                    $"{supplierAdjectives[random.Next(supplierAdjectives.Length)]} {supplierNouns[random.Next(supplierNouns.Length)]}";
                var sup = new Supplier { Contact = new Contact { Name = supplierName } };
                session.Store(sup);
            }

            session.SaveChanges();
        }

        Console.WriteLine("Data successfully added");
    }
}