using off_tone.Application.Interfaces.Repositories.BlogRepos;
using off_tone.Domain.Entities;
using off_tone.Persistence.Contexts;
using off_tone.Persistence.Repositories.Common;

namespace off_tone.Persistence.Repositories.BlogRepos
{
    public class BlogWriteRepository : WriteRepository<Blog>, IBlogWriteRepository
    {
        public BlogWriteRepository(BlogDbContext blogDbContext) : base(blogDbContext) { }
    }
}
