using CaseItau.Application.Common.Results;
using CaseItau.Application.Interfaces.UseCases;
using CaseItau.Application.Mappings;
using CaseItau.Domain.Interfaces.Services;
using FluentValidation;

namespace CaseItau.Application.UseCases
{
    public class DeleteFundUseCase : IDeleteFundUseCase
    {
        private readonly IFundService _fundService;
        private readonly IValidator<string> _validator;

        public DeleteFundUseCase(IFundService fundService, IValidator<string> validator)
        {
            _fundService = fundService;
            _validator = validator;
        }

        public async Task<Result<bool, Error?>> ExecuteAsync(string code)
        {
            var validated = await _validator.ValidateAsync(code);
            if (!validated.IsValid)
            {
                var errors = string.Join(", ", validated.Errors.Select(e => e.ErrorMessage));
                return Result<bool, Error?>.Failure(new Error("validation_error", errors, "BadRequest"));
            }

            var (success, domainError) = await _fundService.DeleteFundAsync(code);
            if (domainError != null)
            {
                var applicationError = ErrorMapping.MapToApplicationError(domainError);
                return Result<bool, Error?>.Failure(applicationError);
            }

            return Result<bool, Error?>.Success(true);
        }
    }
}
