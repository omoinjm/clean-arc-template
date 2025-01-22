using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Architecture.Template.Application.Response.Menu
{
    public class MenuModuleResponse
    {
        public int? ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string ModuleIcon { get; set; }
        public int ModuleSortOrder { get; set; }
        public string? ModuleRouterLink { get; set; }
        public string? ModuleSidebarClass { get; set; }

        public List<MenuResponse> MenuList { get; set; }
    }
}