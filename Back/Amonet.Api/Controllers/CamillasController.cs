using Microsoft.AspNetCore.Mvc;
using Amonet.Application.Abstractions;
using Amonet.Application.Camillas.ObtenerPorId;

namespace Amonet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CamillasController : ControllerBase
{
    private readonly IManejadorConsulta<ObtenerCamillaPorIdConsulta, CamillaDto> _obtenerCamillaManejador;

    public CamillasController(
        IManejadorConsulta<ObtenerCamillaPorIdConsulta, CamillaDto> obtenerCamillaManejador)
    {
        _obtenerCamillaManejador = obtenerCamillaManejador;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CamillaDto>> ObtenerCamillaPorId(Guid id, CancellationToken cancellationToken)
    {
        var consulta = new ObtenerCamillaPorIdConsulta(id);
        var camilla = await _obtenerCamillaManejador.ManejarAsync(consulta, cancellationToken);
        return Ok(camilla);
    }
}

