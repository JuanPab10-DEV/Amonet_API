namespace Amonet.Application.Citas;

public sealed class CitaDto
{
    public Guid Id { get; init; }
    public Guid ClienteId { get; init; }
    public Guid ArtistaId { get; init; }
    public Guid CamillaId { get; init; }
    public DateTime FechaInicio { get; init; }
    public DateTime FechaFin { get; init; }
    public string Estado { get; init; } = string.Empty;
    public DateTime FechaCreacion { get; init; }
}
