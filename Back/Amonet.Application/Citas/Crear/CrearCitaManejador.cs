using System.Text.Json;
using Amonet.Application.Abstractions;
using Amonet.Infrastructure.Dapper;

namespace Amonet.Application.Citas.Crear;

public sealed class CrearCitaManejador
    : IManejadorComando<CrearCitaComando, Guid>
{
    private readonly IEjecutorDapper _bd;

    public CrearCitaManejador(IEjecutorDapper bd)
    {
        _bd = bd;
    }

    public async Task<Guid> ManejarAsync(
        CrearCitaComando comando,
        CancellationToken cancellationToken = default)
    {
        // Verificar que el cliente existe
        const string sqlExisteCliente = "SELECT COUNT(1) FROM dbo.Clientes WHERE Id = @Id;";
        var existeCliente = await _bd.EjecutarEscalarAsync<int>(
            sqlExisteCliente,
            new { Id = comando.ClienteId },
            cancellationToken);

        if (existeCliente == 0)
        {
            throw new KeyNotFoundException("El cliente no existe");
        }

        // Verificar que el artista existe
        const string sqlExisteArtista = "SELECT COUNT(1) FROM dbo.Artistas WHERE Id = @Id;";
        var existeArtista = await _bd.EjecutarEscalarAsync<int>(
            sqlExisteArtista,
            new { Id = comando.ArtistaId },
            cancellationToken);

        if (existeArtista == 0)
        {
            throw new KeyNotFoundException("El artista no existe");
        }

        // Verificar que la camilla existe
        const string sqlExisteCamilla = "SELECT COUNT(1) FROM dbo.Camillas WHERE Id = @Id;";
        var existeCamilla = await _bd.EjecutarEscalarAsync<int>(
            sqlExisteCamilla,
            new { Id = comando.CamillaId },
            cancellationToken);

        if (existeCamilla == 0)
        {
            throw new KeyNotFoundException("La camilla no existe");
        }

        // Verificar disponibilidad de camilla en el intervalo
        const string sqlConflictos = @"
SELECT COUNT(1)
FROM dbo.Citas
WHERE CamillaId = @CamillaId
  AND Estado IN (N'Creada', N'Confirmada', N'EnCurso')
  AND FechaInicio < @FechaFin
  AND FechaFin > @FechaInicio;";

        var conflictos = await _bd.EjecutarEscalarAsync<int>(
            sqlConflictos,
            new
            {
                comando.CamillaId,
                comando.FechaInicio,
                comando.FechaFin
            },
            cancellationToken);

        if (conflictos > 0)
        {
            throw new InvalidOperationException("La camilla no está disponible en ese horario");
        }

        var id = Guid.NewGuid();

        const string sqlInsert = @"
INSERT INTO dbo.Citas
    (Id, ClienteId, ArtistaId, CamillaId, FechaInicio, FechaFin, Estado, FechaCreacion)
VALUES
    (@Id, @ClienteId, @ArtistaId, @CamillaId, @FechaInicio, @FechaFin, N'Creada', SYSUTCDATETIME());";

        await _bd.EjecutarAsync(
            sqlInsert,
            new
            {
                Id = id,
                comando.ClienteId,
                comando.ArtistaId,
                comando.CamillaId,
                comando.FechaInicio,
                comando.FechaFin
            },
            cancellationToken);

        // Registrar auditoría
        const string sqlAuditoria = @"
INSERT INTO dbo.Auditorias (Accion, Datos)
VALUES (@Accion, @Datos);";

        var datos = JsonSerializer.Serialize(new
        {
            CitaId = id,
            comando.ClienteId,
            comando.ArtistaId,
            comando.CamillaId,
            comando.FechaInicio,
            comando.FechaFin
        });

        await _bd.EjecutarAsync(
            sqlAuditoria,
            new
            {
                Accion = "Cita creada",
                Datos = datos
            },
            cancellationToken);

        return id;
    }
}
