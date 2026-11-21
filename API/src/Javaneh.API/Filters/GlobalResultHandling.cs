
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Javaneh.API.Filters;

public class GlobalResultHandling : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception != null)
            return;

        if (context.Result is ObjectResult objectResult)
        {
            var response = new
            {
                success = true,
                statusCode = objectResult.StatusCode ?? 200,
                data = objectResult.Value
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = objectResult.StatusCode
            };
        }
    }
}
