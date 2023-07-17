using off_tone.Application.Dtos;
using off_tone.Application.Interfaces.Repositories.BlogRepos;
using off_tone.Domain.Entities;
using off_tone.Persistence.Contexts;
using off_tone.Persistence.Repositories.Common;

namespace off_tone.Persistence.Repositories.BlogRepos
{
    public class BlogReadRepository : ReadRepository<Blog, BlogListDto>, IBlogReadRepository
    {
        public BlogReadRepository(BlogDbContext blogDbContext) : base(blogDbContext) { }

        public override IQueryable<BlogListDto> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
