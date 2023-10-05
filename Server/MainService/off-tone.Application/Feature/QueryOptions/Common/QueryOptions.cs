using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Application.Feature.QueryOptions.Common
{
    public enum FilterByOptions
    {
        NoFilter = 0,
        ByName = 1,
        ByVotes = 2,
        ByPublicationYear = 3,
        ByTags = 4,
    }
    public enum OrderByOptions
    {
        DefaultOrder = 0,
        ByName = 1,
        ByVote = 2,
        ByPublicationDate = 3,
        ByReviewCount = 4,
    }

    public class QueryOptions
    {
        public int orderBy;
        public int filterBy;
        public string filterValue;

        // Pagination
        public const int DefaultPageSize = 10;
        private int _pageNum = 1;
        private int _pageSize = DefaultPageSize;

        public int[] PageSizes = new int[] {5, DefaultPageSize, 20, 50, 100, 500, 1000};

        public int PageNum
        {
            get { return _pageNum; }
            set { _pageNum = value;  }
        }

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        public int AvailablePagesNum { get; private set; }
        public string PrevCheckState { get; set; }

        public void SetupDto<T>(IQueryable<T> query)
        {
            AvailablePagesNum = (int)Math.Ceiling((double) query.Count() / PageSize);
            PageNum = Math.Min(Math.Max(1, PageNum), AvailablePagesNum);
        }
    }
}
