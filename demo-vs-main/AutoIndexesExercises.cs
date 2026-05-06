using Raven.Client.Documents;
using Raven.Client.Documents.Indexes.Vector;
using Raven.Client.Documents.Queries.Vector;
using TasksVs.Backend;

namespace TasksVs;

public partial class AutoIndexesExercises 
{
    public void TestTextual()
    {
        // HINT: https://ravendb.net/docs/article-page/7.0/csharp/ai-integration/vector-search/vector-search-using-dynamic-query#vector-search-on-text
        const string searchedPhrase = "italian food";
        using var store = DocumentStoreHolder.Store.OpenSession();

        var results = store.Query<Product>()
            .VectorSearch(field => field.WithText(x => x.Name),
                searchedTerm => searchedTerm.ByText(searchedPhrase), 0.82f) // Fill the method to retrieve items related to Italian food. Product object has a `Name` property.
            .ToList();

        ValidateTestTextual(results);
    }

    public void TestNumerical()
    {
        // HINT: https://ravendb.net/docs/article-page/7.0/csharp/ai-integration/vector-search/vector-search-using-dynamic-query#examples
        float[] vector = [.5f, .5f];
        using var store = DocumentStoreHolder.Store.OpenSession();

        // var results = store.Query<MyOwnProduct>()
        //      .VectorSearch(field => field.WithEmbedding(
        //          x => x.TagsEmbeddedAsInt8, VectorEmbeddingType.Int8),
        //          queryVector => queryVector.ByEmbedding(
        //              VectorQuantizer.ToInt8(vector)))
        //      .Customize(x => x.WaitForNonStaleResults())// Use this method to find documents whose 'MyVector' property is similar to 'vector'. Retrieve exactly the top 8 results.
        //     .ToList();
        
        ValidateTestNumerical(vector, results);
    }

    public void TestRavenVector()
    {
        // HINT: https://ravendb.net/docs/article-page/7.0/csharp/ai-integration/vector-search/vector-search-using-dynamic-query#examples
        RavenVector<float>? vector = null; // Create a 2D vector with [0.5f, 0.5f].
        ValidateRavenVector(vector);
        
        using var store = DocumentStoreHolder.Store.OpenSession();

        var results = store.Query<MyOwnProductWithRavenVector>()
            //.VectorSearch( todo (select field), v => v.ByEmbedding(vector)) // Use the above vector to find a vector similar to 'MyVector'.
            .ToList();
        
        ValidateTestRavenVector(vector, results);
    }
}