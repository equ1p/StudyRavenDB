using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace U2Lesson3
{
    public static class DocumentStoreHelper
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

                var asm = Assembly.GetExecutingAssembly();
                IndexCreation.CreateIndexes(asm, store);

                var databaseRecord = store.Maintenance.Server.Send(new GetDatabaseRecordOperation(store.Database));

                if (databaseRecord == null)
                {
                    return store;
                }

                var creaDatabaseOperation = new CreateDatabaseOperation(new DatabaseRecord(store.Database));

                store.Maintenance.Server.Send(creaDatabaseOperation);

                return store;
            });
        public static IDocumentStore Store => LazyStore.Value;
    }
}
