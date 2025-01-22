using Clean.Architecture.Template.Core.Specs;
using MediatR;

namespace Clean.Architecture.Template.Application.Queries.General
{
    public class ListQuery<T>(GeneralSpecParams generalSpecParams) : IRequest<Pagination<T>> where T : class
    {
        public GeneralSpecParams GeneralSpecParams { get; set; } = generalSpecParams;
    }
}