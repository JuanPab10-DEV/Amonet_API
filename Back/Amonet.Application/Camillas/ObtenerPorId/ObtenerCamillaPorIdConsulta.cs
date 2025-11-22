namespace Amonet.Application.Camillas.ObtenerPorId;

public record ObtenerCamillaPorIdConsulta(Guid Id);

public record CamillaDto(
    Guid Id,
    string Codigo,
    bool Activa
);

