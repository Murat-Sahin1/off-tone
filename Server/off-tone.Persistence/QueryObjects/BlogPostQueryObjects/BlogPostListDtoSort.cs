using off_tone.Application.Dtos.BlogPostDtos;
using off_tone.Application.Feature.QueryOptions.Common;

namespace off_tone.Persistence.QueryObjects.BlogPostQueryObjects
{
    public static class BlogPostListDtoSort
    {
        public static IQueryable<BlogPostListDto> OrderBlogPostsBy(this IQueryable<BlogPostListDto> blogPosts, OrderByOptions orderByOptions)
        {
            switch (orderByOptions)
            {
                case OrderByOptions.DefaultOrder:
                    return blogPosts.OrderByDescending(x => x.BlogPostId);
                case OrderByOptions.ByVote:
                    return blogPosts.OrderByDescending(x => x.AvarageReviewsVote);
                case OrderByOptions.ByPublicationDate:
                    return blogPosts.OrderByDescending(x => x.CreationDate);
                default:
                    throw new ArgumentOutOfRangeException(nameof(orderByOptions), orderByOptions, null);
            }
        }
    }
}