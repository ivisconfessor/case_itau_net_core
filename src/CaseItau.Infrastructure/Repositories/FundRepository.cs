using CaseItau.Domain.Entities;
using CaseItau.Domain.Interfaces.Repositories;
using CaseItau.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CaseItau.Infrastructure.Repositories
{
    public class FundRepository : IFundRepository
    {
        private readonly AppDbContext _appDbContext;

        public FundRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Fund> CreateFundAsync(Fund fund)
        {
            await _appDbContext.Funds.AddAsync(fund);

            await _appDbContext.SaveChangesAsync();

            return await GetFundByCodeAsync(fund.Code);
        }

        public async Task<bool> DeleteFundAsync(string code)
        {
            var fund = await _appDbContext.Funds.FindAsync(code);

            if (fund == null)
                return false;

            _appDbContext.Funds.Remove(fund);

            var affected = await _appDbContext.SaveChangesAsync();
            return affected > 0;
        }

        public async Task<Fund> GetFundByCodeAsync(string code)
        {
            return await _appDbContext.Funds
                .AsQueryable()
                .AsNoTracking()
                .Include(ft => ft.FundType)
                .FirstOrDefaultAsync(f => f.Code == code);
        }

        public async Task<IEnumerable<Fund>> ListAllFundsAsync()
        {
            return await _appDbContext.Funds
                .Include(ft => ft.FundType)
                .ToListAsync();
        }

        public async Task<Fund> UpdateFundAsync(string code, Fund fund)
        {
            var existingFund = await _appDbContext.Funds.FindAsync(code);

            if (existingFund == null)
                return null;

            existingFund.Name = fund.Name;
            existingFund.Cnpj = fund.Cnpj;
            existingFund.FundTypeId = fund.FundTypeId;
            existingFund.NetWorth = fund.NetWorth;
            existingFund.UpdatedAt = fund.UpdatedAt;

            await _appDbContext.SaveChangesAsync();

            return await GetFundByCodeAsync(code);
        }

        public async Task<Fund> UpdateFundNetWorthAsync(string code, Fund fund)
        {
            var existingFund = await _appDbContext.Funds.FindAsync(code);

            if (existingFund == null)
                return null;

            existingFund.NetWorth += fund.NetWorth;
            existingFund.UpdatedAt = fund.UpdatedAt;

            await _appDbContext.SaveChangesAsync();

            return await GetFundByCodeAsync(code);
        }
    }
}
