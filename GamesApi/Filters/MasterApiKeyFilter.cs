using GamesApi.Models;
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
            var errorResponse = new Response<string>();
            var config = context.HttpContext.RequestServices.GetService<IConfiguration>() !;
            var helpers = context.HttpContext.RequestServices.GetService<FilterHelpers>() !;

            var apiKeyString = context.HttpContext.Request.Headers["API_KEY"].ToString();
            var masterKey = config["MASTER_KEY"] !;
            if (string.IsNullOrEmpty(apiKeyString) || context.HttpContext.Request.Headers["MASTER_KEY"] != masterKey)
            {
                errorResponse.Message = "ADMIN ACCESS ONLY";
                context.Result = new UnauthorizedObjectResult(errorResponse);
                return;
            }

            if (!Guid.TryParse(apiKeyString, out var apiKey))
            {
                errorResponse.Message = "NOT AN API KEY.";
                context.Result = new UnauthorizedObjectResult(errorResponse);
                return;
            }
            
            if (!await helpers.ValidateApiKey(apiKey))
            {
                errorResponse.Message = "Key may be expired or blocked.";
                context.Result = new UnauthorizedObjectResult(errorResponse);
                return;
            }
        });
    }
}