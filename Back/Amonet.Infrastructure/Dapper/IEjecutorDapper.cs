using System.Data;

namespace Amonet.Infrastructure.Dapper;

public interface IEjecutorDapper
{
    Task<IEnumerable<T>> ConsultarAsync<T>(string sql, object? parametros = null, CancellationToken cancellationToken = default);
    Task<T?> ConsultarPrimeroAsync<T>(string sql, object? parametros = null, CancellationToken cancellationToken = default);
    Task<int> EjecutarAsync(string sql, object? parametros = null, CancellationToken cancellationToken = default);
    Task<T> EjecutarEscalarAsync<T>(string sql, object? parametros = null, CancellationToken cancellationToken = default);
}

