namespace Amonet.Application.Citas.Buscar;

public record ListarCitasConsulta(
    string? Busqueda = null,
    int MaximoRegistros = 50
);

public record CitaBusquedaDto(
    Guid Id,
    Guid ClienteId,
    string ClienteNombre,
    string ClienteCedula,
    Guid ArtistaId,
    string ArtistaNombre,
    DateTime FechaInicio,
    DateTime FechaFin,
    string Estado
);


