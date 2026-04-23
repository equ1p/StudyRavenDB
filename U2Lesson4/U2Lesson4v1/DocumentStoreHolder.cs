using System.Reflection;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using U2Lesson4;

public static class DocumentStoreHolder
{
    private static readonly Lazy<IDocumentStore> LazyStore =
        new Lazy<IDocumentStore>(() =>
        {
            var store = new DocumentStore
            {
                Urls = new[] { "http://localhost:8080" },
                Database = "Northwind"
            };

            store.Initialize();

            var databaseRecord = store.Maintenance.Server.Send(new GetDatabaseRecordOperation(store.Database));

            if (databaseRecord == null)
            {
                var createDatabaseOperation = new CreateDatabaseOperation(new DatabaseRecord(store.Database));
                store.Maintenance.Server.Send(createDatabaseOperation);
            }

            var asm = Assembly.GetExecutingAssembly();
            IndexCreation.CreateIndexes(asm, store);

            DatabaseSeeder.SeedAsync(store).GetAwaiter().GetResult();

            return store;
        });
    public static IDocumentStore Store => LazyStore.Value;
}