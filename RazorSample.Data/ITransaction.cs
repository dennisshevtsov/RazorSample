using System;
using System.Threading.Tasks;

namespace RazorSample.Data
{
  public interface ITransaction : IDisposable
  {
    Task CommitAsync();
    Task RollbackAsync();
  }
}
