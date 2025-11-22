namespace Amonet.Application.Clientes.Crear;

public record CrearClienteComando(
    string NombreCompleto,
    string? Correo,
    string? Telefono
);

