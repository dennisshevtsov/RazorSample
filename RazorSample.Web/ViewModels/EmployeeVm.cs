using RazorSample.Web.Commands;
using RazorSample.Web.Queries;

namespace RazorSample.Web.ViewModels
{
  public sealed class EmployeeVm : FormVmBase<GetEmployeeQuery, UpdateEmployeeCommand> { }
}
