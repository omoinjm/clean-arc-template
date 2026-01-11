using Clean.Architecture.Template.Application.Response.Auth;
using MediatR;

namespace Clean.Architecture.Template.Application.Common.IdentityCommands
{
    public class CreateRegisterCommand : BaseRequest, IRequest<ResponseRegister>
    {
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}