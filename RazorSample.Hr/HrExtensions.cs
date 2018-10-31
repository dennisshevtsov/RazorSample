using System;

namespace RazorSample.Hr
{
  public static class HrExtensions
  {
    public static IResourceBuilder Property(this IResourceBuilder source, string name, string displayName, object value)
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      if (name == null)
      {
        throw new ArgumentNullException(nameof(name));
      }

      if (displayName == null)
      {
        throw new ArgumentNullException(nameof(displayName));
      }

      return source.Property(new Property(name, displayName, value));
    }
  }
}
