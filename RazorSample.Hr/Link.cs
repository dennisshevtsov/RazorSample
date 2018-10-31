using System;

namespace RazorSample.Hr
{
  public sealed class Link
  {
    public Link(string rel, string title, string href)
    {
      Rel = rel ?? throw new ArgumentNullException(nameof(rel));
      Title = title ?? throw new ArgumentNullException(nameof(title));
      Href = href ?? throw new ArgumentNullException(nameof(href));
    }

    public string Rel { get; }
    public string Title { get; }
    public string Href { get; }

    public static bool operator ==(Link left, Link right)
    {
      if ((object.ReferenceEquals(left, null) == false &&
           object.ReferenceEquals(right, null)) ||
          (object.ReferenceEquals(left, null) &&
           object.ReferenceEquals(right, null) == false))
      {
        return false;
      }

      if (object.ReferenceEquals(left, null) &&
          object.ReferenceEquals(right, null))
      {
        return true;
      }

      return left.Href == right.Href;
    }

    public static bool operator !=(Link left, Link right)
    {
      return (left == right) == false;
    }

    public override bool Equals(object obj)
    {
      if (obj is Link link)
      {
        return this == link;
      }

      return false;
    }

    public override int GetHashCode() => Href.GetHashCode();
    public override string ToString() => Title;
  }
}
