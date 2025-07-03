using CaseItau.Application.DTOs.Requests;
using FluentValidation;

namespace CaseItau.Application.Validators.Funds
{
    public class CreateFundRequestDtoValidator : AbstractValidator<CreateFundRequestDto>
    {
        public CreateFundRequestDtoValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("O código do fundo é obrigatório.")
                .MaximumLength(20).WithMessage("O código do fundo deve ter no máximo {MaxLength} caracteres.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome do fundo é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do fundo deve ter no máximo {MaxLength} caracteres.");

            RuleFor(x => x.Cnpj)
                .NotEmpty().WithMessage("O CNPJ do fundo é obrigatório.")
                .Length(14).WithMessage("O CNPJ do fundo deve ter exatamente 14 dígitos.")
                .Matches(@"^\d{14}$").WithMessage("O CNPJ deve conter apenas 14 dígitos numéricos, sem caracteres especiais.")
                .Must(BeAValidCnpj).WithMessage("O CNPJ informado não é válido.");

            RuleFor(x => x.FundTypeId)
                .NotEmpty().WithMessage("O tipo do fundo é obrigatório.")
                .GreaterThan(0).WithMessage("O tipo do fundo deve ser maior que zero.");

            RuleFor(x => x.InitialNetWorth)
                .GreaterThan(0).WithMessage("O patrimônio inicial deve ser maior que zero.");
        }

        private bool BeAValidCnpj(string cnpj)
        {
            // Remove caracteres não numéricos (caso ainda existam)
            cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

            // CNPJ deve ter 14 dígitos
            if (cnpj.Length != 14)
                return false;

            // Verifica se todos os dígitos são iguais
            if (cnpj.Distinct().Count() == 1)
                return false;

            // Cálculo do primeiro dígito verificador
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(cnpj[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            // Cálculo do segundo dígito verificador
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(cnpj[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            // Verifica se os dígitos calculados correspondem aos dígitos informados
            return cnpj.EndsWith(digito1.ToString() + digito2.ToString());
        }
    }
}
