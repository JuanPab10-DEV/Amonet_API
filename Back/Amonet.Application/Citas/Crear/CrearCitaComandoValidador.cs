using FluentValidation;

namespace Amonet.Application.Citas.Crear;

public sealed class CrearCitaComandoValidador : AbstractValidator<CrearCitaComando>
{
    public CrearCitaComandoValidador()
    {
        RuleFor(x => x.ClienteId)
            .NotEmpty();

        RuleFor(x => x.ArtistaId)
            .NotEmpty();

        RuleFor(x => x.CamillaId)
            .NotEmpty();

        RuleFor(x => x.FechaInicio)
            .LessThan(x => x.FechaFin)
            .WithMessage("La fecha de inicio debe ser menor que la fecha de fin");
    }
}
