using Amonet.Application.Abstractions;
using Amonet.Infrastructure.Dapper;

namespace Amonet.Application.Artistas.Buscar;

public class ListarArtistasManejador : IManejadorConsulta<ListarArtistasConsulta, IEnumerable<ArtistaBusquedaDto>>
{
    private readonly IEjecutorDapper _ejecutorDapper;

    public ListarArtistasManejador(IEjecutorDapper ejecutorDapper)
    {
        _ejecutorDapper = ejecutorDapper;
    }

    public async Task<IEnumerable<ArtistaBusquedaDto>> ManejarAsync(
        ListarArtistasConsulta consulta,
        CancellationToken cancellationToken = default)
    {
        string sql;
        object? parametros;

        if (string.IsNullOrWhiteSpace(consulta.Busqueda))
        {
            sql = @"
                SELECT TOP (@MaximoRegistros)
                    Id,
                    NombreArtistico,
                    Estilos,
                    Activo
                FROM dbo.Artistas
                WHERE Activo = 1
                ORDER BY NombreArtistico";
            
            parametros = new { consulta.MaximoRegistros };
        }
        else
        {
            sql = @"
                SELECT TOP (@MaximoRegistros)
                    Id,
                    NombreArtistico,
                    Estilos,
                    Activo
                FROM dbo.Artistas
                WHERE Activo = 1
                  AND (NombreArtistico LIKE @Busqueda OR Estilos LIKE @Busqueda)
                ORDER BY NombreArtistico";
            
            var busquedaPattern = $"%{consulta.Busqueda}%";
            parametros = new { Busqueda = busquedaPattern, consulta.MaximoRegistros };
        }

        return await _ejecutorDapper.ConsultarAsync<ArtistaBusquedaDto>(sql, parametros, cancellationToken);
    }
}

