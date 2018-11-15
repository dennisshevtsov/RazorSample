using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace RazorSample.Data
{
  internal sealed class Transaction : ITransaction
  {
    private readonly IDbContextTransaction _transaction;

    internal Transaction(IDbContextTransaction transaction)
    {
      _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
    }

    public Task CommitAsync()
    {
      _transaction.Commit();

      return Task.CompletedTask;
    }

    public Task RollbackAsync()
    {
      _transaction.Rollback();

      return Task.CompletedTask;
    }

    private bool _disposed;
    public void Dispose()
    {
      if (_disposed == false)
      {
        _transaction?.Dispose();

        GC.SuppressFinalize(this);

        _disposed = true;
      }
    }
  }
}
