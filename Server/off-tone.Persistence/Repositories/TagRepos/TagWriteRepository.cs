using off_tone.Application.Dtos.TagDtos;
using off_tone.Application.Interfaces.Repositories.TagRepos;
using off_tone.Persistence.Contexts;
using off_tone.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Persistence.Repositories.TagRepos
{
    public class TagWriteRepository : WriteRepository<TagCreateDto>, ITagWriteRepository
    {
        public TagWriteRepository(BlogDbContext blogDbContext) : base(blogDbContext)
        {
        }
    }
}
