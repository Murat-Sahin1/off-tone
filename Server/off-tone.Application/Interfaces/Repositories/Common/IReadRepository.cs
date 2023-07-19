using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Application.Interfaces.Repositories.Common
{
    public interface IReadRepository<T, TDto> : IRepository<T> where T : class
    {
        public IQueryable<TDto> GetAll();
        public IQueryable<TDto> GetById(int id);
    }
}
