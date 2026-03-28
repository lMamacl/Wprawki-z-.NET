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
                .UsingEntity<Dictionary<string, object>>( 
                    "UserClusterMemberships",
                    j => j.HasOne<Cluster>().WithMany().HasForeignKey("ClustersId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("UsersId"),
                    je =>
                    {
                        je.HasKey("ClustersId", "UsersId");
                        je.HasData(
                            new { ClustersId = 1, UsersId = 1 }, 
                            new { ClustersId = 2, UsersId = 1 }, 
                            new { ClustersId = 2, UsersId = 2 }  
                        );
                    });

            // 2. Dodawanie Klastrów
            modelBuilder.Entity<Cluster>().HasData(
                new Cluster { Id = 1, Name = "Klaster Północ" },
                new Cluster { Id = 2, Name = "Klaster Południe" },
                new Cluster { Id = 3, Name = "Klaster Wschód" }
            );

            // 3. Dodawanie Użytkowników
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FullName = "Jan Kowalski", Email = "jan@wprawka.pl" },
                new User { Id = 2, FullName = "Anna Nowak", Email = "anna@nowak.pl" }
            );

            // 4. Dodawanie Domów (Relacja 1:N)
            modelBuilder.Entity<Home>().HasData(
                new Home { Id = 1, Address = "ul. Wiejska 45, Białystok", UserId = 1 },
                new Home { Id = 2, Address = "ul. Mazowiecka 10, Warszawa", UserId = 1 },
                new Home { Id = 3, Address = "ul. Lipowa 1, Białystok", UserId = 2 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
