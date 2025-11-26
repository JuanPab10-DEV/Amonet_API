namespace Amonet.Application.Camillas.Buscar;

public record ListarCamillasConsulta(
    string? Busqueda = null,
    int MaximoRegistros = 50
);

public record CamillaBusquedaDto(
    Guid Id,
    string Codigo,
    bool Activa
);


