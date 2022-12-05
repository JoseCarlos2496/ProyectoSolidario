using System.Reflection;
using BackendSolidario.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendSolidario.Infrastructure.Data {
    public class BcoSolidarioDbContext : DbContext{
        public BcoSolidarioDbContext() : base() { }

        public BcoSolidarioDbContext(DbContextOptions<BcoSolidarioDbContext> options) 
            : base(options) { 
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }

        protected void OnConfiguring(DbContextOptionsBuilder optionBuilder) {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}