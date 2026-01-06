using Clean.Architecture.Template.Core.Enum;

namespace Clean.Architecture.Template.Application.Response
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            ShowError = true;
            ErrorDisplay = EnumValidationDisplay.Popup;
            ShowSuccess = false;
            IsError = false;
            ResponseMessage = "Success";
        }

        public object? Data { get; set; }
        public bool IsError { get; set; }
        public bool ShowError { get; set; }
        public int StatusCode { get; set; }
        public bool ShowSuccess { get; set; }
        public string ResponseMessage { get; set; }
        public EnumValidationDisplay ErrorDisplay { get; set; }
    }
}
