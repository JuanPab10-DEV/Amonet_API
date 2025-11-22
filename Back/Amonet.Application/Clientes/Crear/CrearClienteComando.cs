namespace Amonet.Application.Clientes.Crear;

public record CrearClienteComando(
    string Cedula,
    string NombreCompleto,
    string? Correo,
    string? Telefono
);

