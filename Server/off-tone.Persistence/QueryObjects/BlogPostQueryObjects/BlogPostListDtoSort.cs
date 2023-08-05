using off_tone.Application.Dtos.BlogPostDtos;

namespace off_tone.Persistence.QueryObjects.BlogPostQueryObjects
{
    public enum OrderBlogPostsByOptions
    {
        DefaultOrder = 0,
        ByVotes = 1,
        ByPublicationDate = 2,
    }

    public static class BlogPostListDtoSort
    {
        public static IQueryable<BlogPostListDto> OrderBlogPostsBy(this IQueryable<BlogPostListDto> blogPosts, OrderBlogPostsByOptions orderByOptions)
        {
            switch (orderByOptions)
            {
                case OrderBlogPostsByOptions.DefaultOrder:
                    return blogPosts.OrderByDescending(x => x.BlogPostId);
                case OrderBlogPostsByOptions.ByVotes:
                    return blogPosts.OrderByDescending(x => x.AvarageReviewsVote);
                case OrderBlogPostsByOptions.ByPublicationDate:
                    return blogPosts.OrderByDescending(x => x.CreationDate);
                default:
                    throw new ArgumentOutOfRangeException(nameof(orderByOptions), orderByOptions, null);
            }
        }
    }
}