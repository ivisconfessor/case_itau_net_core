using Mapster;

namespace CaseItau.Application.Mappings
{
    public class MappingConfig
    {
        public static void ConfigureMapster()
        {
            // Configurações globais
            TypeAdapterConfig.GlobalSettings.Default
                .NameMatchingStrategy(NameMatchingStrategy.IgnoreCase)
                .PreserveReference(true);

            // Configurar mapeamentos de fundos
            FundMapping.Configure(TypeAdapterConfig.GlobalSettings);
        }
    }
}
