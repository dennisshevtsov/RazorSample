using System;

namespace RazorSample.Web.Queries
{
    public sealed class QueryExecutionResult<TResult> where TResult : class
    {
        public QueryExecutionResult(TResult result)
        {
            Result = result ?? throw new ArgumentNullException(nameof(result));
        }

        public QueryExecutionResult(string errorMessage)
        {
            ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
        }

        public TResult Result { get; }

        public string ErrorMessage { get; }
        public bool HasError => string.IsNullOrWhiteSpace(ErrorMessage) == false;
    }
}
