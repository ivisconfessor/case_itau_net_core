using CaseItau.Application.Common.Results;
using CaseItau.Application.DTOs.Responses;

namespace CaseItau.Application.Interfaces.UseCases
{
    public interface IGetFundByCodeUseCase
    {
        Task<Result<FundResponseDto?, Error?>> ExecuteAsync(string code);
    }
}
