using Microsoft.AspNetCore.Mvc;
using SistemaDeDeudas.Models;
using SistemaDeDeudas.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/clientes")]  // La ruta base será "api/clientes"
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;

    // Inyectamos el servicio (patrón Dependency Injection)
    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    // GET api/clientes → Obtener todos los clientes
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var clientes = await _clienteService.GetAllClientes();
        return Ok(clientes);  // Devuelve 200 OK con la lista de clientes
    }

    // GET api/clientes/5 → Obtener cliente por ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var cliente = await _clienteService.GetClienteById(id);
        if (cliente == null) return NotFound();  // Si no existe, devuelve 404
        return Ok(cliente);  // Si existe, devuelve 200 OK con el cliente
    }

    // POST api/clientes → Crear nuevo cliente
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Cliente cliente)
    {
        var createdCliente = await _clienteService.CreateCliente(cliente);
        return CreatedAtAction(nameof(GetById), new { id = createdCliente.Id }, createdCliente);
        // Devuelve 201 Created con la ubicación del nuevo cliente
    }

    // PUT api/clientes/5/deuda → Actualizar deuda
    [HttpPut("{id}/deuda")]
    public async Task<IActionResult> UpdateDeuda(int id, [FromBody] decimal nuevaDeuda)
    {
        var result = await _clienteService.UpdateDeuda(id, nuevaDeuda);
        if (!result) return NotFound();  // Si no existe, 404
        return NoContent();  // Si se actualizó, 204 No Content
    }

    // DELETE api/clientes/5 → Eliminar cliente
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _clienteService.DeleteCliente(id);
        if (!result) return NotFound();  // Si no existe, 404
        return NoContent();  // Si se eliminó, 204 No Content
    }
}