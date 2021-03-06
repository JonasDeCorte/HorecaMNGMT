using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Horeca.Shared.AuthUtils
{
    public static class HttpContextExtensions
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = true
        };

        public static async ValueTask WriteAccessDeniedResponse(
            this HttpContext context,
            string? title = null,
            int? statusCode = null,
            CancellationToken cancellationToken = default)
        {
            var problem = new ProblemDetails
            {
                Instance = context.Request.Path,
                Title = title ?? "Access denied",
                Status = statusCode ?? StatusCodes.Status403Forbidden,
            };
            context.Response.StatusCode = problem.Status.Value;

            await context.Response.WriteAsync(JsonSerializer.Serialize(problem, JsonSerializerOptions),
                cancellationToken);
        }
    }
}