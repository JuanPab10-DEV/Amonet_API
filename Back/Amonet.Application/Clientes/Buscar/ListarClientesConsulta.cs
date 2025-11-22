namespace Amonet.Application.Clientes.Buscar;

public record ListarClientesConsulta(
    string? Busqueda = null,
    int MaximoRegistros = 50
);

public record ClienteBusquedaDto(
    Guid Id,
    string Cedula,
    string NombreCompleto,
    string? Correo,
    string? Telefono
);

