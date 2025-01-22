namespace Clean.Architecture.Template.Core.Entity.Common
{
    public class DateRange
    {
        public string? Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsOnClear { get; set; }
        public bool IsOnChange { get; set; }
        public bool IsRange { get; set; }
    }
}