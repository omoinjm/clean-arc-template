namespace Clean.Architecture.Template.Application.Response.General
{
    public class LookupResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool AltBoolValue { get; set; }
        public string? Code { get; set; } = "?";
        public string? Colour { get; set; } = "Blue";
    }
}