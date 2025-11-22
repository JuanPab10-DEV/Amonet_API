using Amonet.Application.Abstractions;
using Amonet.Application.Utilidades;
using Amonet.Infrastructure.Dapper;

namespace Amonet.Application.Clientes.Actualizar;

public class ActualizarClienteManejador : IManejadorComando<ActualizarClienteComando, bool>
{
    private readonly IEjecutorDapper _ejecutorDapper;

    public ActualizarClienteManejador(IEjecutorDapper ejecutorDapper)
    {
        _ejecutorDapper = ejecutorDapper;
    }

    public async Task<bool> ManejarAsync(ActualizarClienteComando comando, CancellationToken cancellationToken = default)
    {
        // Verificar que el cliente existe
        const string sqlExiste = "SELECT COUNT(1) FROM dbo.Clientes WHERE Id = @Id";
        var existe = await _ejecutorDapper.EjecutarEscalarAsync<int>(sqlExiste, new { comando.Id }, cancellationToken);
        
        if (existe == 0)
        {
            throw new KeyNotFoundException("El cliente no existe");
        }

        // Verificar que la cédula no esté en uso por otro cliente
        const string sqlCedula = "SELECT COUNT(1) FROM dbo.Clientes WHERE Cedula = @Cedula AND Id != @Id";
        var cedulaEnUso = await _ejecutorDapper.EjecutarEscalarAsync<int>(sqlCedula, new { comando.Cedula, comando.Id }, cancellationToken);
        
        if (cedulaEnUso > 0)
        {
            throw new InvalidOperationException("Ya existe otro cliente con esta cédula");
        }

        // Capitalizar nombre según RAE
        var nombreCapitalizado = CapitalizacionHelper.CapitalizarSegunRAE(comando.NombreCompleto);
        
        const string sql = @"
            UPDATE dbo.Clientes
            SET Cedula = @Cedula,
                NombreCompleto = @NombreCompleto,
                Correo = @Correo,
                Telefono = @Telefono,
                FechaActualizacion = SYSUTCDATETIME()
            WHERE Id = @Id";

        var parametros = new
        {
            comando.Id,
            comando.Cedula,
            NombreCompleto = nombreCapitalizado,
            comando.Correo,
            comando.Telefono
        };

        var filas = await _ejecutorDapper.EjecutarAsync(sql, parametros, cancellationToken);
        
        return filas > 0;
    }
}

