namespace Amonet.Application.Clientes.ObtenerPorId;

public record ClienteDto(
    Guid Id,
    string NombreCompleto,
    string? Correo,
    string? Telefono,
    DateTime FechaCreacion,
    DateTime? FechaActualizacion
);

