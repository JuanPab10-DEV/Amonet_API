using Amonet.Application.Abstractions;
using Amonet.Infrastructure.Dapper;

namespace Amonet.Application.Clientes.ObtenerPorId;

public class ObtenerClientePorIdManejador : IManejadorConsulta<ObtenerClientePorIdConsulta, ClienteDto>
{
    private readonly IEjecutorDapper _ejecutorDapper;

    public ObtenerClientePorIdManejador(IEjecutorDapper ejecutorDapper)
    {
        _ejecutorDapper = ejecutorDapper;
    }

    public async Task<ClienteDto> ManejarAsync(ObtenerClientePorIdConsulta consulta, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                Id,
                Cedula,
                NombreCompleto,
                Correo,
                Telefono,
                FechaCreacion,
                FechaActualizacion
            FROM dbo.Clientes
            WHERE Id = @Id";

        var parametros = new { consulta.Id };

        var cliente = await _ejecutorDapper.ConsultarPrimeroAsync<ClienteDto>(sql, parametros);

        return cliente ?? throw new KeyNotFoundException($"No se encontró el cliente con ID {consulta.Id}");
    }
}

