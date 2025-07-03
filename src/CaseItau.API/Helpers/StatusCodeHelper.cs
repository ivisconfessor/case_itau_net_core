using Microsoft.AspNetCore.Http;

namespace CaseItau.API.Helpers
{
    public class StatusCodeHelper
    {
        public static int ConvertStatusHttp(string? statusCode)
        {
            return statusCode switch
            {
                "NotFound" => StatusCodes.Status404NotFound,
                "BadRequest" => StatusCodes.Status400BadRequest,
                "Conflict" => StatusCodes.Status409Conflict,
                "NoContent" => StatusCodes.Status204NoContent,
                "UnprocessableEntity" => StatusCodes.Status422UnprocessableEntity,
                "InternalServerError" => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status200OK
            };    
        }
    }
}
