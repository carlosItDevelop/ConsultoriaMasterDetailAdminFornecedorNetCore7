using FluentValidation;

namespace Cooperchip.ITDeveloper.Farmacia.Domain.Entities.Validations
{
    public class RepresentanteLegalValidation : AbstractValidator<RepresentanteLegal>
    {
        public RepresentanteLegalValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 40).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.SobreNome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 40).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .MaximumLength(255).WithMessage("O campo {PropertyName} precisa ter, no máximo, {MaxLength} caracteres");

            RuleFor(c => c.Telefone)
                .NotEmpty().WithMessage("A campo {PropertyName} precisa ser fornecida")
                .Length(9, 13).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Documento)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(11, 14).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        }
    }
}
