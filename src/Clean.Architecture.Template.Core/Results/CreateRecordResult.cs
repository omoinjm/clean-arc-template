namespace Clean.Architecture.Template.Core.Results
{
    public class CreateRecordResult
    {
        public int? ReturnRecordId { get; set; }
        public bool IsSuccess { get; set; }
        public string Error { get; set; }

        public CreateRecordResult()
        {
            IsSuccess = false;
            Error = string.Empty;
            ReturnRecordId = 0;
        }
    }
}