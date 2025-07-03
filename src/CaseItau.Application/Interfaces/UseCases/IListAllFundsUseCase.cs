using CaseItau.Application.Common.Results;
using CaseItau.Application.DTOs.Responses;

namespace CaseItau.Application.Interfaces.UseCases
{
    public interface IListAllFundsUseCase
    {
        Task<Result<IEnumerable<FundResponseDto>, Error?>> ExecuteAsync();
    }
}
