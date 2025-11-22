using Amonet.Application.Abstractions;
using Amonet.Infrastructure.Dapper;

namespace Amonet.Application.Auditorias;

public sealed class ObtenerAuditoriasManejador
    : IManejadorConsulta<ObtenerAuditoriasConsulta, IEnumerable<AuditoriaDto>>
{
    private readonly IEjecutorDapper _bd;

    public ObtenerAuditoriasManejador(IEjecutorDapper bd)
    {
        _bd = bd;
    }

    public async Task<IEnumerable<AuditoriaDto>> ManejarAsync(
        ObtenerAuditoriasConsulta consulta,
        CancellationToken cancellationToken = default)
    {
        const string sql = @"
SELECT TOP (@MaximoRegistros)
       Id,
       Accion,
       Fecha,
       Datos
FROM dbo.Auditorias
ORDER BY Fecha DESC;";

        var lista = await _bd.ConsultarAsync<AuditoriaDto>(
            sql,
            new { MaximoRegistros = consulta.MaximoRegistros },
            cancellationToken);

        return lista;
    }
}
