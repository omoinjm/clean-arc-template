namespace Clean.Architecture.Template.Application.Response.Auth
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public List<string> MessageList { get; set; }

        public BaseResponse()
        {
            MessageList = [];
        }
    }
}