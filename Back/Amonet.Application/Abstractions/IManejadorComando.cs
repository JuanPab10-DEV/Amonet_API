namespace Amonet.Application.Abstractions;

public interface IManejadorComando<in TComando, TResultado>
{
    Task<TResultado> ManejarAsync(TComando comando, CancellationToken cancellationToken = default);
}

