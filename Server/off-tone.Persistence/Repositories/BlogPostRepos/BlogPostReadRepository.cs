using off_tone.Application.Interfaces.Repositories.BlogPostRepos;
using off_tone.Domain.Entities;
using off_tone.Persistence.Contexts;
using off_tone.Application.Dtos;
using off_tone.Persistence.Repositories.Common;

namespace off_tone.Persistence.Repositories.BlogPostRepos
{
    public class BlogPostReadRepository : ReadRepository<BlogPost, BlogPostListDto>, IBlogPostReadRepository
    {
        public BlogPostReadRepository(BlogDbContext blogDbContext) : base(blogDbContext) { }

        public override IQueryable<BlogPostListDto> GetAll()
        {
            return Table.AsQueryable().Select(bp => new BlogPostListDto
            {
                BlogPostId = bp.BlogPostId,
                BlogPostTitle = bp.BlogPostTitle,
                BlogPostText = bp.BlogPostText,
                TagStrings = bp.Tags.Select(t => t.Name).ToArray(),
            });
        }
    }
}
