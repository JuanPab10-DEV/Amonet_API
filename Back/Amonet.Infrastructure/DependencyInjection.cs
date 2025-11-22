using Microsoft.Extensions.DependencyInjection;
using Amonet.Infrastructure.Persistence;
using Amonet.Infrastructure.Dapper;

namespace Amonet.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructura(this IServiceCollection services)
    {
        services.AddSingleton<IFabricaConexionSql, FabricaConexionSql>();
        services.AddScoped<IEjecutorDapper, EjecutorDapper>();
        return services;
    }
}
