using Clean.Architecture.Template.Application.Common.IdentityCommands;
using Clean.Architecture.Template.Application.Response.Auth;
using Clean.Architecture.Template.Core.Entity;
using Clean.Architecture.Template.Core.Repository;
using Clean.Architecture.Template.Core.Utils;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Clean.Architecture.Template.Application.Handlers.Auth
{
    public class RegisterQueryHandler(
        IUserRepository repository) :
        IRequestHandler<CreateRegisterCommand, ResponseRegister>
    {
        private readonly IUserRepository _repository = repository;

        public async Task<ResponseRegister> Handle(CreateRegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _repository.GetUserByEmail(request.Email);

            if (existingUser != null) throw new InvalidOperationException("Email is already exsits!");

            var email = request.Email;
            var salt = CryptoUtil.GenerateSalt();
            var password = request.Password;
            var hashedPassword = CryptoUtil.HashMultiple(password, salt);

            var user = new UserEntity
            {
                Email = email,
                Salt = salt,
                Password = hashedPassword,
                Role = request.Role
            };

            await _repository.CreateUser(user);

            var responseRegister = new ResponseRegister { Success = true, MessageList = ["User successfully created!"] };

            return responseRegister;
        }
    }
}