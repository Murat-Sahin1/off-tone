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
        ByVotes = 1,
        ByTags = 2,
        ByPublicationYear = 3,
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
    }
}
