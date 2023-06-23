using Microsoft.EntityFrameworkCore;
using user.Models;

namespace user.Data
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }


        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
            Users = Set<User>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
