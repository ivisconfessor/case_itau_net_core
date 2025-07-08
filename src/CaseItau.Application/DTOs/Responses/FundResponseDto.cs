namespace CaseItau.Application.DTOs.Responses
{
    public class FundResponseDto
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public int CodigoTipo { get; set; }
        public string NomeTipo { get; set; }
        public decimal Patrimonio { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
