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
                Console.WriteLine($"DATABASE_URL is: {connectionString}");
                if (!string.IsNullOrEmpty(connectionString))
                {
                    optionsBuilder.UseNpgsql(connectionString);
                }
                else
                {
                    
                    optionsBuilder.UseNpgsql("Server=localhost;Database=ClientesDb; User Id=postgres; Password=barbie08*.");
                }
            }
        }
    }
}