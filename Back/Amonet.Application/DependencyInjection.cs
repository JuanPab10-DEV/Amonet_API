using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Amonet.Application.Abstractions;
using Amonet.Application.Clientes.Crear;
using Amonet.Application.Clientes.ObtenerPorId;
using Amonet.Application.Citas.Crear;
using Amonet.Application.Citas.Acciones;
using Amonet.Application.Auditorias;

namespace Amonet.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAplicacion(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        // Clientes
        services.AddScoped<IManejadorComando<CrearClienteComando, Guid>, CrearClienteManejador>();
        services.AddScoped<IManejadorConsulta<ObtenerClientePorIdConsulta, ClienteDto>, ObtenerClientePorIdManejador>();

        // Citas
        services.AddScoped<IManejadorComando<CrearCitaComando, Guid>, CrearCitaManejador>();
        services.AddScoped<IManejadorComando<ActualizarEstadoCitaComando, bool>, ActualizarEstadoCitaManejador>();

        // Auditorías
        services.AddScoped<IManejadorConsulta<ObtenerAuditoriasConsulta, IEnumerable<AuditoriaDto>>, ObtenerAuditoriasManejador>();

        return services;
    }
}
