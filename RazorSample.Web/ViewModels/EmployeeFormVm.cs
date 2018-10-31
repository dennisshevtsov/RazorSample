using RazorSample.Web.Commands;
using RazorSample.Web.Queries;

namespace RazorSample.Web.ViewModels
{
  public interface IEmployeeFormVm<out TCommand> : ICommandSource<TCommand>
    where TCommand : EmployeeFormCommandBase
  { }

  public sealed class EmployeeAddFormVm : FormVmBase<CreateEmployeeCommand>, IEmployeeFormVm<CreateEmployeeCommand> { }

  public sealed class EmployeeEditFormVm : FormVmBase<UpdateEmployeeQuery, UpdateEmployeeCommand>,
                                           IEmployeeFormVm<UpdateEmployeeCommand>
  { }
}
