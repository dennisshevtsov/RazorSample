using System.Collections.Generic;

namespace RazorSample.Web.ViewModels
{
    public abstract class ListVmBase<TQuery, TItem> : VmBase<TQuery>
        where TQuery : class
        where TItem : class
    {
        public IEnumerable<TItem> Items { get; internal set; }

        public int PageNo { get; internal set; }
        public int PageCount { get; internal set; }
        public int PageSize { get; internal set; }

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
