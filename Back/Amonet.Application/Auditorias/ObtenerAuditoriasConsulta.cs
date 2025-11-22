namespace Amonet.Application.Auditorias;

public sealed class ObtenerAuditoriasConsulta
{
    public int MaximoRegistros { get; init; } = 50;
}

public sealed class AuditoriaDto
{
    public long Id { get; init; }
    public string Accion { get; init; } = string.Empty;
    public DateTime Fecha { get; init; }
    public string? Datos { get; init; }
}
