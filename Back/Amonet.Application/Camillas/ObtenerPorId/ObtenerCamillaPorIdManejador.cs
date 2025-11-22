using Amonet.Application.Abstractions;
using Amonet.Infrastructure.Dapper;

namespace Amonet.Application.Camillas.ObtenerPorId;

public class ObtenerCamillaPorIdManejador : IManejadorConsulta<ObtenerCamillaPorIdConsulta, CamillaDto>
{
    private readonly IEjecutorDapper _ejecutorDapper;

    public ObtenerCamillaPorIdManejador(IEjecutorDapper ejecutorDapper)
    {
        _ejecutorDapper = ejecutorDapper;
    }

    public async Task<CamillaDto> ManejarAsync(ObtenerCamillaPorIdConsulta consulta, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                Id,
                Codigo,
                Activa
            FROM dbo.Camillas
            WHERE Id = @Id";

        var parametros = new { consulta.Id };

        var camilla = await _ejecutorDapper.ConsultarPrimeroAsync<CamillaDto>(sql, parametros, cancellationToken);

        return camilla ?? throw new KeyNotFoundException($"No se encontró la camilla con ID {consulta.Id}");
    }
}

