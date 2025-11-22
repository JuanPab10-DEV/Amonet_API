using Amonet.Application.Abstractions;
using Amonet.Infrastructure.Dapper;

namespace Amonet.Application.Citas.Buscar;

public class ListarCitasManejador : IManejadorConsulta<ListarCitasConsulta, IEnumerable<CitaBusquedaDto>>
{
    private readonly IEjecutorDapper _ejecutorDapper;

    public ListarCitasManejador(IEjecutorDapper ejecutorDapper)
    {
        _ejecutorDapper = ejecutorDapper;
    }

    public async Task<IEnumerable<CitaBusquedaDto>> ManejarAsync(
        ListarCitasConsulta consulta,
        CancellationToken cancellationToken = default)
    {
        string sql;
        object? parametros;

        if (string.IsNullOrWhiteSpace(consulta.Busqueda))
        {
            sql = @"
                SELECT TOP (@MaximoRegistros)
                    c.Id,
                    c.ClienteId,
                    cl.NombreCompleto AS ClienteNombre,
                    cl.Cedula AS ClienteCedula,
                    c.ArtistaId,
                    a.NombreArtistico AS ArtistaNombre,
                    c.FechaInicio,
                    c.FechaFin,
                    c.Estado
                FROM dbo.Citas c
                INNER JOIN dbo.Clientes cl ON c.ClienteId = cl.Id
                INNER JOIN dbo.Artistas a ON c.ArtistaId = a.Id
                ORDER BY c.FechaInicio DESC";
            
            parametros = new { consulta.MaximoRegistros };
        }
        else
        {
            sql = @"
                SELECT TOP (@MaximoRegistros)
                    c.Id,
                    c.ClienteId,
                    cl.NombreCompleto AS ClienteNombre,
                    cl.Cedula AS ClienteCedula,
                    c.ArtistaId,
                    a.NombreArtistico AS ArtistaNombre,
                    c.FechaInicio,
                    c.FechaFin,
                    c.Estado
                FROM dbo.Citas c
                INNER JOIN dbo.Clientes cl ON c.ClienteId = cl.Id
                INNER JOIN dbo.Artistas a ON c.ArtistaId = a.Id
                WHERE cl.NombreCompleto LIKE @Busqueda
                   OR cl.Cedula LIKE @Busqueda
                   OR a.NombreArtistico LIKE @Busqueda
                ORDER BY c.FechaInicio DESC";
            
            var busquedaPattern = $"%{consulta.Busqueda}%";
            parametros = new { Busqueda = busquedaPattern, consulta.MaximoRegistros };
        }

        return await _ejecutorDapper.ConsultarAsync<CitaBusquedaDto>(sql, parametros, cancellationToken);
    }
}

