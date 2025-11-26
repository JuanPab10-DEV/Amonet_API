using Amonet.Application.Abstractions;
using Amonet.Infrastructure.Dapper;

namespace Amonet.Application.Clientes.Eliminar;

public sealed class EliminarClienteManejador : IManejadorComando<EliminarClienteComando, bool>
{
    private readonly IEjecutorDapper _ejecutorDapper;

    public EliminarClienteManejador(IEjecutorDapper ejecutorDapper)
    {
        _ejecutorDapper = ejecutorDapper;
    }

    public async Task<bool> ManejarAsync(EliminarClienteComando comando, CancellationToken cancellationToken = default)
    {
        const string sqlExiste = "SELECT COUNT(1) FROM dbo.Clientes WHERE Id = @Id";
        var existe = await _ejecutorDapper.EjecutarEscalarAsync<int>(sqlExiste, new { comando.Id }, cancellationToken);
        if (existe == 0)
        {
            throw new KeyNotFoundException("El cliente no existe");
        }

        const string sqlCitas = "SELECT COUNT(1) FROM dbo.Citas WHERE ClienteId = @Id";
        var tieneCitas = await _ejecutorDapper.EjecutarEscalarAsync<int>(sqlCitas, new { comando.Id }, cancellationToken);
        if (tieneCitas > 0)
        {
            throw new InvalidOperationException("No se puede eliminar el cliente porque tiene citas asociadas");
        }

        const string sqlEliminar = "DELETE FROM dbo.Clientes WHERE Id = @Id";
        var filas = await _ejecutorDapper.EjecutarAsync(sqlEliminar, new { comando.Id }, cancellationToken);

        return filas > 0;
    }
}

