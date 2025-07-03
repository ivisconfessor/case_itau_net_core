using CaseItau.Domain.Common;
using CaseItau.Domain.Entities;

namespace CaseItau.Domain.Interfaces.Services
{
    public interface IFundService
    {
        Task<(Fund? funds, DomainError? domainError)> CreateFundAsync(Fund fund);
        Task<(bool deleteSuccess, DomainError? domainError)> DeleteFundAsync(string code);
        Task<(Fund? funds, DomainError? domainError)> GetFundByCodeAsync(string code);
        Task<(IEnumerable<Fund>? funds, DomainError? domainError)> ListAllFundsAsync();
        Task<(bool updateFundNetWorthSuccess, DomainError? domainError)> UpdateFundNetWorthAsync(string code, Fund fund);
        Task<(bool updateFundSuccess, DomainError? domainError)> UpdateFundAsync(string code, Fund fund);
    }
}
