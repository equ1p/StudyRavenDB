using Raven.Client.Documents.Indexes;

namespace U2Lesson4v2;

public class EmployeesSalesPerMonth : AbstractIndexCreationTask<Order, EmployeesSalesPerMonth.Result>
{
    public class Result
    {
        public string Employee { get; set; }
        public string Month { get; set; }
        public int TotalSales { get; set; }
    }

    public EmployeesSalesPerMonth()
    {
        Map = orders =>
            from order in orders
            select new
            {
                Employee = order.Employee,
                Month = order.OrderedAt.ToString("yyyy-MM"),
                TotalSales = 1
            };

        Reduce = results =>
            from result in results
            group result by new
            {
                result.Employee,
                result.Month
            }
            into g
            select new
            {
                g.Key.Employee,
                g.Key.Month,
                
            }
    }
}