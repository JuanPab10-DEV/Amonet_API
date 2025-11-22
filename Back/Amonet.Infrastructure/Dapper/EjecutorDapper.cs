using System.Data;
using Dapper;
using Amonet.Infrastructure.Persistence;

namespace Amonet.Infrastructure.Dapper;

public class EjecutorDapper : IEjecutorDapper
{
    private readonly IFabricaConexionSql _fabricaConexion;

    public EjecutorDapper(IFabricaConexionSql fabricaConexion)
    {
        _fabricaConexion = fabricaConexion;
    }

    public async Task<IEnumerable<T>> ConsultarAsync<T>(string sql, object? parametros = null, CancellationToken cancellationToken = default)
    {
        using var conexion = _fabricaConexion.CrearConexion();
        return await conexion.QueryAsync<T>(new CommandDefinition(sql, parametros, cancellationToken: cancellationToken));
    }

    public async Task<T?> ConsultarPrimeroAsync<T>(string sql, object? parametros = null, CancellationToken cancellationToken = default)
    {
        using var conexion = _fabricaConexion.CrearConexion();
        return await conexion.QueryFirstOrDefaultAsync<T>(new CommandDefinition(sql, parametros, cancellationToken: cancellationToken));
    }

    public async Task<int> EjecutarAsync(string sql, object? parametros = null, CancellationToken cancellationToken = default)
    {
        using var conexion = _fabricaConexion.CrearConexion();
        return await conexion.ExecuteAsync(new CommandDefinition(sql, parametros, cancellationToken: cancellationToken));
    }

    public async Task<T> EjecutarEscalarAsync<T>(string sql, object? parametros = null, CancellationToken cancellationToken = default)
    {
        using var conexion = _fabricaConexion.CrearConexion();
        var resultado = await conexion.ExecuteScalarAsync<T>(new CommandDefinition(sql, parametros, cancellationToken: cancellationToken));
        return resultado ?? throw new InvalidOperationException("El resultado de la consulta escalar fue null.");
    }
}

