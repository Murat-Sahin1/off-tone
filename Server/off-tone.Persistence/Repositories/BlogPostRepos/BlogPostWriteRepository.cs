using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using off_tone.Application.Interfaces.Repositories.BlogPostRepos;
using off_tone.Domain.Entities;
using off_tone.Persistence.Contexts;
using off_tone.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using off_tone.Application.Dtos.BlogPostDtos;

namespace off_tone.Persistence.Repositories.BlogPostRepos
{
    public class BlogPostWriteRepository : WriteRepository<BlogPost>, IBlogPostWriteRepository
    {
        public BlogPostWriteRepository(BlogDbContext blogDbContext) : base(blogDbContext) { }
    }

}
