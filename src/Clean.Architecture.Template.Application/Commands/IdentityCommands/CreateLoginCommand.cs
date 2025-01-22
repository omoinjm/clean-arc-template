using Clean.Architecture.Template.Application.Response.Auth;
using MediatR;

namespace Clean.Architecture.Template.Application.Common.IdentityCommands
{
    public class CreateLoginCommand : BaseRequest, IRequest<ResponseLogin>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}