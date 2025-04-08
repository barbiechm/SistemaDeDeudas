using SistemaDeDeudas.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SistemaDeDeudas.Services
{
    public interface IClienteService
    {
        Task<List<Cliente>> GetAllClientes();
        Task<Cliente> GetClienteById(int id);
        Task<Cliente> CreateCliente(Cliente cliente);
        Task<bool> UpdateDeuda(int id, decimal nuevaDeuda);
        Task<bool> DeleteCliente(int id);
    }
}
