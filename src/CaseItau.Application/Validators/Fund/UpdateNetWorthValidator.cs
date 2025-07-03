using CaseItau.Application.DTOs.Requests;
using FluentValidation;

namespace CaseItau.Application.Validators.Fund
{
    public class UpdateNetWorthValidator : AbstractValidator<UpdateNetWorthRequestDto>
    {
        public UpdateNetWorthValidator() 
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("O código do fundo não pode ser vazio.")
                .MaximumLength(20).WithMessage("O código do fundo não pode exceder 20 caracteres.");

            RuleFor(x => x.Amount)
                .NotNull().WithMessage("O valor da atualização do patrimônio não pode ser nulo.");
        }
    }
}
