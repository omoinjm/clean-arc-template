using System.Threading;
using System.Threading.Tasks;
using Clean.Architecture.Template.Application.Commands.General;
using Clean.Architecture.Template.Application.Handlers.Users;
using Clean.Architecture.Template.Application.Response.General;
using Clean.Architecture.Template.Core.Entity;
using Clean.Architecture.Template.Core.Repository;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Clean.Architecture.Template.Application.UnitTests.Handlers.Users.Commands.CreateUser
{
    public class CreateUserValidationTests
    {
        private readonly IUserRepository _userRepository;
        private readonly CreateUserHandler _createUserHandler;

        public CreateUserValidationTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _createUserHandler = new CreateUserHandler(_userRepository);
        }

        [Fact]
        public async Task Handle_Should_ReturnErrorResponse_WhenUserIsNull()
        {
            // Arrange
            var command = new CreateCommand<UserEntity, CreateResponse>(null);

            // Act
            var result = await _createUserHandler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.ReturnRecordId.Should().Be(0);
            await _userRepository.DidNotReceive().CreateUser(Arg.Any<UserEntity>());
        }
    }
}
