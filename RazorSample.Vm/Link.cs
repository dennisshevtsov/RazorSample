using System;
using System.Collections.Generic;
using System.Text;

namespace RazorSample.Vm
{
  public sealed class Link
  {
    internal Link(string rel, string title, string href)
    {
      Rel = rel ?? throw new ArgumentNullException(nameof(rel));
      Title = title ?? throw new ArgumentNullException(nameof(title));
      Href = href ?? throw new ArgumentNullException(nameof(href));
    }

    public string Rel { get; }
    public string Title { get; }
    public string Href { get; }
  }
}
