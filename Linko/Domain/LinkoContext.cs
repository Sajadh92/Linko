using Microsoft.EntityFrameworkCore;

namespace Linko.Domain
{
    public class LinkoContext : DbContext
    {
        public LinkoContext(DbContextOptions<LinkoContext> options) : base(options)
        {

        }

        protected LinkoContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserProfileMap());
            modelBuilder.ApplyConfiguration(new AccountMap());
        }

        public DbSet<UserProfile> UsersProfiles { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}