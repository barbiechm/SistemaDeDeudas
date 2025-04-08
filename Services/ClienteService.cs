using Microsoft.EntityFrameworkCore;
using SistemaDeDeudas.EFCore;
using SistemaDeDeudas.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SistemaDeDeudas.Services
{
    public class ClienteService : IClienteService
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> GetAllClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> GetClienteById(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<Cliente> CreateCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<bool> UpdateDeuda(int id, decimal nuevaDeuda)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return false;

            cliente.Deuda = nuevaDeuda;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return false;

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
