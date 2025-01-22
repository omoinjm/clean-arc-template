using Clean.Architecture.Template.Core.Specs;
using MediatR;

namespace Clean.Architecture.Template.Application.Queries.General
{
    public class LookupQuery<T>(LookupParams p) : IRequest<DataList<T>> where T : class
    {
        public LookupParams LookupParams { get; set; } = p;
    }
}