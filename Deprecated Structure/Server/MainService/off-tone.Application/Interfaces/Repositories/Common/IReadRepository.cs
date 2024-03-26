using Microsoft.EntityFrameworkCore;
using off_tone.Application.Feature.QueryOptions.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Application.Interfaces.Repositories.Common
{
    public interface IReadRepository<T, TDto> : IRepository<T> where T : class
    {
        public IQueryable<TDto> GetAllMappedToDto(QueryOptions queryOptions);
        public IQueryable<TDto> GetByIdMappedToDto(int id);
        public Task<T> GetByIdAsync(int id);
        public IQueryable<T> GetWhere(Expression<Func<T, bool>> filterExpression);
    }
}
