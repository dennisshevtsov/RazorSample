using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RazorSample.Vm;

namespace RazorSample.Web
{
  internal sealed class ViewNameActionFilter : IActionFilter
  {
    public void OnActionExecuted(ActionExecutedContext context)
    {
      if (context.Result is ViewResult viewResult)
      {
        if (viewResult.Model is IGridVm)
        {
          viewResult.ViewName = "GridView";
        }
        else if (viewResult.Model is IFormVm)
        {
          viewResult.ViewName = "FormView";
        }
      }
    }

    public void OnActionExecuting(ActionExecutingContext context) { }
  }
}
