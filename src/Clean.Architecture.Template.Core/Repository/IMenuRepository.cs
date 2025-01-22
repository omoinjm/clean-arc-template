using Clean.Architecture.Template.Core.Entity;

namespace Clean.Architecture.Template.Core.Repository
{
    public interface IMenuRepository
    {
        Task<List<MenuEntity>> GetMenuItems();
    }
}