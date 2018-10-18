using Microsoft.AspNetCore.Mvc;
using RazorSample.Data;
using RazorSample.Web.Queries;
using RazorSample.Web.ViewModels;
using System;
using System.Collections.Generic;

namespace RazorSample.Web.Extensions
{
  public static class VmExtensions
  {
    public const string InfoMessageKey = "InfoMessage";
    public const string ErrorMessageKey = "ErrorMessage";

    public static TViewModel Controller<TViewModel>(this TViewModel source, Controller controller)
        where TViewModel : VmBase
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      if (controller == null)
      {
        throw new ArgumentNullException(nameof(controller));
      }

      if (controller.TempData.TryGetValue(InfoMessageKey, out object info))
      {
        source.InfoMessage = (string)info;
      }

      if (controller.TempData.TryGetValue(ErrorMessageKey, out object error))
      {
        source.ErrorMessage = (string)error;
      }

      return source;
    }

    public static TViewModel Items<TViewModel, TItem>(
        this TViewModel source, QueryExecutionResult<Page<TItem>> queryExecutionResult)
        where TViewModel : VmBase, IListSourceInternal<TItem>
        where TItem : class
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      if (queryExecutionResult == null)
      {
        throw new ArgumentNullException(nameof(queryExecutionResult));
      }

      if (queryExecutionResult.HasError)
      {
        source.ErrorMessage = queryExecutionResult.ErrorMessage;
      }
      else
      {
        source.Items = queryExecutionResult.Result;
      }

      return source;
    }

    public static TViewModel Items<TViewModel, TItem>(
    this TViewModel source, IEnumerable<TItem> items)
    where TViewModel : VmBase, IListSourceInternal<TItem>
    where TItem : class
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      if (items == null)
      {
        throw new ArgumentNullException(nameof(items));
      }

      source.Items = items;

      return source;
    }

    public static TViewModel Query<TViewModel, TQuery>(this TViewModel source, TQuery query)
        where TViewModel : VmBase<TQuery>
        where TQuery : class
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      if (query == null)
      {
        throw new ArgumentNullException(nameof(query));
      }

      source.Query = query;

      return source;
    }

    public static TViewModel Command<TViewModel, TCommand>(
      this TViewModel source, QueryExecutionResult<TCommand> queryExecutionResult)
      where TViewModel : VmBase, ICommandSourceInternal<TCommand>
      where TCommand : class
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      if (queryExecutionResult == null)
      {
        throw new ArgumentNullException(nameof(queryExecutionResult));
      }

      if (queryExecutionResult.HasError)
      {
        source.ErrorMessage = queryExecutionResult.ErrorMessage;
      }
      else
      {
        source.Command = queryExecutionResult.Result;
      }

      return source;
    }

    public static TViewModel Command<TViewModel, TCommand>(
      this TViewModel source, TCommand command)
      where TViewModel : VmBase, ICommandSourceInternal<TCommand>
      where TCommand : class
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }

      if (command == null)
      {
        throw new ArgumentNullException(nameof(command));
      }

      source.Command = command;

      return source;
    }
  }
}
