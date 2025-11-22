using Microsoft.AspNetCore.Mvc;
using Amonet.Application.Abstractions;
using Amonet.Application.Auditorias;

namespace Amonet.Api.Controllers;

[ApiController]
[Route("api/auditorias")]
public sealed class AuditoriasController : ControllerBase
{
    private readonly IManejadorConsulta<ObtenerAuditoriasConsulta, IEnumerable<AuditoriaDto>> _manejador;

    public AuditoriasController(
        IManejadorConsulta<ObtenerAuditoriasConsulta, IEnumerable<AuditoriaDto>> manejador)
    {
        _manejador = manejador;
    }

    // GET api/auditorias?maximoRegistros=50
    [HttpGet]
    public async Task<IActionResult> Obtener([FromQuery] int maximoRegistros = 50, CancellationToken ct = default)
    {
        var consulta = new ObtenerAuditoriasConsulta
        {
            MaximoRegistros = maximoRegistros
        };

        var lista = await _manejador.ManejarAsync(consulta, ct);
        return Ok(lista);
    }
}
