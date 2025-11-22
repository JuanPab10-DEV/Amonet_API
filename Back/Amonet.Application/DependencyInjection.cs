using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Amonet.Application.Abstractions;
using Amonet.Application.Clientes.Crear;
using Amonet.Application.Clientes.ObtenerPorId;
using Amonet.Application.Clientes.Buscar;
using Amonet.Application.Clientes.Actualizar;
using Amonet.Application.Artistas.Buscar;
using Amonet.Application.Artistas.ObtenerPorId;
using Amonet.Application.Camillas.Buscar;
using Amonet.Application.Camillas.ObtenerPorId;
using Amonet.Application.Citas.Crear;
using Amonet.Application.Citas.Acciones;
using Amonet.Application.Citas.Buscar;
using Amonet.Application.Auditorias;

namespace Amonet.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAplicacion(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        // Clientes
        services.AddScoped<IManejadorComando<CrearClienteComando, Guid>, CrearClienteManejador>();
        services.AddScoped<IManejadorComando<ActualizarClienteComando, bool>, ActualizarClienteManejador>();
        services.AddScoped<IManejadorConsulta<ObtenerClientePorIdConsulta, ClienteDto>, ObtenerClientePorIdManejador>();
        services.AddScoped<IManejadorConsulta<ListarClientesConsulta, IEnumerable<ClienteBusquedaDto>>, ListarClientesManejador>();
        
        // Artistas
        services.AddScoped<IManejadorConsulta<ObtenerArtistaPorIdConsulta, ArtistaDto>, ObtenerArtistaPorIdManejador>();
        services.AddScoped<IManejadorConsulta<ListarArtistasConsulta, IEnumerable<ArtistaBusquedaDto>>, ListarArtistasManejador>();
        
        // Camillas
        services.AddScoped<IManejadorConsulta<ObtenerCamillaPorIdConsulta, CamillaDto>, ObtenerCamillaPorIdManejador>();
        services.AddScoped<IManejadorConsulta<ListarCamillasConsulta, IEnumerable<CamillaBusquedaDto>>, ListarCamillasManejador>();

        // Citas
        services.AddScoped<IManejadorComando<CrearCitaComando, Guid>, CrearCitaManejador>();
        services.AddScoped<IManejadorComando<ActualizarEstadoCitaComando, bool>, ActualizarEstadoCitaManejador>();
        services.AddScoped<IManejadorConsulta<ListarCitasConsulta, IEnumerable<CitaBusquedaDto>>, ListarCitasManejador>();

        // Auditorías
        services.AddScoped<IManejadorConsulta<ObtenerAuditoriasConsulta, IEnumerable<AuditoriaDto>>, ObtenerAuditoriasManejador>();

        return services;
    }
}
