using Microsoft.AspNetCore.Mvc;
using RazorSample.Web.ViewModels;
using System;

namespace RazorSample.Web.Extensions
{
  public static class UrlExtensions
  {
    public static string AppUri<TQuery>(this IUrlHelper source, string action, string controller, TQuery query)
      where TQuery : class
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      if (action == null)
      {
        throw new ArgumentNullException(nameof(action));
      }

      if (controller == null)
      {
        throw new ArgumentNullException(nameof(controller));
      }

      var controllerShortName = controller.Replace("controller", "", StringComparison.InvariantCultureIgnoreCase);
      var uri = source.Action(action, controllerShortName, query);

      return uri;
    }

    public static string AppUri(this IUrlHelper source, string action, string controller)
    {
      return source.AppUri(action, controller, default(object));
    }

    public static Link AppLink<TQuery>(
      this IUrlHelper source, string rel, string title, string action, string controller, TQuery query)
      where TQuery : class
    {
      if (string.IsNullOrWhiteSpace(rel))
      {
        throw new ArgumentException($"Argument {nameof(rel)} cannot be null or a white space.");
      }

      var uri = source.AppUri(action, controller, query);
      var link = new Link(rel, title, uri);

      return link;
    }

    public static Link AppLink(this IUrlHelper source, string rel, string title, string action, string controller)
    {
      return source.AppLink(rel, title, action, controller, default(object));
    }
  }
}
