using Microsoft.EntityFrameworkCore;
using Wprawka_2.Models;

namespace Wprawka_2.Data
{
    // Klasa musi dziedziczyć po DbContext z Entity Framework Core
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet oznacza: "Chcę mieć w bazie tabelę o nazwie Clusters zbudowaną na podstawie klasy Cluster"
        public DbSet<Cluster> Clusters { get; set; }

        // DbSet oznacza: "Chcę mieć tabelę Devices zbudowaną na podstawie klasy Device"
        public DbSet<Device> Devices { get; set; }

        // Ta metoda służy do konfiguracji bazy i DODAWANIA DANYCH TESTOWYCH (Seeding)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Wstawiamy 3 testowe klastry bezpośrednio do bazy przy jej tworzeniu
            modelBuilder.Entity<Cluster>().HasData(
                new Cluster { Id = 1, Name = "Klaster Północ" },
                new Cluster { Id = 2, Name = "Klaster Południe" },
                new Cluster { Id = 3, Name = "Klaster Wschód" }
            );

            // Opcjonalnie: Możemy też dodać od razu testowe urządzenie przypisane do Klastra nr 1
            modelBuilder.Entity<Device>().HasData(
                new Device { Id = 1, DeviceName = "Pompa Ciepła Alpha", PowerWatt = 4500, ClusterId = 1 },
                new Device { Id = 2, DeviceName = "Pompa Ciepła Beta", PowerWatt = 2500, ClusterId = 2 }
            );
        }
    }
}