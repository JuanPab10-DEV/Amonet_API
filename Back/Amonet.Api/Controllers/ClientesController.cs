using Microsoft.AspNetCore.Mvc;
using Amonet.Application.Abstractions;
using Amonet.Application.Clientes.Crear;
using Amonet.Application.Clientes.ObtenerPorId;

namespace Amonet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IManejadorComando<CrearClienteComando, Guid> _crearClienteManejador;
    private readonly IManejadorConsulta<ObtenerClientePorIdConsulta, ClienteDto> _obtenerClienteManejador;

    public ClientesController(
        IManejadorComando<CrearClienteComando, Guid> crearClienteManejador,
        IManejadorConsulta<ObtenerClientePorIdConsulta, ClienteDto> obtenerClienteManejador)
    {
        _crearClienteManejador = crearClienteManejador;
        _obtenerClienteManejador = obtenerClienteManejador;
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
}

