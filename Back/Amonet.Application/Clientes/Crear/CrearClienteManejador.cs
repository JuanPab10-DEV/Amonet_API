using Amonet.Application.Abstractions;
using Amonet.Application.Utilidades;
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
        // Verificar que la cédula no exista
        const string sqlVerificar = "SELECT COUNT(1) FROM dbo.Clientes WHERE Cedula = @Cedula";
        var existe = await _ejecutorDapper.EjecutarEscalarAsync<int>(sqlVerificar, new { comando.Cedula }, cancellationToken);
        
        if (existe > 0)
        {
            throw new InvalidOperationException("Ya existe un cliente con esta cédula");
        }

        var clienteId = Guid.NewGuid();
        
        // Capitalizar nombre según RAE
        var nombreCapitalizado = CapitalizacionHelper.CapitalizarSegunRAE(comando.NombreCompleto);
        
        const string sql = @"
            INSERT INTO dbo.Clientes (Id, Cedula, NombreCompleto, Correo, Telefono, FechaCreacion)
            VALUES (@Id, @Cedula, @NombreCompleto, @Correo, @Telefono, SYSUTCDATETIME())";

        var parametros = new
        {
            Id = clienteId,
            comando.Cedula,
            NombreCompleto = nombreCapitalizado,
            comando.Correo,
            comando.Telefono
        };

        await _ejecutorDapper.EjecutarAsync(sql, parametros, cancellationToken);
        
        return clienteId;
    }
}

