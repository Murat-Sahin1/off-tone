using off_tone.Application.Dtos.ReviewDtos;
using off_tone.Application.Feature.QueryOptions.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Persistence.QueryObjects.ReviewQueryObjects
{
    public static class ReviewListDtoSort
    {
        public static IQueryable<ReviewListDto> OrderReviewsBy(this IQueryable<ReviewListDto> query, OrderByOptions orderByOptions)
        {
            switch (orderByOptions)
            {
                case OrderByOptions.DefaultOrder:
                    return query.OrderBy(x => x.ReviewId);
                case OrderByOptions.ByVote:
                    return query.OrderByDescending(x => x.Stars);
                default:
                    throw new ArgumentOutOfRangeException(nameof(orderByOptions), orderByOptions, null);
            }
        }
    }
}
