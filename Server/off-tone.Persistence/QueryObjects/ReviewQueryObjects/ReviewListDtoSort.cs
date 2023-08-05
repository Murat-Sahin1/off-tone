using off_tone.Application.Dtos.ReviewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Persistence.QueryObjects.ReviewQueryObjects
{
    public enum OrderReviewsByOptions
    {
        DefaultOrder = 0,
        ByVote,
    }
    public static class ReviewListDtoSort
    {
        public static IQueryable<ReviewListDto> OrderReviewsBy(this IQueryable<ReviewListDto> query, OrderReviewsByOptions orderReviewsByOptions)
        {
            switch (orderReviewsByOptions)
            {
                case OrderReviewsByOptions.DefaultOrder:
                    return query.OrderBy(x => x.ReviewId);
                case OrderReviewsByOptions.ByVote:
                    return query.OrderByDescending(x => x.Stars);
                default:
                    throw new ArgumentOutOfRangeException(nameof(orderReviewsByOptions), orderReviewsByOptions, null);
            }
        }
    }
}
