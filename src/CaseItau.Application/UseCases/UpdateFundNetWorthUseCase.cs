﻿using CaseItau.Application.Common.Results;
using CaseItau.Application.DTOs.Requests;
using CaseItau.Application.Interfaces.UseCases;
using CaseItau.Application.Mappings;
using CaseItau.Application.Validators.Fund;
using CaseItau.Domain.Entities;
using CaseItau.Domain.Interfaces.Services;
using MapsterMapper;

namespace CaseItau.Application.UseCases
{
    public class UpdateFundNetWorthUseCase : IUpdateFundNetWorthUseCase
    {
        private readonly IFundService _fundService;
        private readonly UpdateNetWorthValidator _validator;
        private readonly IMapper _mapper;

        public UpdateFundNetWorthUseCase(IFundService fundService, UpdateNetWorthValidator validator, IMapper mapper)
        {
            _fundService = fundService;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<Result<bool, Error?>> ExecuteAsync(string code, UpdateNetWorthRequestDto requestDto)
        {
            requestDto.Code = code;
            var validated = await _validator.ValidateAsync(requestDto);
            if (!validated.IsValid)
            {
                var errors = string.Join(" - ", validated.Errors.Select(e => e.ErrorMessage));
                return Result<bool, Error?>.Failure(new Error("validation_error", errors, "BadRequest"));
            }

            var fundEntity = _mapper.Map<Fund>(requestDto);

            var (success, domainError) = await _fundService.UpdateFundNetWorthAsync(code, fundEntity);
            if (domainError != null)
            {
                var applicationError = ErrorMapping.MapToApplicationError(domainError);
                return Result<bool, Error?>.Failure(applicationError);
            }

            return Result<bool, Error?>.Success(true);
        }
    }
}
