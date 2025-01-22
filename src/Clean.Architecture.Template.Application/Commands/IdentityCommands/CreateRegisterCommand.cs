using Clean.Architecture.Template.Application.Response.Auth;
using MediatR;

namespace Clean.Architecture.Template.Application.Common.IdentityCommands
{
    public class CreateRegisterCommand : BaseRequest, IRequest<ResponseRegister>
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }
}