using CaseItau.Application.Common.Results;
using CaseItau.Application.DTOs.Requests;

namespace CaseItau.Application.Interfaces.UseCases
{
    public interface IUpdateFundNetWorthUseCase
    {
        Task<Result<bool, Error?>> ExecuteAsync(string code, UpdateNetWorthRequestDto requestDto);
    }
}
