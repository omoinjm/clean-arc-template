using Clean.Architecture.Template.Core.Entity.Common;
using Clean.Architecture.Template.Core.Specs;

namespace Clean.Architecture.Template.Core.Repository
{
    public interface ILookupRepository
    {
        Task<DataList<LookupEntity>> GetLookupList(LookupParams lookupParams);
    }
}