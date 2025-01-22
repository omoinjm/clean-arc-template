using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Clean.Architecture.Template.Application.Common.IdentityCommands;
using Clean.Architecture.Template.Application.Response.Auth;
using Clean.Architecture.Template.Core.Entity;
using Clean.Architecture.Template.Core.Repository;
using Clean.Architecture.Template.Core.Services;
using Clean.Architecture.Template.Core.Utils;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Clean.Architecture.Template.Application.Handlers.Auth
{
    public class LoginQueryHandler(
        IConfiguration configuration,
        IUserRepository repository,
        ICachingInMemoryService caching) :
        IRequestHandler<CreateLoginCommand, ResponseLogin>
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IUserRepository _repository = repository;
        private readonly ICachingInMemoryService _caching = caching;

        public async Task<ResponseLogin> Handle(CreateLoginCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _repository.GetUserByEmail(request.Email)
                ?? throw new ArgumentException("The provided email or username is invalid.", nameof(request.Email));

            if (!(request.Password == existingUser.Password)) throw new UnauthorizedAccessException("Password is incorrect.");

            bool isPasswordVerified = CryptoUtil.VerifyPassword(request.Password, existingUser.Salt!, existingUser.Password);

            if (!isPasswordVerified) throw new UnauthorizedAccessException("Password is incorrect.");

            var expiryDate = DateTime.Now.AddDays(1);

            var responseLogin = MapResponseLogin(existingUser!, GenerateJwtToken(existingUser!));

            // Set the user with token and expiry date
            existingUser.Token = responseLogin.Token;
            existingUser.ExpiryDateTime = expiryDate;
            existingUser.LoginTimeStamp = DateTime.Now;
            existingUser.UpdatedBy = $"{existingUser.Name} {existingUser.Surname}";

            SetUserInCache(existingUser!, responseLogin);

            // Update the user with token and expiry date
            await _repository.UpdateUser(existingUser!);

            return responseLogin;
        }

        #region Helper methods
        private string GenerateJwtToken(UserEntity existingUser)
        {
            var claimList = new List<Claim>
        {
            new(ClaimTypes.Name, existingUser.Email!),
            new(ClaimTypes.Role, existingUser.Role!)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expireDate = DateTime.UtcNow.AddDays(1);

            var token = new JwtSecurityToken(
                claims: claimList,
                notBefore: DateTime.UtcNow,
                expires: expireDate,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private ResponseLogin MapResponseLogin(UserEntity existingUser, string token)
        {
            var expiryDate = DateTime.Now.AddDays(1);
            int timeStamp = DateUtil.ConvertToTimeStamp(expiryDate);

            return new ResponseLogin
            {
                Success = true,
                UserId = existingUser.Id,
                Role = existingUser.Role!,
                Username = existingUser.Username!,
                Email = existingUser.Email!,
                Token = token,
                ExpireDate = expiryDate,
                TimeStamp = timeStamp
            };
        }


        private void SetUserInCache(UserEntity existingUser, ResponseLogin responseLogin)
        {
            _caching.Set("token", responseLogin.Token, TimeSpan.FromDays(1));
            _caching.Set("loggedInUserId", existingUser.Id, TimeSpan.FromDays(1));
            _caching.Set(responseLogin.Token, responseLogin, TimeSpan.FromDays(1));
        }
        #endregion
    }
}