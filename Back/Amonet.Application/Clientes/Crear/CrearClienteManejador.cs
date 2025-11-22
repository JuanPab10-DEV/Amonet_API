using Amonet.Application.Abstractions;
using Amonet.Infrastructure.Dapper;

namespace Amonet.Application.Clientes.Crear;

public class CrearClienteManejador : IManejadorComando<CrearClienteComando, Guid>
{
    private readonly IEjecutorDapper _ejecutorDapper;

    public CrearClienteManejador(IEjecutorDapper ejecutorDapper)
    {
        _ejecutorDapper = ejecutorDapper;
    }

    public async Task<Guid> ManejarAsync(CrearClienteComando comando, CancellationToken cancellationToken = default)
    {
        var clienteId = Guid.NewGuid();
        
        const string sql = @"
            INSERT INTO dbo.Clientes (Id, NombreCompleto, Correo, Telefono, FechaCreacion)
            VALUES (@Id, @NombreCompleto, @Correo, @Telefono, SYSUTCDATETIME())";

        var parametros = new
        {
            Id = clienteId,
            comando.NombreCompleto,
            comando.Correo,
            comando.Telefono
        };

        await _ejecutorDapper.EjecutarAsync(sql, parametros);
        
        return clienteId;
    }
}

