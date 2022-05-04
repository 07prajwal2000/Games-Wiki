using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GamesApi.Filters;

public class MasterApiKeyFilter : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        await Task.Run(async () =>
        {
            var config = context.HttpContext.RequestServices.GetService<IConfiguration>() !;
            var helpers = context.HttpContext.RequestServices.GetService<FilterHelpers>() !;

            var apiKeyString = context.HttpContext.Request.Headers["API_KEY"].ToString();
            var masterKey = config["MASTER_KEY"] !;
            if (string.IsNullOrEmpty(apiKeyString) || context.HttpContext.Request.Headers["MASTER_KEY"] != masterKey)
            {
                context.Result = new UnauthorizedObjectResult("ADMIN ACCESS ONLY");
                return;
            }

            if (!Guid.TryParse(apiKeyString, out var apiKey))
            {
                context.Result = new UnauthorizedObjectResult("NOT AN API KEY.");
                return;
            }
            
            if (!await helpers.ValidateApiKey(apiKey))
            {
                context.Result = new UnauthorizedObjectResult("Key may be expired or blocked.");
                return;
            }
        });
    }
}