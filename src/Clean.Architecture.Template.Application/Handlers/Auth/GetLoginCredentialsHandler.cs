using Clean.Architecture.Template.Application.Queries.Identity;
using Clean.Architecture.Template.Application.Response.Auth;
using Clean.Architecture.Template.Core.Services;
using MediatR;

namespace Clean.Architecture.Template.Application.Handlers.Auth
{
    public class GetLoginCredentialsHandler(
        ICachingInMemoryService caching) :
        IRequestHandler<GetLoginCredentialsQuery,
        ResponseLogin>
    {
        private readonly ICachingInMemoryService _caching = caching;

        public Task<ResponseLogin> Handle(GetLoginCredentialsQuery request, CancellationToken cancellationToken)
        {
            var cachedResponse = _caching.Get<ResponseLogin>(request.Token);
            
            return Task.FromResult(cachedResponse ?? new ResponseLogin());
        }
    }
}