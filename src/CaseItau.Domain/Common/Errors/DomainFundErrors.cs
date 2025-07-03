namespace CaseItau.Domain.Common.Errors
{
    public static class DomainFundErrors
    {
        public static readonly DomainError NegativeNetWorth = new("negative_net_worth", "O valor do patrimônio não pode ser negativo.");
        public static readonly DomainError FundNotFound = new("fund_not_found", "Não foi encontrado um fundo com o código especificado.");
        public static readonly DomainError DuplicateFundCode = new("duplicate_fund_code", "Já existe um fundo cadastrado com este código.");
        public static readonly DomainError DuplicateFundCnpj = new("duplicate_fund_cnpj", "Já existe um fundo cadastrado com este CNPJ.");
        public static readonly DomainError DatabaseError = new("database_error", "Ocorreu um erro ao acessar o banco de dados. Tente novamente mais tarde.");
    }
}
