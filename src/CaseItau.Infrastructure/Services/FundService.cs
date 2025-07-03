using CaseItau.Domain.Common;
using CaseItau.Domain.Common.Errors;
using CaseItau.Domain.Entities;
using CaseItau.Domain.Interfaces.Repositories;
using CaseItau.Domain.Interfaces.Services;

namespace CaseItau.Infrastructure.Services
{
    public class FundService : IFundService
    {
        private readonly IFundRepository _fundRepository;
        
        public FundService(IFundRepository fundRepository)
        {
            _fundRepository = fundRepository;
        }

        public async Task<(Fund?, DomainError?)> CreateFundAsync(Fund fund)
        {
            if (fund.NetWorth < 0)
                return (null, DomainFundErrors.NegativeNetWorth);

            try
            {
                var existingFundByCode = await _fundRepository.GetFundByCodeAsync(fund.Code);
                if (existingFundByCode != null)
                    return (null, DomainFundErrors.DuplicateFundCode);

                var createdFund = await _fundRepository.CreateFundAsync(fund);
                return (createdFund, null);
            }
            catch
            {
                return (null, DomainFundErrors.DatabaseError);
            }
        }

        public async Task<(bool, DomainError?)> DeleteFundAsync(string code)
        {
            try
            {
                var notExistingFund = await _fundRepository.GetFundByCodeAsync(code);
                if (notExistingFund == null)
                    return (false, DomainFundErrors.FundNotFound);

                await _fundRepository.DeleteFundAsync(code);
                return (true, null);
            }
            catch (Exception)
            {
                return (false, DomainFundErrors.DatabaseError);
            }
        }

        public async Task<(Fund?, DomainError?)> GetFundByCodeAsync(string code)
        {
            try
            {
                var fund = await _fundRepository.GetFundByCodeAsync(code);
                if (fund == null)
                    return (null, DomainFundErrors.FundNotFound);

                return (fund, null);
            }
            catch (Exception)
            {
                return (null, DomainFundErrors.DatabaseError);
            }
        }

        public async Task<(IEnumerable<Fund>?, DomainError?)> ListAllFundsAsync()
        {
            try
            {
                var funds = await _fundRepository.ListAllFundsAsync();
                return (funds, null);
            }
            catch (Exception)
            {
                return (null, DomainFundErrors.DatabaseError);
            }
        }

        public async Task<(bool, DomainError?)> UpdateFundNetWorthAsync(string code, Fund fund)
        {
            try
            {
                var notExistingFund = await _fundRepository.GetFundByCodeAsync(code);
                if (notExistingFund == null)
                    return (false, DomainFundErrors.FundNotFound);

                var updatedFund = await _fundRepository.UpdateFundNetWorthAsync(code, fund);
                return (true, null);
            }
            catch (Exception)
            {
                return (false, DomainFundErrors.DatabaseError);
            }
        }

        public async Task<(bool, DomainError?)> UpdateFundAsync(string code, Fund fund)
        {
            var notExistingFund = await _fundRepository.GetFundByCodeAsync(code);
            if (notExistingFund == null)
                return (false, DomainFundErrors.FundNotFound);

            try
            {
                var updatedFund = await _fundRepository.UpdateFundAsync(code, fund);
                return (true, null);
            }
            catch (Exception)
            {
                return (false, DomainFundErrors.DatabaseError);
            }
        }
    }
}
