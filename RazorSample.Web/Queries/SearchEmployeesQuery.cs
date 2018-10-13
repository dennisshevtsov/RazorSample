using Microsoft.AspNetCore.Mvc;

namespace RazorSample.Web.Queries
{
    public sealed class SearchEmployeesQuery
    {
        [FromQuery]
        public string EmployeeNo { get; set; }

        [FromQuery]
        public int PageNo { get; set; }

        internal int PageSize => 10;
    }
}
