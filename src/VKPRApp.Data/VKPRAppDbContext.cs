using Microsoft.EntityFrameworkCore;
using VKPRApp.Shared.Models;

namespace VKPRApp.Data
{
    public class VKPRAppDbContext : DbContext
    {
        public VKPRAppDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<VKUser> VKUsers { get; set; }
        public DbSet<BankCard> BankCard { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Shared.Models.Task> Tasks { get; set; }
    }
}
