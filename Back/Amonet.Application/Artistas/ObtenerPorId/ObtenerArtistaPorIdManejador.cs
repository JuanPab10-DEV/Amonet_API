using Amonet.Application.Abstractions;
using Amonet.Infrastructure.Dapper;

namespace Amonet.Application.Artistas.ObtenerPorId;

public class ObtenerArtistaPorIdManejador : IManejadorConsulta<ObtenerArtistaPorIdConsulta, ArtistaDto>
{
    private readonly IEjecutorDapper _ejecutorDapper;

    public ObtenerArtistaPorIdManejador(IEjecutorDapper ejecutorDapper)
    {
        _ejecutorDapper = ejecutorDapper;
    }

    public async Task<ArtistaDto> ManejarAsync(ObtenerArtistaPorIdConsulta consulta, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                Id,
                NombreArtistico,
                Estilos,
                Activo
            FROM dbo.Artistas
            WHERE Id = @Id";

        var parametros = new { consulta.Id };

        var artista = await _ejecutorDapper.ConsultarPrimeroAsync<ArtistaDto>(sql, parametros, cancellationToken);

        return artista ?? throw new KeyNotFoundException($"No se encontró el artista con ID {consulta.Id}");
    }
}

