using Login.Models;
using Microsoft.EntityFrameworkCore;

namespace Login.DB
{
    public class ContextModel : DbContext
    {
        public ContextModel(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        }
    }
}
