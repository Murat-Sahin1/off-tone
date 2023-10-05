using off_tone.Application.Dtos.BlogDtos;
using off_tone.Application.Feature.QueryOptions.Common;

namespace off_tone.Persistence.QueryObjects.BlogQueryObjects
{
    public static class BlogListDtoSort
    {
        public static IQueryable<BlogListDto> OrderBlogsBy(this IQueryable<BlogListDto> query, OrderByOptions orderByOptions)
        {
            switch (orderByOptions)
            {
                case OrderByOptions.DefaultOrder:
                    return query.OrderByDescending(x => x.BlogId);
                case OrderByOptions.ByName:
                    return query.OrderByDescending(x => x.BlogName);
                default:
                    throw new ArgumentOutOfRangeException(nameof(orderByOptions), orderByOptions, null);
            }
        }
    }
}