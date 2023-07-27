using Microsoft.EntityFrameworkCore;
using off_tone.Application.Interfaces.Repositories.Common;
using off_tone.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Persistence.Repositories.Common
{
    public abstract class ReadRepository<T, TDto> : Repository<T>, IReadRepository<T, TDto> where T : class
    {
        public ReadRepository(BlogDbContext blogDbContext) : base(blogDbContext) {}
        public abstract IQueryable<TDto> GetAllMappedToDto();
        public abstract IQueryable<TDto> GetByIdMappedToDto(int id);
        public async Task<T> GetByIdAsync(int id)
        {
            return await Table.FindAsync(id);
        }
        public IQueryable<T> GetWhere(Expression<Func<T, bool>> filterExpression)
        {
            return Table.AsNoTracking().Where(filterExpression);
        }
    }
}