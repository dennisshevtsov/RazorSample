using RazorSample.Data;
using RazorSample.Web.Queries;
using RazorSample.Web.ViewModels;

namespace RazorSample.Web.Services
{
    public interface IEmployeeService : IQueryHandler<SearchEmployeesQuery, Page<EmployeeListItemVm>>
    { }
}
