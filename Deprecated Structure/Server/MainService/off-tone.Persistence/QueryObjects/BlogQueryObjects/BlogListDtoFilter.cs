using off_tone.Application.Dtos.BlogDtos;
using off_tone.Application.Feature.QueryOptions.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Persistence.QueryObjects.BlogQueryObjects
{
    public static class BlogListDtoFilter
    {
        public static IQueryable<BlogListDto> FilterBlogsBy(this IQueryable<BlogListDto> query, FilterByOptions filterBy, string filterValue)
        {
            if (string.IsNullOrEmpty(filterValue)) return query;

            switch (filterBy)
            {
                case FilterByOptions.NoFilter:
                    return query;
                case FilterByOptions.ByName:
                    return query.Where(x => x.BlogName == filterValue);
                default: 
                    throw new ArgumentOutOfRangeException(nameof(filterValue), filterBy, null);
            }
        }
    }
}
