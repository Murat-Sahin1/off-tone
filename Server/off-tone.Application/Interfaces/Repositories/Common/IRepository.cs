using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Application.Interfaces.Repositories.Common
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Table { get; }
        public bool EnsureCreation();
        public bool AnyElements();
    }
}
