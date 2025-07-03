using CaseItau.Domain.Entities;

namespace CaseItau.Domain.Interfaces.Repositories
{
    public interface IFundRepository
    {
        Task<Fund> CreateFundAsync(Fund fund);
        Task<bool> DeleteFundAsync(string code);
        Task<Fund> GetFundByCodeAsync(string code);
        Task<IEnumerable<Fund>> ListAllFundsAsync();
        Task<Fund> UpdateFundNetWorthAsync(string code, Fund fund);
        Task<Fund> UpdateFundAsync(string code, Fund fund);
    }
}
