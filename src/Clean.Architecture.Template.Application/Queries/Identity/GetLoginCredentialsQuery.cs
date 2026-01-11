using Clean.Architecture.Template.Application.Response.Auth;
using MediatR;

namespace Clean.Architecture.Template.Application.Queries.Identity
{
    public class GetLoginCredentialsQuery : IRequest<ResponseLogin>
    {
        public string Token { get; set; } = string.Empty;
    }
}