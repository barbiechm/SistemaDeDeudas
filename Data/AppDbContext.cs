using Microsoft.EntityFrameworkCore;
using SistemaDeDeudas.Models;
using System.Threading.Tasks;

namespace SistemaDeDeudas.EFCore
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        
    }
}
