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
            FilterHelpers helpers = context.HttpContext.RequestServices.GetService<FilterHelpers>()!;
            var key = context.HttpContext.Request.Headers["API_KEY"].ToString();
            if (!Guid.TryParse(key, out var apiKey))
            {
                context.Result = new UnauthorizedObjectResult("You Don't have access to this endpoint.");
                return;
            }

            if (!await helpers.ValidateApiKey(apiKey))
            {
                context.Result = new UnauthorizedObjectResult("You Don't have access to this endpoint.");
            }
        });
    }
}