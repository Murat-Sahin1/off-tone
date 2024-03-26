using Microsoft.EntityFrameworkCore;
using off_tone.Domain.Entities;


namespace off_tone.Persistence.Contexts
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
        }
    }
}
