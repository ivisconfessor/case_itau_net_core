namespace CaseItau.Domain.Entities
{
    public class Fund
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public int FundTypeId { get; set; }
        public decimal NetWorth {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Propriedade de navegação (não mapeada diretamente na tabela)
        public virtual FundType FundType { get; set; }

        // Propriedade calculada (não armazenada no banco)
        public string FundTypeName => FundType?.Name;
    }
}
