namespace Clean.Architecture.Template.Application.Response.General
{
    public class DeleteResponse
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }

        public DeleteResponse()
        {
            IsSuccess = false;
            Error = string.Empty;
        }
    }
}