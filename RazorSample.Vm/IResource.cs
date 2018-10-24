using System.Collections.Generic;

namespace RazorSample.Vm
{
  public interface IResource
  {
    IEnumerable<Link> Links { get; }
  }
}
