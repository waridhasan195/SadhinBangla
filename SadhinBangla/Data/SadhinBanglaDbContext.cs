using Microsoft.EntityFrameworkCore;
using SadhinBangla.Models.Domain;

namespace SadhinBangla.Data
{
    public class SadhinBanglaDbContext : DbContext
    {
        public SadhinBanglaDbContext(DbContextOptions<SadhinBanglaDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> tags { get; set; }
    }
}
