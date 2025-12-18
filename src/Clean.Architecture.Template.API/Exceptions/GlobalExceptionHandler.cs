using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clean.Architecture.Template.API.Exceptions.CustomException;
using Clean.Architecture.Template.Core.Enum;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Architecture.Template.API.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception ex, CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails
            {
                Title = ErrorTitleProvider.GetErrorTitle(ex),
                Detail = ex.Message,
                Type = ex.GetType().Name,
                Instance = httpContext.Request.Path.ToString(),
                Status = httpContext.Response.StatusCode,
                Extensions =
                {
                    ["traceID"] = Guid.NewGuid().ToString(),
                    ["raw"] = ex.ToString(),
                    ["isError"] = true,
                    ["errorDisplay"] = ex.Data["errorDisplay"] ?? EnumValidationDisplay.Popup,
                    ["showException"] = true,
                    ["errorList"] = new List<string>() { ex.Message },
                    ["isException"] = true
                }
            };

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
