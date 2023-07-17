using Microsoft.EntityFrameworkCore;
using off_tone.Application.Interfaces.Repositories.Common;
using off_tone.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Persistence.Repositories.Common
{
    public abstract class WriteRepository<T> : Repository<T>, IWriteRepository<T> where T : class
    {
        public WriteRepository(BlogDbContext blogDbContext) : base(blogDbContext) {}

        public async Task<bool> InsertRangeAsync(List<T> list)
        {
            await Table.AddRangeAsync(list);
            return true;
        }

        public async Task<bool> SaveAsync()
        {
            await _blogDbContext.SaveChangesAsync();
            return true;
        }
    }
}

