using CaseItau.Application.Common.Results;
using CaseItau.Application.DTOs.Responses;
using CaseItau.Application.Interfaces.UseCases;
using CaseItau.Application.Mappings;
using CaseItau.Domain.Interfaces.Services;
using MapsterMapper;

namespace CaseItau.Application.UseCases
{
    public class GetFundByCodeUseCase : IGetFundByCodeUseCase
    {
        private readonly IFundService _fundService;
        private readonly IMapper _mapper;

        public GetFundByCodeUseCase(IFundService fundService, IMapper mapper)
        {
            _fundService = fundService;
            _mapper = mapper;
        }

        public async Task<Result<FundResponseDto?, Error?>> ExecuteAsync(string code)
        {
            var (fund, domainError) = await _fundService.GetFundByCodeAsync(code);

            if (domainError != null)
            {
                var applicationError = ErrorMapping.MapToApplicationError(domainError);
                return Result<FundResponseDto?, Error?>.Failure(applicationError);
            }

            var responseDto = _mapper.Map<FundResponseDto>(fund);
            return Result<FundResponseDto, Error>.Success(responseDto);
        }
    }
}
