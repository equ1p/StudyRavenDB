using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Text;
using Raven.Client.Documents;

namespace U2Lesson3
{
    public static class PeopleSearchHelper
    {
        public static IEnumerable<People_Search.Result> Search(
            IDocumentSession session, string searchTerms)
        {
            var results = session.Query<People_Search.Result, People_Search>()
                .Customize(x => x.WaitForNonStaleResults())
                .Search(
                    r => r.Name,
                    searchTerms
                )
                .ProjectInto<People_Search.Result>()
                .ToList();

            return results;
        }
    }
}
