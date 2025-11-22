using FluentValidation;

namespace Amonet.Application.Clientes.Crear;

public class CrearClienteComandoValidador : AbstractValidator<CrearClienteComando>
{
    public CrearClienteComandoValidador()
    {
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

