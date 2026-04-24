using Raven.Client.Documents;
using U2Lesson3;

class Program
{
    static void Main(string[] args)
    {
        Console.Title = "Multi-Map Indexing";

        var store = DocumentStoreHolder.Store;

        DatabaseManager.DropDatabase(store);
        DatabaseManager.SeedDatabase(store);


        using (var session = store.OpenSession())
        {
            while (true)
            {
                Console.Write("Search Terms(exit to quit):");
                var searchTerms = Console.ReadLine();

                if (string.Equals(searchTerms, "exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                if (string.IsNullOrWhiteSpace(searchTerms)) continue;

                var results = PeopleSearchHelper.Search(session, searchTerms);

               Console.WriteLine("\nResults:");
               Console.WriteLine(new string('-', 50));

               int count = 0;
               foreach (var result in results)
               {
                   Console.WriteLine($"{result.SourceId,-20} | {result.Type,-20} | {result.Name}");
                   count++;
               }

               if (count == 0)
               {
                   Console.WriteLine("Nothing Found.");
               }
               Console.WriteLine(new string('-', 50));
            }
        }
    }
}
