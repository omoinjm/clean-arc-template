namespace Clean.Architecture.Template.Core.Results
{
    public class UpdateRecordResult
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public UpdateRecordResult()
        {
            IsSuccess = false;
            Error = string.Empty;
        }
    }
}