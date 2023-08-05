using Microsoft.EntityFrameworkCore;
using off_tone.Application.Dtos.BlogDtos;
using off_tone.Application.Dtos.BlogPostDtos;
using off_tone.Application.Interfaces.Repositories.BlogRepos;
using off_tone.Domain.Entities;
using off_tone.Persistence.Contexts;
using off_tone.Persistence.QueryObjects.BlogQueryObjects;
using off_tone.Persistence.Repositories.Common;

namespace off_tone.Persistence.Repositories.BlogRepos
{
    public class BlogReadRepository : ReadRepository<Blog, BlogListDto>, IBlogReadRepository
    {
        public BlogReadRepository(BlogDbContext blogDbContext) : base(blogDbContext) { }

        public override IQueryable<BlogListDto> GetAllMappedToDto()
        {
            return Table.AsNoTracking().AsQueryable().AsNoTracking().Select(b => new BlogListDto
            {
                BlogId = b.BlogId,
                BlogName = b.BlogName,
                BlogDescription = b.BlogDescription,
                SubName = b.SubName
            }).OrderBlogsBy(OrderBlogsByOptions.DefaultOrder);
        }

        public override IQueryable<BlogListDto> GetByIdMappedToDto(int id)
        {
            return Table.AsNoTracking().AsQueryable().Where(b => b.BlogId == id).Select(b => new BlogListDto
            {
                BlogId = b.BlogId,
                BlogName = b.BlogName,
                BlogDescription = b.BlogDescription,
                SubName = b.SubName,
                BlogPosts = b.BlogPosts.Select(bp => new BlogPostListDto
                {
                    BlogPostId = bp.BlogPostId,
                    BlogPostTitle = bp.BlogPostTitle,
                    BlogPostText = bp.BlogPostText,
                    BlogName = bp.Blog.BlogName,
                    TagStrings = bp.Tags.Select(t => t.Name).ToArray(),
                }),
            });
        }
    }
}
