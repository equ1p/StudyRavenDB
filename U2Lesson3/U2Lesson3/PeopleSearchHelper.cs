using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Text;

namespace U2Lesson3
{
    public static class PeopleSearchHelper
    {
        public static IEnumerable<People_Search.Result> Search(
            IDocumentSession session, string searchTerms)
        {
            var results = session
        }
    }
}
