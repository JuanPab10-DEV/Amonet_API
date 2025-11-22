using Microsoft.AspNetCore.Mvc;
using Amonet.Application.Abstractions;
using Amonet.Application.Clientes.Buscar;
using Amonet.Application.Artistas.Buscar;
using Amonet.Application.Camillas.Buscar;
using Amonet.Application.Citas.Buscar;

namespace Amonet.Api.Controllers;

[ApiController]
[Route("api/busqueda")]
public class BusquedaController : ControllerBase
{
    private readonly IManejadorConsulta<ListarClientesConsulta, IEnumerable<ClienteBusquedaDto>> _listarClientes;
    private readonly IManejadorConsulta<ListarArtistasConsulta, IEnumerable<ArtistaBusquedaDto>> _listarArtistas;
    private readonly IManejadorConsulta<ListarCamillasConsulta, IEnumerable<CamillaBusquedaDto>> _listarCamillas;
    private readonly IManejadorConsulta<ListarCitasConsulta, IEnumerable<CitaBusquedaDto>> _listarCitas;

    public BusquedaController(
        IManejadorConsulta<ListarClientesConsulta, IEnumerable<ClienteBusquedaDto>> listarClientes,
        IManejadorConsulta<ListarArtistasConsulta, IEnumerable<ArtistaBusquedaDto>> listarArtistas,
        IManejadorConsulta<ListarCamillasConsulta, IEnumerable<CamillaBusquedaDto>> listarCamillas,
        IManejadorConsulta<ListarCitasConsulta, IEnumerable<CitaBusquedaDto>> listarCitas)
    {
        _listarClientes = listarClientes;
        _listarArtistas = listarArtistas;
        _listarCamillas = listarCamillas;
        _listarCitas = listarCitas;
    }

    [HttpGet("clientes")]
    public async Task<ActionResult<IEnumerable<ClienteBusquedaDto>>> BuscarClientes(
        [FromQuery] string? busqueda = null,
        [FromQuery] int maximoRegistros = 50,
        CancellationToken cancellationToken = default)
    {
        var consulta = new ListarClientesConsulta(busqueda, maximoRegistros);
        var resultado = await _listarClientes.ManejarAsync(consulta, cancellationToken);
        return Ok(resultado);
    }

    [HttpGet("artistas")]
    public async Task<ActionResult<IEnumerable<ArtistaBusquedaDto>>> BuscarArtistas(
        [FromQuery] string? busqueda = null,
        [FromQuery] int maximoRegistros = 50,
        CancellationToken cancellationToken = default)
    {
        var consulta = new ListarArtistasConsulta(busqueda, maximoRegistros);
        var resultado = await _listarArtistas.ManejarAsync(consulta, cancellationToken);
        return Ok(resultado);
    }

    [HttpGet("camillas")]
    public async Task<ActionResult<IEnumerable<CamillaBusquedaDto>>> BuscarCamillas(
        [FromQuery] string? busqueda = null,
        [FromQuery] int maximoRegistros = 50,
        CancellationToken cancellationToken = default)
    {
        var consulta = new ListarCamillasConsulta(busqueda, maximoRegistros);
        var resultado = await _listarCamillas.ManejarAsync(consulta, cancellationToken);
        return Ok(resultado);
    }

    [HttpGet("citas")]
    public async Task<ActionResult<IEnumerable<CitaBusquedaDto>>> BuscarCitas(
        [FromQuery] string? busqueda = null,
        [FromQuery] int maximoRegistros = 50,
        CancellationToken cancellationToken = default)
    {
        var consulta = new ListarCitasConsulta(busqueda, maximoRegistros);
        var resultado = await _listarCitas.ManejarAsync(consulta, cancellationToken);
        return Ok(resultado);
    }
}

