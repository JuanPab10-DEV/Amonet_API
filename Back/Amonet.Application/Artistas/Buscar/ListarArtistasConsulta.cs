namespace Amonet.Application.Artistas.Buscar;

public record ListarArtistasConsulta(
    string? Busqueda = null,
    int MaximoRegistros = 50
);

public record ArtistaBusquedaDto(
    Guid Id,
    string NombreArtistico,
    string? Estilos,
    bool Activo
);


