using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clean.Architecture.Template.Application.Response;
using Clean.Architecture.Template.Core.Enum;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Architecture.Template.API.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public object ReturnSuccessModel<T>(
           T? data,
           string message = "Success",
           int statusCode = StatusCodes.Status200OK,
           bool showSuccess = false,
           EnumValidationDisplay errorDisplay = EnumValidationDisplay.Popup,
           bool showError = true)
        {
            return new BaseResponse
            {
                Data = data,
                ResponseMessage = message,
                StatusCode = statusCode,
                ErrorDisplay = errorDisplay,
                ShowSuccess = showSuccess,
                ShowError = showError,
                IsError = false
            };
        }
    }
}
