using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Amonet.Application.Abstractions;
using Amonet.Application.Clientes.Crear;
using Amonet.Application.Clientes.ObtenerPorId;

namespace Amonet.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAplicacion(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddScoped<IManejadorComando<CrearClienteComando, Guid>, CrearClienteManejador>();
        services.AddScoped<IManejadorConsulta<ObtenerClientePorIdConsulta, ClienteDto>, ObtenerClientePorIdManejador>();

        return services;
    }
}
