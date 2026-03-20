using Microsoft.EntityFrameworkCore;
using Wprawka_1.Models;
namespace Wprawka_1.Data
{
    public class VoltShareContext : DbContext
    {
        public VoltShareContext(DbContextOptions<VoltShareContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Home> Homes { get; set; }
        public DbSet<Cluster> Clusters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Clusters)
                .WithMany(c => c.Users)
                .UsingEntity(j => j.ToTable("UserClusterMemberships"));

            base.OnModelCreating(modelBuilder);
        }
    }
}
