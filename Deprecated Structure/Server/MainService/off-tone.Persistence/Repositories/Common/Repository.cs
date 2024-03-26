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
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly BlogDbContext _blogDbContext;
        public Repository(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }
        public DbSet<T> Table => _blogDbContext.Set<T>();

        public bool AnyElements()
        {
            bool isSeeded = Table.Any();
            return isSeeded;
        }

        public bool EnsureCreation()
        {
            bool isCreated = _blogDbContext.Database.EnsureCreated();
            return isCreated;
        }
    }
}