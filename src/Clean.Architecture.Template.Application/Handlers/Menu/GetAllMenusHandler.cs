using Clean.Architecture.Template.Application.Mapper;
using Clean.Architecture.Template.Application.Queries.General;
using Clean.Architecture.Template.Application.Response.Menu;
using Clean.Architecture.Template.Core.Repository;
using MediatR;

namespace Clean.Architecture.Template.Application.Handlers.Menu
{
    public class GetAllMenusHandler(
        IMenuRepository repository) :
        IRequestHandler<ListAllQuery<MenuModuleResponse>,
        IList<MenuModuleResponse>>
    {
        private readonly IMenuRepository _repository = repository;

        public async Task<IList<MenuModuleResponse>> Handle(ListAllQuery<MenuModuleResponse> request, CancellationToken cancellationToken)
        {
            var menuList = await _repository.GetMenuItems();

            var menuResponseList = LazyMapper.Mapper.Map<List<MenuResponse>>(menuList);

            return [.. menuResponseList
                .GroupBy(menu => new { menu.ModuleId, menu.ModuleDisplayText, menu.ModuleIcon, menu.ModuleSortOrder, menu.ModuleRouterLink })
                .Select(group => new MenuModuleResponse
                {
                    ModuleId = group.Key.ModuleId,
                    ModuleIcon = group.Key.ModuleIcon,
                    ModuleName = group.Key.ModuleDisplayText,
                    ModuleSortOrder = group.Key.ModuleSortOrder,
                    ModuleRouterLink = group.Key.ModuleRouterLink,

                    MenuList = [.. group]
                })];
        }
    }
}
