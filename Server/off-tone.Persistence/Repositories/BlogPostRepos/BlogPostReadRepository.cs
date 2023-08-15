using off_tone.Application.Interfaces.Repositories.BlogPostRepos;
using off_tone.Domain.Entities;
using off_tone.Persistence.Contexts;
using off_tone.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using off_tone.Application.Dtos.BlogPostDtos;
using off_tone.Persistence.QueryObjects.BlogPostQueryObjects;
using off_tone.Application.Feature.QueryOptions.Common;
using off_tone.Persistence.QueryObjects.Common;

namespace off_tone.Persistence.Repositories.BlogPostRepos
{
    public class BlogPostReadRepository : ReadRepository<BlogPost, BlogPostListDto>, IBlogPostReadRepository
    {
        public BlogPostReadRepository(BlogDbContext blogDbContext) : base(blogDbContext) { }

        public override IQueryable<BlogPostListDto> GetAllMappedToDto(QueryOptions queryOptions)
        {
            var query = Table.AsNoTracking().AsQueryable().Select(bp => new BlogPostListDto
            {
                BlogPostId = bp.BlogPostId,
                BlogId = bp.BlogId,
                BlogPostTitle = bp.BlogPostTitle,
                BlogPostText = bp.BlogPostText,
                BlogName = bp.Blog.BlogName,
                TagStrings = bp.Tags.Select(t => t.Name).ToArray(),
                AvarageReviewsVote = bp.Reviews.Select(r => (double?)r.Stars).Average(),
                ReviewCount = bp.Reviews.Count,
                CreationDate = bp.CreationDate,
            })
                .OrderBlogPostsBy((OrderByOptions)queryOptions.orderBy)
                .FilterBlogPostsBy((FilterByOptions)queryOptions.filterBy, queryOptions.filterValue);

                queryOptions.SetupDto(query);

                return query.Page(queryOptions.PageNum -1, queryOptions.PageSize);
        }

        public override IQueryable<BlogPostListDto> GetByIdMappedToDto(int id)
        {
            return Table.AsNoTracking().AsQueryable().Where(bp => bp.BlogPostId == id).Select(bp => new BlogPostListDto
            {
                BlogPostId = bp.BlogPostId,
                BlogId = bp.BlogId,
                BlogPostTitle = bp.BlogPostTitle,
                BlogPostText = bp.BlogPostText,
                BlogName = bp.Blog.BlogName,
                TagStrings = bp.Tags.Select(t => t.Name).ToArray(),
                AvarageReviewsVote = bp.Reviews.Select(r => (double?)r.Stars).Average(),
                ReviewCount = bp.Reviews.Count,
                CreationDate = bp.CreationDate,
            });
        }

    }
}
