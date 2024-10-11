using Microsoft.EntityFrameworkCore;
using Project_New_Server.Model;

namespace Project_New_Server.Context
{
    public class UserContext : DbContext
    {

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public DbSet<UserData> Users { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserData>().ToTable("Users");

        }

    }
}
