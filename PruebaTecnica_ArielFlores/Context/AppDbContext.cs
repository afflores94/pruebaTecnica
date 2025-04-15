using Microsoft.EntityFrameworkCore;
using PruebaTecnica_ArielFlores.Models.Entidades;

namespace PruebaTecnica_ArielFlores.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Cuentas> Cuentas { get; set; }
        public DbSet<Transacciones> Transacciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cuentas>()
                .HasIndex(c => c.NumeroCuenta)
                .IsUnique();
        }
    }
}
