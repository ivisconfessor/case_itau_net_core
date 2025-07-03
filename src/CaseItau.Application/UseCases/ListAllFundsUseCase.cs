using CaseItau.Application.Common.Results;
using CaseItau.Application.DTOs.Responses;
using CaseItau.Application.Interfaces.UseCases;
using CaseItau.Domain.Interfaces.Services;
using MapsterMapper;

namespace CaseItau.Application.UseCases
{
    public class ListAllFundsUseCase : IListAllFundsUseCase
    {
        private readonly IFundService _fundService;
        private readonly IMapper _mapper;

        public ListAllFundsUseCase(IFundService fundService, IMapper mapper)
        {
            _fundService = fundService;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<FundResponseDto>?, Error?>> ExecuteAsync()
        {
            var (fundsEntity, domainError) = await _fundService.ListAllFundsAsync();

            if (domainError != null) 
            { 
            
            }

            var funds = _mapper.Map<IEnumerable<FundResponseDto>>(fundsEntity);

            return Result<IEnumerable<FundResponseDto>, Error>.Success(funds);
        }
    }
}
