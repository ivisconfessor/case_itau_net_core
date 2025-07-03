using CaseItau.Application.Common.Results;
using CaseItau.Application.DTOs.Requests;
using CaseItau.Application.DTOs.Responses;
using CaseItau.Application.Interfaces.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace CaseItau.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class FundController : BaseController
    {
        private readonly ICreateFundUseCase _createFundUseCase;
        private readonly IDeleteFundUseCase _deleteFundUseCase;
        private readonly IGetFundByCodeUseCase _getFundByCodeUseCase;
        private readonly IListAllFundsUseCase _listAllFundsUseCase;
        private readonly IUpdateFundNetWorthUseCase _updateFundNetWorthUseCase;
        private readonly IUpdateFundUseCase _updateFundUseCase;

        public FundController(
            ICreateFundUseCase createFundUseCase, 
            IDeleteFundUseCase deleteFundUseCase, 
            IGetFundByCodeUseCase getFundByCodeUseCase, 
            IListAllFundsUseCase listAllFundsUseCase, 
            IUpdateFundNetWorthUseCase updateFundNetWorthUseCase, 
            IUpdateFundUseCase updateFundUseCase)
        {
            _createFundUseCase = createFundUseCase;
            _deleteFundUseCase = deleteFundUseCase;
            _getFundByCodeUseCase = getFundByCodeUseCase;
            _listAllFundsUseCase = listAllFundsUseCase;
            _updateFundNetWorthUseCase = updateFundNetWorthUseCase;
            _updateFundUseCase = updateFundUseCase;
        }

        /// <summary>
        /// Cria um novo fundo.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(FundResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest, MediaTypeNames.Application.ProblemJson)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError, MediaTypeNames.Application.ProblemJson)]
        public async Task<IActionResult> CreateFund([FromBody] CreateFundRequestDto request) => 
            ProcessResponse(await _createFundUseCase.ExecuteAsync(request), StatusCodes.Status201Created, nameof(GetFundByCode));
        

        /// <summary>
        /// Exclui um fundo pelo código.
        /// </summary>
        [HttpDelete("{code}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, MediaTypeNames.Application.ProblemJson)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError, MediaTypeNames.Application.ProblemJson)]
        public async Task<IActionResult> DeleteFund(string code) =>
            ProcessResponse(await _deleteFundUseCase.ExecuteAsync(code));
        

        /// <summary>
        /// Retorna um fundo específico pelo código.
        /// </summary>
        [HttpGet("{code}")]
        [ProducesResponseType(typeof(Result<FundResponseDto, Error>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFundByCode(string code) =>
            ProcessResponse(await _getFundByCodeUseCase.ExecuteAsync(code));

        /// <summary>
        /// Lista todos os fundos cadastrados.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FundResponseDto>), StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest, MediaTypeNames.Application.ProblemJson)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError, MediaTypeNames.Application.ProblemJson)]
        public async Task<IActionResult> GetAllFunds() =>
            ProcessResponse(await _listAllFundsUseCase.ExecuteAsync());

        /// <summary>
        /// Atualiza o valor do patrimônio do fundo.
        /// </summary>
        [HttpPut("{code}/net-worth")]
        [ProducesResponseType(typeof(FundResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateNetWorth(string code, [FromBody] UpdateNetWorthRequestDto request) =>
            ProcessResponse(await _updateFundNetWorthUseCase.ExecuteAsync(code, request));

        /// <summary>
        /// Atualiza um fundo existente.
        /// </summary>
        [HttpPut("{code}")]
        [ProducesResponseType(typeof(FundResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateFund(string code, [FromBody] UpdateFundRequestDto request) =>
            ProcessResponse(await _updateFundUseCase.ExecuteAsync(code, request));
    }
}
