using CaseItau.Application.Common.Errors;
using CaseItau.Application.Common.Results;
using CaseItau.Domain.Common;

namespace CaseItau.Application.Mappings
{
    public static class ErrorMapping
    {
        public static Error MapToApplicationError(DomainError domainError)
        {
            return domainError.Code switch
            {
                "duplicate_fund_code" => FundErrors.FundAlreadyExits,
                "fund_not_found" => FundErrors.FundNotFound,
                "database_error" => FundErrors.DatabaseError
            };
        }
    }
}
