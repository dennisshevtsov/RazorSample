using System.Collections.Generic;

namespace RazorSample.Web.ViewModels
{
  public interface IListSource<TItem>
  {
    IEnumerable<TItem> Items { get; }

    int PageNo { get; }
    int PageCount { get; }
    int PageSize { get; }
  }

  public interface IListSourceInternal<TItem>
  {
    IEnumerable<TItem> Items { get; set; }

    int PageNo { get; set; }
    int PageCount { get; set; }
    int PageSize { get; set; }
  }

  public abstract class ListVmBase<TQuery, TItem> : VmBase<TQuery>, IListSource<TItem>, IListSourceInternal<TItem>
      where TQuery : class
      where TItem : class
  {
    public IEnumerable<TItem> Items { get; internal set; }
    IEnumerable<TItem> IListSourceInternal<TItem>.Items { get { return Items; } set { Items = value; } }

    public int PageNo { get; internal set; }
    int IListSourceInternal<TItem>.PageNo { get { return PageNo; } set { PageNo = value; } }

    public int PageCount { get; internal set; }
    int IListSourceInternal<TItem>.PageCount { get { return PageCount; } set { PageCount = value; } }

    public int PageSize { get; internal set; }
    int IListSourceInternal<TItem>.PageSize { get { return PageSize; } set { PageSize = value; } }

    public int FirstPageNo => 0;

    public int PrevPageNo
    {
      get
      {
        if (IsPrevPageEnabled)
        {
          return PageNo - 1;
        }

        return PageNo;
      }
    }

    public int NextPageNo
    {
      get
      {
        if (IsNextPageEnabled)
        {
          return PageNo + 1;
        }

        return PageNo;
      }
    }

    public int LastPageNo => PageCount - 1;

    public bool IsFirtPageEnabled => PageNo > FirstPageNo;
    public bool IsPrevPageEnabled => IsFirtPageEnabled;
    public bool IsNextPageEnabled => IsLastPageEnabled;
    public bool IsLastPageEnabled => PageNo < LastPageNo;
  }
}
