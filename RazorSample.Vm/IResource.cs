using System.Collections.Generic;

namespace RazorSample.Vm
{
  public interface IResource
  {
    IEnumerable<Link> Links { get; }

    IEnumerable<Property> Properties { get; }

    IEnumerable<KeyValuePair<string, IResource>> Embedded { get; }
  }
}
