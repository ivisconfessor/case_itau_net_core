using CaseItau.Application.DTOs.Requests;
using FluentValidation;

namespace CaseItau.Application.Validators.Fund
{
    public class UpdateFundRequestDtoValidator : AbstractValidator<UpdateFundRequestDto>
    {
        public UpdateFundRequestDtoValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("O código do fundo é obrigatório.")
                .MaximumLength(20).WithMessage("O código do fundo deve ter no máximo {MaxLength} caracteres.");

            // Validação do Name (opcional, caso seja fornecido)
            When(x => !string.IsNullOrWhiteSpace(x.Name), () => {
                RuleFor(x => x.Name)
                    .MaximumLength(100).WithMessage("O nome do fundo não pode exceder 100 caracteres.");
            });

            // Validação do FundTypeId (opcional, caso seja fornecido)
            When(x => x.FundTypeId > 0, () => {
                RuleFor(x => x.FundTypeId)
                    .GreaterThan(0).WithMessage("O código do tipo de fundo deve ser maior que zero.");
            });
        }
    }
}
