using off_tone.Application.Interfaces.Repositories.ReviewRepos;
using off_tone.Domain.Entities;
using off_tone.Persistence.Contexts;
using off_tone.Persistence.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Persistence.Repositories.ReviewRepos
{
    public class ReviewWriteRepository : WriteRepository<Review>, IReviewWriteRepository
    {
        public ReviewWriteRepository(BlogDbContext blogDbContext) : base(blogDbContext)
        {
        }
    }
}
