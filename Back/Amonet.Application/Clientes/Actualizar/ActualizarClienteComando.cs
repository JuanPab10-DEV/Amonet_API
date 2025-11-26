namespace Amonet.Application.Clientes.Actualizar;

public record ActualizarClienteComando(
    Guid Id,
    string Cedula,
    string NombreCompleto,
    string? Correo,
    string? Telefono
);


