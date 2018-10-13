namespace RazorSample.Web.ViewModels
{
    public abstract class FormVmBase<TQuery, TCommand> : VmBase<TQuery>
        where TQuery : class
        where TCommand : class
    {
        public TCommand Command { get; internal set; }
    }
}
