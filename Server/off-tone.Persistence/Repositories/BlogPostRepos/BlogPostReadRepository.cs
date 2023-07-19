using off_tone.Application.Interfaces.Repositories.BlogPostRepos;
using off_tone.Domain.Entities;
using off_tone.Persistence.Contexts;
using off_tone.Application.Dtos;
using off_tone.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace off_tone.Persistence.Repositories.BlogPostRepos
{
    public class BlogPostReadRepository : ReadRepository<BlogPost, BlogPostListDto>, IBlogPostReadRepository
    {
        public BlogPostReadRepository(BlogDbContext blogDbContext) : base(blogDbContext) { }

        public override IQueryable<BlogPostListDto> GetAll()
        {
            return Table.AsNoTracking().AsQueryable().Select(bp => new BlogPostListDto
            {
                BlogPostId = bp.BlogPostId,
                BlogPostTitle = bp.BlogPostTitle,
                BlogPostText = bp.BlogPostText,
                BlogName = bp.Blog.BlogName,
                // TagStrings = bp.Tags.Select(t => t.Name).ToArray(),
            });
        }

        public override IQueryable<BlogPostListDto> GetById(int id)
        {
            return Table.AsNoTracking().AsQueryable().Where(bp => bp.BlogPostId == id).Select(bp => new BlogPostListDto
            {
                BlogPostId = bp.BlogPostId,
                BlogPostTitle = bp.BlogPostTitle,
                BlogPostText = bp.BlogPostText,
                BlogName = bp.Blog.BlogName,
                // TagStrings = bp.Tags.Select(t => t.Name).ToArray(),
            });
        }
    }
}
