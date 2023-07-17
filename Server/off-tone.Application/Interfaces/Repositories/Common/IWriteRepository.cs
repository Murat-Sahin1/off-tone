using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Application.Interfaces.Repositories.Common
{
    public interface IWriteRepository<T> : IRepository<T> where T : class
    {
        public Task<bool> InsertRangeAsync(List<T> list);
    }
}
