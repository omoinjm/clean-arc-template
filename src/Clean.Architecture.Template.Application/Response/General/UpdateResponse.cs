namespace Clean.Architecture.Template.Application.Response.General
{
    public class UpdateResponse
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }

        public UpdateResponse()
        {
            IsSuccess = false;
            Error = string.Empty;
        }
    }
}