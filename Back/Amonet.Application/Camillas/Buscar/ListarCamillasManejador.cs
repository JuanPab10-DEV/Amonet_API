using Amonet.Application.Abstractions;
using Amonet.Infrastructure.Dapper;

namespace Amonet.Application.Camillas.Buscar;

public class ListarCamillasManejador : IManejadorConsulta<ListarCamillasConsulta, IEnumerable<CamillaBusquedaDto>>
{
    private readonly IEjecutorDapper _ejecutorDapper;

    public ListarCamillasManejador(IEjecutorDapper ejecutorDapper)
    {
        _ejecutorDapper = ejecutorDapper;
    }

    public async Task<IEnumerable<CamillaBusquedaDto>> ManejarAsync(
        ListarCamillasConsulta consulta,
        CancellationToken cancellationToken = default)
    {
        string sql;
        object? parametros;

        if (string.IsNullOrWhiteSpace(consulta.Busqueda))
        {
            sql = @"
                SELECT TOP (@MaximoRegistros)
                    Id,
                    Codigo,
                    Activa
                FROM dbo.Camillas
                WHERE Activa = 1
                ORDER BY Codigo";
            
            parametros = new { consulta.MaximoRegistros };
        }
        else
        {
            sql = @"
                SELECT TOP (@MaximoRegistros)
                    Id,
                    Codigo,
                    Activa
                FROM dbo.Camillas
                WHERE Activa = 1
                  AND Codigo LIKE @Busqueda
                ORDER BY Codigo";
            
            var busquedaPattern = $"%{consulta.Busqueda}%";
            parametros = new { Busqueda = busquedaPattern, consulta.MaximoRegistros };
        }

        return await _ejecutorDapper.ConsultarAsync<CamillaBusquedaDto>(sql, parametros, cancellationToken);
    }
}

