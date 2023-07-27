using off_tone.Application.Interfaces.Repositories.BlogPostRepos;
using off_tone.Domain.Entities;
using off_tone.Persistence.Contexts;
using off_tone.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using off_tone.Application.Dtos.BlogPostDtos;

namespace off_tone.Persistence.Repositories.BlogPostRepos
{
    public class BlogPostReadRepository : ReadRepository<BlogPost, BlogPostListDto>, IBlogPostReadRepository
    {
        public BlogPostReadRepository(BlogDbContext blogDbContext) : base(blogDbContext) { }

        public override IQueryable<BlogPostListDto> GetAllMappedToDto()
        {
            return Table.AsNoTracking().AsQueryable().Select(bp => new BlogPostListDto
            { 
                BlogPostId = bp.BlogPostId,
                BlogId = bp.BlogId,
                BlogPostTitle = bp.BlogPostTitle,
                BlogPostText = bp.BlogPostText,
                BlogName = bp.Blog.BlogName,
                TagStrings = bp.Tags.Select(t => t.Name).ToArray(),
            });
        }

        public override IQueryable<BlogPostListDto> GetByIdMappedToDto(int id)
        {
            return Table.AsQueryable().Where(bp => bp.BlogPostId == id).Select(bp => new BlogPostListDto
            {
                BlogPostId = bp.BlogPostId,
                BlogId = bp.BlogId,
                BlogPostTitle = bp.BlogPostTitle,
                BlogPostText = bp.BlogPostText,
                BlogName = bp.Blog.BlogName,
                TagStrings = bp.Tags.Select(t => t.Name).ToArray(),
            });
        }
    }
}
