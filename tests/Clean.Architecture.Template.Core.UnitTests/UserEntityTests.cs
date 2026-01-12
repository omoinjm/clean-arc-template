using Clean.Architecture.Template.Core.Entity;
using Clean.Architecture.Template.TestCommon.Factories;

namespace Clean.Architecture.Template.Core.UnitTests
{
    public class UserEntityTests
    {
        [Fact]
        public void UserEntity_Properties_CanBeSetAndGet()
        {
            // Arrange
            var user = UserFactory.CreateUser(
                id: 1,
                name: "John",
                surname: "Doe",
                email: "john.doe@example.com");

            // Assert
            user.Id.Should().Be(1);
            user.Name.Should().Be("John");
            user.Surname.Should().Be("Doe");
            user.Email.Should().Be("john.doe@example.com");
            user.Username.Should().Be("johndoe");
            user.Password.Should().Be("password");
            user.Salt.Should().Be("salt");
            user.ChangePassword.Should().BeFalse();
            user.Deleted.Should().BeFalse();
            user.CreatedBy.Should().Be("admin");
            user.UpdatedAt.Should().BeNull();
            user.DeletedAt.Should().BeNull();
            user.IdNumber.Should().Be("123456789");
            user.PhoneNumber.Should().Be("1234567890");
            user.Role.Should().Be("User");
            user.ForgotPasswordGuid.Should().BeNull();
            user.Otp.Should().BeNull();
            user.IsActive.Should().BeTrue();
            user.LoginDate.Should().BeNull();
            user.UserStatusId.Should().Be(1);
            user.UserStatusName.Should().Be("Active");
            user.UserStatusSymbol.Should().Be("A");
            user.UserStatusColor.Should().Be("Green");
            user.Token.Should().Be("token");
            user.ExpiryDateTime.Should().BeNull();
            user.LoginTimeStamp.Should().BeNull();
        }
    }
}
