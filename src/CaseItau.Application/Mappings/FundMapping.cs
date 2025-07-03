using CaseItau.Application.DTOs.Requests;
using CaseItau.Application.DTOs.Responses;
using CaseItau.Domain.Entities;
using Mapster;

namespace CaseItau.Application.Mappings
{
    public class FundMapping
    {
        public static void Configure(TypeAdapterConfig config)
        {
            config.NewConfig<Fund, FundResponseDto>()
                .Map(dest => dest.Codigo, src => src.Code)
                .Map(dest => dest.Nome, src => src.Name)
                .Map(dest => dest.Cnpj, src => src.Cnpj)
                .Map(dest => dest.CodigoTipo, src => src.FundTypeId)
                .Map(dest => dest.NomeTipo, src => src.FundTypeName)
                .Map(dest => dest.Patrimonio, src => src.NetWorth);

            config.NewConfig<CreateFundRequestDto, Fund>()
                .Map(dest => dest.Code, src => src.Code)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Cnpj, src => src.Cnpj)
                .Map(dest => dest.FundTypeId, src => src.FundTypeId)
                .Map(dest => dest.NetWorth, src => src.InitialNetWorth)
                .Map(dest => dest.CreatedAt, _ => DateTime.Now)
                .Map(dest => dest.UpdatedAt, _ => DateTime.Now);

            config.NewConfig<UpdateFundRequestDto, Fund>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.FundTypeId, src => src.FundTypeId)
                .Map(dest => dest.UpdatedAt, _ => DateTime.Now)
                .IgnoreNullValues(true)
                .Ignore(dest => dest.Code)
                .Ignore(dest => dest.Cnpj)
                .Ignore(dest => dest.CreatedAt);

            config.NewConfig<UpdateNetWorthRequestDto, Fund>()
                .Ignore(dest => dest.NetWorth) // Primeiro ignoramos o mapeamento automático
                .AfterMapping((src, dest) => {
                    // Realizamos a operação de adicionar/subtrair o valor após o mapeamento básico
                    dest.NetWorth += src.Amount;
                    dest.UpdatedAt = DateTime.Now;
                })
                .IgnoreNonMapped(true);
        }
    }
}
