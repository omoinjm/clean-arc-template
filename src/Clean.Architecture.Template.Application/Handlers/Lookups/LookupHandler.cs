using Clean.Architecture.Template.Application.Mapper;
using Clean.Architecture.Template.Application.Queries.General;
using Clean.Architecture.Template.Application.Response.General;
using Clean.Architecture.Template.Core.Repository;
using Clean.Architecture.Template.Core.Specs;
using MediatR;

namespace Clean.Architecture.Template.Application.Handlers.Lookups
{
    public class LookupHandler(
        ILookupRepository repository) :
        IRequestHandler<LookupQuery<LookupResponse>, DataList<LookupResponse>>
    {
        private readonly ILookupRepository _repository = repository;

        public async Task<DataList<LookupResponse>> Handle(LookupQuery<LookupResponse> request, CancellationToken cancellationToken)
        {
            var items = await _repository.GetLookupList(request.LookupParams);

            return LazyMapper.Mapper.Map<DataList<LookupResponse>>(items);
        }
    }
}