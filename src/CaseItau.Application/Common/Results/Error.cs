namespace CaseItau.Application.Common.Results
{
    public sealed record Error(string Title, string Description, string? Code = null);
}
