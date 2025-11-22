using System.Text.Json;
using Amonet.Application.Abstractions;
using Amonet.Infrastructure.Dapper;

namespace Amonet.Application.Citas.Acciones;

public sealed class ActualizarEstadoCitaManejador
    : IManejadorComando<ActualizarEstadoCitaComando, bool>
{
    private readonly IEjecutorDapper _bd;

    public ActualizarEstadoCitaManejador(IEjecutorDapper bd)
    {
        _bd = bd;
    }

    public async Task<bool> ManejarAsync(
        ActualizarEstadoCitaComando comando,
        CancellationToken cancellationToken = default)
    {
        const string sqlUpdate = @"
UPDATE dbo.Citas
SET Estado = @NuevoEstado
WHERE Id = @Id;";

        var filas = await _bd.EjecutarAsync(
            sqlUpdate,
            new { comando.Id, comando.NuevoEstado },
            cancellationToken);

        if (filas == 0)
        {
            throw new KeyNotFoundException("La cita no existe");
        }

        const string sqlAuditoria = @"
INSERT INTO dbo.Auditorias (Accion, Datos)
VALUES (@Accion, @Datos);";

        var datos = JsonSerializer.Serialize(new
        {
            CitaId = comando.Id,
            NuevoEstado = comando.NuevoEstado
        });

        await _bd.EjecutarAsync(
            sqlAuditoria,
            new
            {
                Accion = comando.AccionAuditoria,
                Datos = datos
            },
            cancellationToken);

        return true;
    }
}
