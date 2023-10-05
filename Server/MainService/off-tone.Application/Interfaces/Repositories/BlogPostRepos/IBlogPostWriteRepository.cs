using off_tone.Application.Interfaces.Repositories.Common;
using off_tone.Domain.Entities;

namespace off_tone.Application.Interfaces.Repositories.BlogPostRepos
{
    public interface IBlogPostWriteRepository : IWriteRepository<BlogPost>
    {
    }
}
