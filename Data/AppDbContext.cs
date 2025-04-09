using Microsoft.EntityFrameworkCore;
using SistemaDeDeudas.Models;
using System;

namespace SistemaDeDeudas.EFCore
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
                var connectionStringLocal = Environment.GetEnvironmentVariable("DATABASE_URL_LOCAL");
                Console.WriteLine($"DATABASE_URL is: {connectionString}");
                if (!string.IsNullOrEmpty(connectionString))
                {
                    optionsBuilder.UseNpgsql(connectionString);
                }
                else if (!string.IsNullOrEmpty(connectionStringLocal))
                {

                    optionsBuilder.UseNpgsql(connectionStringLocal);
                }
                else
                {
                    // Fallback para desarrollo si no hay variable de entorno local

                    Console.WriteLine("No hay base disponible");
                  
                }
            }
        }
    }
}