using Javaneh.API.Contracts.Responses;
using Javaneh.API.Helpers;
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
            var statusCode = objectResult.StatusCode ?? 200;

            // create message response
            var response = new ApiResponse<object>
            {
                Code = statusCode,
                Message = ResponseMessageHelper.GetMessage(statusCode),
                Data = objectResult.Value
            };
            // change controller message with this one
            context.Result = new ObjectResult(response)
            {
                StatusCode = statusCode
            };
        }
    }


}