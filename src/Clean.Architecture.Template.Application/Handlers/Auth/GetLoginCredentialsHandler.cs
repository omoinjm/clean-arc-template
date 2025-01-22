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

        public async Task<ResponseLogin> Handle(GetLoginCredentialsQuery request, CancellationToken cancellationToken)
        {
            return _caching.Get<ResponseLogin>(request.Token);
        }
    }
}