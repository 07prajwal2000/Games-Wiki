using GamesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GamesApi.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyFilter : Attribute, IAsyncAuthorizationFilter
{

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        await Task.Run(async () =>
        {
            var errorResponse = new Response<string>();
            FilterHelpers helpers = context.HttpContext.RequestServices.GetService<FilterHelpers>()!;
            var key = context.HttpContext.Request.Headers["API_KEY"].ToString();
            if (!Guid.TryParse(key, out var apiKey))
            {
                errorResponse.Message = "You Don't have access to this endpoint.";
                context.Result = new UnauthorizedObjectResult(errorResponse);
                return;
            }

            if (!await helpers.ValidateApiKey(apiKey))
            {
                errorResponse.Message = "The Key may be expired or blocked.";
                context.Result = new UnauthorizedObjectResult(errorResponse);
            }
        });
    }
}