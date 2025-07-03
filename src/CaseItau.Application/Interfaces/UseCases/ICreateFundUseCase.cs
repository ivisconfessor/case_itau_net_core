using CaseItau.Application.Common.Results;
using CaseItau.Application.DTOs.Requests;
using CaseItau.Application.DTOs.Responses;

namespace CaseItau.Application.Interfaces.UseCases
{
    public interface ICreateFundUseCase
    {
        Task<Result<FundResponseDto?, Error?>> ExecuteAsync(CreateFundRequestDto requestDto);
    }
}
