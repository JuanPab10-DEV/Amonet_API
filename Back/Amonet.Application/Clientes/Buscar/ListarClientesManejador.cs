using Amonet.Application.Abstractions;
using Amonet.Infrastructure.Dapper;

namespace Amonet.Application.Clientes.Buscar;

public class ListarClientesManejador : IManejadorConsulta<ListarClientesConsulta, IEnumerable<ClienteBusquedaDto>>
{
    private readonly IEjecutorDapper _ejecutorDapper;

    public ListarClientesManejador(IEjecutorDapper ejecutorDapper)
    {
        _ejecutorDapper = ejecutorDapper;
    }

    public async Task<IEnumerable<ClienteBusquedaDto>> ManejarAsync(
        ListarClientesConsulta consulta,
        CancellationToken cancellationToken = default)
    {
        string sql;
        object? parametros;

        if (string.IsNullOrWhiteSpace(consulta.Busqueda))
        {
            sql = @"
                SELECT TOP (@MaximoRegistros)
                    Id,
                    Cedula,
                    NombreCompleto,
                    Correo,
                    Telefono
                FROM dbo.Clientes
                ORDER BY NombreCompleto";
            
            parametros = new { consulta.MaximoRegistros };
        }
        else
        {
            sql = @"
                SELECT TOP (@MaximoRegistros)
                    Id,
                    Cedula,
                    NombreCompleto,
                    Correo,
                    Telefono
                FROM dbo.Clientes
                WHERE NombreCompleto LIKE @Busqueda
                   OR Cedula LIKE @Busqueda
                   OR Correo LIKE @Busqueda
                   OR Telefono LIKE @Busqueda
                ORDER BY NombreCompleto";
            
            var busquedaPattern = $"%{consulta.Busqueda}%";
            parametros = new { Busqueda = busquedaPattern, consulta.MaximoRegistros };
        }

        return await _ejecutorDapper.ConsultarAsync<ClienteBusquedaDto>(sql, parametros, cancellationToken);
    }
}

