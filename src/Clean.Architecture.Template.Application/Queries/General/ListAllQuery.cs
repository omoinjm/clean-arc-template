using MediatR;

namespace Clean.Architecture.Template.Application.Queries.General
{
    public class ListAllQuery<T> : IRequest<IList<T>> where T : class { }
}