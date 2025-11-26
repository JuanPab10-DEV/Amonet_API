using Microsoft.AspNetCore.Mvc;
using Amonet.Application.Abstractions;
using Amonet.Application.Clientes.Crear;
using Amonet.Application.Clientes.ObtenerPorId;
using Amonet.Application.Clientes.Actualizar;
using Amonet.Application.Clientes.Eliminar;

namespace Amonet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IManejadorComando<CrearClienteComando, Guid> _crearClienteManejador;
    private readonly IManejadorComando<ActualizarClienteComando, bool> _actualizarClienteManejador;
    private readonly IManejadorConsulta<ObtenerClientePorIdConsulta, ClienteDto> _obtenerClienteManejador;
    private readonly IManejadorComando<EliminarClienteComando, bool> _eliminarClienteManejador;

    public ClientesController(
        IManejadorComando<CrearClienteComando, Guid> crearClienteManejador,
        IManejadorComando<ActualizarClienteComando, bool> actualizarClienteManejador,
        IManejadorConsulta<ObtenerClientePorIdConsulta, ClienteDto> obtenerClienteManejador,
        IManejadorComando<EliminarClienteComando, bool> eliminarClienteManejador)
    {
        _crearClienteManejador = crearClienteManejador;
        _actualizarClienteManejador = actualizarClienteManejador;
        _obtenerClienteManejador = obtenerClienteManejador;
        _eliminarClienteManejador = eliminarClienteManejador;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CrearCliente([FromBody] CrearClienteComando comando, CancellationToken cancellationToken)
    {
        var clienteId = await _crearClienteManejador.ManejarAsync(comando, cancellationToken);
        return Ok(clienteId);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClienteDto>> ObtenerClientePorId(Guid id, CancellationToken cancellationToken)
    {
        var consulta = new ObtenerClientePorIdConsulta(id);
        var cliente = await _obtenerClienteManejador.ManejarAsync(consulta, cancellationToken);
        return Ok(cliente);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> ActualizarCliente(Guid id, [FromBody] ActualizarClienteComando comando, CancellationToken cancellationToken)
    {
        var comandoConId = comando with { Id = id };
        await _actualizarClienteManejador.ManejarAsync(comandoConId, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarCliente(Guid id, CancellationToken cancellationToken)
    {
        var comando = new EliminarClienteComando(id);
        await _eliminarClienteManejador.ManejarAsync(comando, cancellationToken);
        return NoContent();
    }
}

