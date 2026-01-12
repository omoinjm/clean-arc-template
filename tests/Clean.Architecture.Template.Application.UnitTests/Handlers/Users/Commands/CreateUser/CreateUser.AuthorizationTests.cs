using System.Threading.Tasks;
using Xunit;

namespace Clean.Architecture.Template.Application.UnitTests.Handlers.Users.Commands.CreateUser
{
    public class CreateUserAuthorizationTests
    {
        [Fact(Skip = "Authorization not yet implemented")]
        public Task Handle_Should_ReturnErrorResponse_WhenUserIsNotAuthorized()
        {
            // Arrange

            // Act

            // Assert
            return Task.CompletedTask;
        }
    }
}
