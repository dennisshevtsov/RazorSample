using System;
using System.Collections;
using System.Collections.Generic;

namespace RazorSample.Data
{
  public static class Page
  {
    public const int DefaultPageSize = 10;
  }

  public sealed class Page<TEntity> : IEnumerable<TEntity> where TEntity : class
  {
    private readonly IEnumerable<TEntity> _entities;

    public Page(IEnumerable<TEntity> entities, int pageNo, int pageCount, int pageSize)
    {
      _entities = entities ?? throw new ArgumentNullException(nameof(entities));

      PageNo = pageNo;
      PageCount = pageCount;
      PageSize = pageSize;
    }

    public int PageNo { get; }
    public int PageCount { get; }
    public int PageSize { get; }

    public IEnumerator<TEntity> GetEnumerator() => _entities.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _entities.GetEnumerator();
  }
}
