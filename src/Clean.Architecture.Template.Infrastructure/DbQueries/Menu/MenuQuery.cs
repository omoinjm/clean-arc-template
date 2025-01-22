using Clean.Architecture.Template.Core.Specs;

namespace Clean.Architecture.Template.Infrastructure.DbQueries.Menu
{
    public class MenuQuery : QueryBase
    {
        public override string Query(GeneralSpecParams generalSpecParams)
        {
            return $@"";
        }

        public static string Query()
        {
            var query = $@"
                
                SELECT

                    a.id as ModuleItemId,
                    a.module_id as ModuleId,

                    m.sidebar_class AS ModuleSidebarClass,
                    a.sort_order as SortOrder,
                    m.sort_order AS ModuleSortOrder,
                    a.icon as Icon,
                    m.icon AS ModuleIcon,
                    a.display_text as DisplayText,
                    m.display_text AS ModuleDisplayText,
                    a.router_link as RouterLink,
                    m.router_link AS ModuleRouterLink

                FROM ui_sys_module_item a

                LEFT JOIN ui_sys_module m ON m.id = a.module_id

                WHERE COALESCE(a.require_admin, false) <> true AND a.is_enabled = true

                ORDER BY COALESCE(m.sort_order, 999) ASC, COALESCE(a.sort_order, 999) ASC;
            ";

            return query;
        }

        protected override string GetDefaultSortField()
        {
            return "";
        }
    }
}