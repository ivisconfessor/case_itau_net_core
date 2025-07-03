using CaseItau.Application.Common.Results;
using CaseItau.Application.DTOs.Requests;

namespace CaseItau.Application.Interfaces.UseCases
{
    public interface IUpdateFundUseCase
    {
        Task<Result<bool, Error?>> ExecuteAsync(string code, UpdateFundRequestDto requestDto);
    }
}
