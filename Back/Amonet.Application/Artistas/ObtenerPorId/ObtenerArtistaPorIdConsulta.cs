namespace Amonet.Application.Artistas.ObtenerPorId;

public record ObtenerArtistaPorIdConsulta(Guid Id);

public record ArtistaDto(
    Guid Id,
    string NombreArtistico,
    string? Estilos,
    bool Activo
);


