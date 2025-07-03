namespace CaseItau.Domain.Common
{
    public class DomainError(string code, string message)
    {
        public string Code { get; } = code;

        public string Message { get; } = message;
    }
}
