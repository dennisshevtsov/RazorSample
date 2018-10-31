using System.Collections.Generic;

namespace RazorSample.Hr
{
  public interface IResource
  {
    IEnumerable<Link> Links { get; }

    IEnumerable<Property> Properties { get; }

    IEnumerable<KeyValuePair<string, IResource>> Embedded { get; }
  }
}
