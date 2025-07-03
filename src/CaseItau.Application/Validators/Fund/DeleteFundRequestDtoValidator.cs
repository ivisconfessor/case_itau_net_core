using FluentValidation;

namespace CaseItau.Application.Validators.Fund
{
    public class DeleteFundRequestDtoValidator : AbstractValidator<string>
    {
        public DeleteFundRequestDtoValidator() 
        {
            RuleFor(code => code)
                .NotEmpty().WithMessage("O código do fundo não pode ser vazio.")
                .MaximumLength(20).WithMessage("O código do fundo não pode exceder 20 caracteres.");
        }
    }
}
