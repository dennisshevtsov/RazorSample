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
      return left.Href == right.Href;
    }

    public static bool operator !=(Link left, Link right)
    {
      return left.Href != right.Href;
    }

    public override bool Equals(object obj)
    {
      if (obj is Link link)
      {
        return Href == link.Href;
      }

      return false;
    }

    public override int GetHashCode() => Href.GetHashCode();
    public override string ToString() => Title;
  }
}
