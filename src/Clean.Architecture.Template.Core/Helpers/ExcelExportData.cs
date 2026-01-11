namespace Clean.Architecture.Template.Core.Helpers
{
    public class ExcelExportData
    {
        public IEnumerable<object> Data { get; set; } = new List<object>();
        public List<string> PropertyHeaders { get; set; } = new List<string>();
        public List<string> ExcelHeaders { get; set; } = new List<string>();
        public string SheetName { get; set; } = string.Empty;
    }
}