using Microsoft.AspNetCore.Mvc;
using Amonet.Application.Abstractions;
using Amonet.Application.Artistas.ObtenerPorId;

namespace Amonet.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArtistasController : ControllerBase
{
    private readonly IManejadorConsulta<ObtenerArtistaPorIdConsulta, ArtistaDto> _obtenerArtistaManejador;

    public ArtistasController(
        IManejadorConsulta<ObtenerArtistaPorIdConsulta, ArtistaDto> obtenerArtistaManejador)
    {
        _obtenerArtistaManejador = obtenerArtistaManejador;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ArtistaDto>> ObtenerArtistaPorId(Guid id, CancellationToken cancellationToken)
    {
        var consulta = new ObtenerArtistaPorIdConsulta(id);
        var artista = await _obtenerArtistaManejador.ManejarAsync(consulta, cancellationToken);
        return Ok(artista);
    }
}


