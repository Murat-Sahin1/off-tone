using off_tone.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace off_tone.Persistence.Services
{
    public class BlogPostService
    {
        public readonly BlogDbContext _context;
        public BlogPostService(BlogDbContext context)
        {
            _context = context;
        }
        
    }
}
