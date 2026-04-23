using System;
using Raven.Client;
using Raven.Client.Documents.Indexes;
using System.Threading.Tasks;
using Raven.Client.Documents;
using U2Lesson4.Models;

class Program
{
    static async Task Main(string[] args)
    {
        using (var session = DocumentStoreHolder.Store.OpenAsyncSession())
        {
            var results = await session
                .Query<ProductsByCategory.Result, ProductsByCategory>()
                .Customize(x => x.WaitForNonStaleResults())
                .Include(x => x.Category)
                .ToListAsync();

            foreach (var result in results)
            {
                var category = await session.LoadAsync<Category>(result.Category);
                if (category != null)
                    Console.WriteLine($"{category.Name}: {result.Count}");
            }
        }
    }
}
