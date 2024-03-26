using off_tone.Application.Dtos.BlogPostDtos;
using off_tone.Application.Feature.QueryOptions.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Persistence.QueryObjects.BlogPostQueryObjects
{
    public static class BlogPostListDtoFilter
    {
        public static IQueryable<BlogPostListDto> FilterBlogPostsBy(this IQueryable<BlogPostListDto> query, FilterByOptions filterBy, string filterValue)
        {
            if (string.IsNullOrEmpty(filterValue)) return query;

            switch(filterBy)
            {
                case FilterByOptions.NoFilter:
                    return query;
                case FilterByOptions.ByVotes:
                    var filterVote = int.Parse(filterValue);
                    return query.Where(x => x.AvarageReviewsVote >= filterVote);
                case FilterByOptions.ByTags:
                    return query.Where(x => x.TagStrings.Any(y => y == filterValue));
                case FilterByOptions.ByPublicationYear:
                    var filterYear = int.Parse(filterValue);
                    return query.Where(x => x.CreationDate.Year == filterYear);
                default:
                    throw new ArgumentOutOfRangeException
                        (nameof(filterBy), filterBy, null);
            }
        } 
    }
}
