using CaseItau.Application.Common.Results;

namespace CaseItau.Application.Common.Errors
{
    public static class FundErrors
    {
        public static readonly Error FundNotFound = new("Fundo não encontrado!", "Não foi encontrado nenhum Fundo de Investimento com o código informado.", "NotFound");
        public static readonly Error FundAlreadyExits = new("Fundo já existe!", "Já existe um Fundo de Investimento com o código informado.", "Conflict");
        public static readonly Error DatabaseError = new("Falha Banco de Dados!", "Ocorreu um erro ao acessar o banco de dados. Tente novamente.", "InternalServerError");
    }
}
