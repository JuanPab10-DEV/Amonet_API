namespace Amonet.Application.Citas.Acciones;

public sealed class ActualizarEstadoCitaComando
{
    public Guid Id { get; init; }
    public string NuevoEstado { get; init; } = string.Empty;
    public string AccionAuditoria { get; init; } = string.Empty;
}
