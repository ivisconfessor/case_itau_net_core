using CaseItau.API.Helpers;
using CaseItau.Application.Common.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CaseItau.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IActionResult ProcessResponse<T>(Result<T, Error?> result, int? successStatusCode = null, string? actionName = null)
        {
            if (!result.IsSuccess || result.Erros != null)
            {
                return ProcessError(result.Erros);
            }

            return ProcessSuccess(result.Value, successStatusCode, actionName);
        }

        protected IActionResult ProcessResponse(Result<bool, Error?> result)
        {
            if (!result.IsSuccess || result.Erros != null)
            {
                return ProcessError(result.Erros);
            }

            return NoContent();
        }

        private IActionResult ProcessSuccess<T>(T? value, int? statusCode = null, string? actionName = null)
        {
            // Se for boolean e true, ou se o valor for nulo, retorna NoContent
            if ((value is bool boolValue && boolValue) || value == null)
            {
                return NoContent();
            }

            if (statusCode == StatusCodes.Status201Created && actionName != null)
            {
                var idProp = value?.GetType().GetProperty("Codigo");
                if (idProp != null)
                {
                    var id = idProp.GetValue(value);
                    var paramName = idProp.Name.ToLowerInvariant();

                    // Cria o route value dictionary manualmente em vez de usar sintaxe de índice dinâmico
                    var routeValues = new RouteValueDictionary
                    {
                        { "code", id }
                    };

                    return CreatedAtAction(actionName, routeValues, value);
                }
                return StatusCode(StatusCodes.Status201Created, value);
            }

            // Se um código específico foi informado, use-o
            if (statusCode.HasValue)
            {
                return StatusCode(statusCode.Value, value);
            }

            // Caso contrário, retorna Ok com o valor
            return Ok(value);
        }

        private IActionResult ProcessError(Error error)
        {
            // Converter o código de erro para StatusCode HTTP usando o helper
            int statusCode = StatusCodeHelper.ConvertStatusHttp(error.Code);

            return Problem(error.Description, HttpContext.Request.Path, statusCode, error.Title);
        }
    }
}
