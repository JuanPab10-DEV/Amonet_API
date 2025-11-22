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
        // Obtener información de la cita con nombres del cliente y artista
        const string sqlObtenerCita = @"
            SELECT 
                c.Id,
                c.Estado,
                cl.NombreCompleto AS ClienteNombre,
                cl.Cedula AS ClienteCedula,
                a.NombreArtistico AS ArtistaNombre
            FROM dbo.Citas c
            INNER JOIN dbo.Clientes cl ON c.ClienteId = cl.Id
            INNER JOIN dbo.Artistas a ON c.ArtistaId = a.Id
            WHERE c.Id = @Id";

        var cita = await _bd.ConsultarPrimeroAsync<dynamic>(sqlObtenerCita, new { comando.Id }, cancellationToken);

        if (cita == null)
        {
            throw new KeyNotFoundException("La cita no existe");
        }

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

        // Registrar auditoría con información detallada
        const string sqlAuditoria = @"
INSERT INTO dbo.Auditorias (Accion, Datos)
VALUES (@Accion, @Datos);";

        var datos = JsonSerializer.Serialize(new
        {
            CitaId = comando.Id,
            ClienteNombre = (string)cita.ClienteNombre,
            ClienteCedula = (string)cita.ClienteCedula,
            ArtistaNombre = (string)cita.ArtistaNombre,
            EstadoAnterior = (string)cita.Estado,
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
