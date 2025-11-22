using FluentValidation;

namespace Amonet.Application.Clientes.Actualizar;

public class ActualizarClienteComandoValidador : AbstractValidator<ActualizarClienteComando>
{
    public ActualizarClienteComandoValidador()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("El ID es requerido");

        RuleFor(x => x.Cedula)
            .NotEmpty().WithMessage("La cédula es requerida")
            .MaximumLength(20).WithMessage("La cédula no puede exceder 20 caracteres");

        RuleFor(x => x.NombreCompleto)
            .NotEmpty().WithMessage("El nombre completo es requerido")
            .MaximumLength(150).WithMessage("El nombre completo no puede exceder 150 caracteres");

        RuleFor(x => x.Correo)
            .EmailAddress().WithMessage("El correo electrónico no es válido")
            .When(x => !string.IsNullOrWhiteSpace(x.Correo))
            .MaximumLength(150).WithMessage("El correo no puede exceder 150 caracteres");

        RuleFor(x => x.Telefono)
            .MaximumLength(50).WithMessage("El teléfono no puede exceder 50 caracteres")
            .When(x => !string.IsNullOrWhiteSpace(x.Telefono));
    }
}

