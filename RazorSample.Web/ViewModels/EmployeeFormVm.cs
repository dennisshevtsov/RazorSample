using RazorSample.Web.Commands;
using RazorSample.Web.Queries;

namespace RazorSample.Web.ViewModels
{
  public sealed class EmployeeAddFormVm : FormVmBase<GetEmployeeQuery, CreateEmployeeCommand> { }

  public sealed class EmployeeEditFormVm : FormVmBase<GetEmployeeQuery, UpdateEmployeeCommand> { }
}
