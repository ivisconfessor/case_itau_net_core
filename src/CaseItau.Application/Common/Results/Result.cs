using System.Text.Json.Serialization;

namespace CaseItau.Application.Common.Results
{
    public class Result<TValue, TError>
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TValue? Value { get; set; } = default;
        public bool IsSuccess { get; set; }
        public TError? Erros { get; set; } = default;

        public static Result<TValue, TError?> Success(TValue? value = default) =>
            new() { Value = value, IsSuccess = true };

        public static Result<TValue, TError?> Failure(TError erros) =>
            new() { Erros = erros, IsSuccess = false };
    }
}
