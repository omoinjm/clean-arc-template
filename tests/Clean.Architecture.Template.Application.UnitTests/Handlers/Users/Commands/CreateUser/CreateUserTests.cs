using System.Threading;
using System.Threading.Tasks;
using Clean.Architecture.Template.Application.Commands.General;
using Clean.Architecture.Template.Application.Handlers.Users;
using Clean.Architecture.Template.Application.Response.General;
using Clean.Architecture.Template.Core.Entity;
using Clean.Architecture.Template.Core.Repository;
using Clean.Architecture.Template.Core.Results;
using Clean.Architecture.Template.TestCommon.Factories;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Clean.Architecture.Template.Application.UnitTests.Handlers.Users.Commands.CreateUser
{
    public class CreateUserTests
    {
        private readonly IUserRepository _userRepository;
        private readonly CreateUserHandler _createUserHandler;

        public CreateUserTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _createUserHandler = new CreateUserHandler(_userRepository);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessResponse_WhenUserIsCreated()
        {
            // Arrange
            var user = UserFactory.CreateUser();
            var command = new CreateCommand<UserEntity, CreateResponse>(user);
            var createRecordResult = new CreateRecordResult { ReturnRecordId = user.Id, IsSuccess = true };

            _userRepository.CreateUser(user).Returns(Task.FromResult(createRecordResult));

            // Act
            var result = await _createUserHandler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.ReturnRecordId.Should().Be(user.Id);
            result.IsSuccess.Should().BeTrue();
            await _userRepository.Received(1).CreateUser(user);
        }
    }
}
