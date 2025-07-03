using CaseItau.Application.Common.Results;

namespace CaseItau.Application.Interfaces.UseCases
{
    public interface IDeleteFundUseCase
    {
        Task<Result<bool, Error?>> ExecuteAsync(string code);
    }
}
