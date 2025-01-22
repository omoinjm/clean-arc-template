namespace Clean.Architecture.Template.Application.Response.Menu
{
    public class MenuResponse
    {
        public int ModuleId { get; set; }
        public int ModuleItemId { get; set; }

        public string? ModuleSidebarClass { get; set; }

        public int SortOrder { get; set; }
        public int ModuleSortOrder { get; set; }

        public string Icon { get; set; }
        public string ModuleIcon { get; set; }

        public string DisplayText { get; set; }
        public string ModuleDisplayText { get; set; }

        public string RouterLink { get; set; }
        public string? ModuleRouterLink { get; set; }
    }
}