namespace CaseItau.Application.DTOs.Requests
{
    public class CreateFundRequestDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public int FundTypeId { get; set; }
        public decimal InitialNetWorth { get; set; }
    }
}
