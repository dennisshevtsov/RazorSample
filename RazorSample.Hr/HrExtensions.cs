using System;

namespace RazorSample.Hr
{
  public static class HrExtensions
  {
    public static IResourceBuilder Property(this IResourceBuilder source, string name, string displayName)
    {
      return source.Property(name, displayName, null, null);
    }

    public static IResourceBuilder Property(this IResourceBuilder source, string name, string displayName, object value)
    {
      return source.Property(name, displayName, value, null);
    }

    public static IResourceBuilder Property(this IResourceBuilder source, string name, string displayName, object value, string displayValue)
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

      return source.Property(new Property(name, displayName, value, displayValue));
    }

    public static IResourceBuilder Link(this IResourceBuilder source, string rel, string title, string href)
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      if (rel == null)
      {
        throw new ArgumentNullException(nameof(rel));
      }

      if (title == null)
      {
        throw new ArgumentNullException(nameof(title));
      }

      if (href == null)
      {
        throw new ArgumentNullException(nameof(href));
      }

      return source.Link(new Link(rel, title, href));
    }
  }
}
