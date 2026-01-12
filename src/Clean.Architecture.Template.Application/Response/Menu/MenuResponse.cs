namespace Clean.Architecture.Template.Application.Response.Menu
{
    public class MenuResponse
    {
        public int ModuleId { get; set; }
        public int ModuleItemId { get; set; }

        public string? ModuleSidebarClass { get; set; }

        public int SortOrder { get; set; }
        public int ModuleSortOrder { get; set; }

        public string Icon { get; set; } = string.Empty;
        public string ModuleIcon { get; set; } = string.Empty;

        public string DisplayText { get; set; } = string.Empty;
        public string ModuleDisplayText { get; set; } = string.Empty;

        public string RouterLink { get; set; } = string.Empty;
        public string? ModuleRouterLink { get; set; }
    }
}