using off_tone.Application.Interfaces.Repositories.Common;
using off_tone.Domain.Entities;
using off_tone.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Application.Interfaces.Repositories.BlogPostRepos
{
    public interface IBlogPostReadRepository : IReadRepository<BlogPost, BlogPostListDto>
    {

    }
}
