using CaseItau.Application.DTOs.Requests;
using CaseItau.Application.DTOs.Responses;
using CaseItau.Application.UseCases;
using CaseItau.Domain.Entities;
using CaseItau.Domain.Interfaces.Services;
using FluentValidation;
using MapsterMapper;
using Moq;

namespace CaseItau.UnitTests.Application.UseCases
{
    public class CreateFundCaseTests
    {
        private readonly Mock<IFundService> _mockFundService;
        private readonly Mock<IValidator<CreateFundRequestDto>> _mockValidator;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CreateFundUseCase _useCase;

        public CreateFundCaseTests()
        {
            _mockFundService = new Mock<IFundService>();
            _mockValidator = new Mock<IValidator<CreateFundRequestDto>>();
            _mockMapper = new Mock<IMapper>();

            _useCase = new CreateFundUseCase(
                _mockFundService.Object,
                _mockValidator.Object,
                _mockMapper.Object
            );
        }

        [Fact]
        public async Task ExecuteAsync_WithValidRequest_ShouldReturnSuccess()
        {
            // Arrange
            var request = new CreateFundRequestDto
            {
                Code = "FUND123",
                Name = "New Test Fund",
                FundTypeId = 1,
                Cnpj = "12345678901234"
            };

            var fundEntity = new Fund
            {
                Code = "FUND123",
                Name = "New Test Fund",
                FundTypeId = 1,
                Cnpj = "12345678901234",
                NetWorth = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var responseDto = new FundResponseDto
            {
                Codigo = "FUND123",
                Nome = "New Test Fund",
                CodigoTipo = 1,
                NomeTipo = "Fixed Income",
                Cnpj = "12345678901234",
                Patrimonio = 0
            };

            
            _mockMapper.Setup(x => x.Map<Fund>(request))
                .Returns(fundEntity);

            _mockFundService.Setup(x => x.CreateFundAsync(It.IsAny<Fund>()))
                .ReturnsAsync((fundEntity, null));

            _mockMapper.Setup(x => x.Map<FundResponseDto>(fundEntity))
                .Returns(responseDto);

            // Act
            var result = await _useCase.ExecuteAsync(request);

            // Assert
            Assert.True(result.IsSuccess);

            _mockFundService.Verify(x => x.CreateFundAsync(It.IsAny<Fund>()), Times.Once);
        }
    }
}
