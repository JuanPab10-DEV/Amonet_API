namespace Amonet.Application.Abstractions;

public interface IManejadorConsulta<in TConsulta, TResultado>
{
    Task<TResultado> ManejarAsync(TConsulta consulta, CancellationToken cancellationToken = default);
}

