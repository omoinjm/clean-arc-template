namespace Clean.Architecture.Template.Core.Results
{
    public class DeleteRecordResult
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public DeleteRecordResult()
        {
            IsSuccess = false;
            Error = string.Empty;
        }
    }
}