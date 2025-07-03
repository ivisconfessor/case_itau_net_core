using CaseItau.Application.Common.Results;
using CaseItau.Application.DTOs.Requests;
using CaseItau.Application.DTOs.Responses;
using CaseItau.Application.Interfaces.UseCases;
using CaseItau.Application.Mappings;
using CaseItau.Domain.Entities;
using CaseItau.Domain.Interfaces.Services;
using FluentValidation;
using MapsterMapper;

namespace CaseItau.Application.UseCases
{
    public class CreateFundUseCase : ICreateFundUseCase
    {
        private readonly IFundService _fundService;
        private readonly IValidator<CreateFundRequestDto> _validator;
        private readonly IMapper _mapper;

        public CreateFundUseCase(IFundService fundService, IValidator<CreateFundRequestDto> validator, IMapper mapper)
        {
            _fundService = fundService;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<Result<FundResponseDto?, Error?>> ExecuteAsync(CreateFundRequestDto requestDto)
        {
            var validated = await _validator.ValidateAsync(requestDto);
            if (!validated.IsValid)
            {
                var errors = string.Join(" - ", validated.Errors.Select(e => e.ErrorMessage));
                return Result<FundResponseDto?, Error?>.Failure(new Error("validation_error", errors, "BadRequest"));
            }

            var fundEntity = _mapper.Map<Fund>(requestDto);

            var (createdFund, domainError) = await _fundService.CreateFundAsync(fundEntity);
            if (domainError != null)
            {
                var applicationError = ErrorMapping.MapToApplicationError(domainError);
                return Result<FundResponseDto?, Error?>.Failure(applicationError);
            }

            var responseDto = _mapper.Map<FundResponseDto>(createdFund!);
            return Result<FundResponseDto?, Error?>.Success(responseDto);
        }
    }
}
