using Raven.Client.Documents.Indexes;
using Raven.Client.Documents.Indexes;
using U2Lesson4.Models;

public class ProductsByCategory : AbstractIndexCreationTask<Product, ProductsByCategory.Result>
{
    public class Result
    {
        public string Category { get; set; }
        public int Count { get; set; }
    }

    public ProductsByCategory()
    {
        Map = products =>
            from product in products
            select new
            {
                Category = product.Category,
                Count = 1
            };

        Reduce = results =>
            from result in results
            group result by result.Category
            into g
            select new
            {
                Category = g.Key,
                Count = g.Sum(x => x.Count)
            };
    }
}

