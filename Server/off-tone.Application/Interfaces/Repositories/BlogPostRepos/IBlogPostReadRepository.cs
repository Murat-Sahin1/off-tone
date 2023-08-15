using off_tone.Application.Interfaces.Repositories.Common;
using off_tone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using off_tone.Application.Dtos.BlogPostDtos;
using Microsoft.EntityFrameworkCore;

namespace off_tone.Application.Interfaces.Repositories.BlogPostRepos
{
    public interface IBlogPostReadRepository : IReadRepository<BlogPost, BlogPostListDto>
    {
        
    }
}
