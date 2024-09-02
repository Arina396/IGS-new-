using IGS.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace IGS.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Game> Games { get; set; }
    }
}
