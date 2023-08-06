using off_tone.Application.Dtos.ReviewDtos;
using off_tone.Application.Feature.QueryOptions.Common;

namespace off_tone.Persistence.QueryObjects.ReviewQueryObjects
{
    public static class ReviewListDtoFilter
    {
        public static IQueryable<ReviewListDto> FilterReviewsBy(this IQueryable<ReviewListDto> query, FilterByOptions filterBy, string filterValue)
        {
            if (string.IsNullOrEmpty(filterValue)) return query;

            switch (filterBy)
            {
                case FilterByOptions.NoFilter:
                    return query;
                case FilterByOptions.ByVotes:
                    var filterVote = int.Parse(filterValue);
                    return query.Where(x => x.Stars >= filterVote);
                default: 
                    throw new ArgumentOutOfRangeException(nameof(filterValue), filterBy, null);
            }
        }
    }
}
