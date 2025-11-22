namespace Amonet.Application.Citas.Crear;

public sealed class CrearCitaComando
{
    public Guid ClienteId { get; init; }
    public Guid ArtistaId { get; init; }
    public Guid CamillaId { get; init; }
    public DateTime FechaInicio { get; init; }
    public DateTime FechaFin { get; init; }
}
