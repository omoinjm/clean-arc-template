using Clean.Architecture.Template.Core.Entity;

namespace Clean.Architecture.Template.Core.UnitTests.Entity;

public class UserEntityTests
{
    [Fact]
    public void UserEntity_WhenCreated_ShouldInitializeProperties()
    {
        // Arrange & Act
        var user = UserFactory.CreateUser();

        // Assert
        user.Should().NotBeNull();
        user.Id.Should().Be(Constants.User.Id);
        user.Name.Should().Be(Constants.User.Name);
        user.Surname.Should().Be(Constants.User.Surname);
        user.Email.Should().Be(Constants.User.Email);
    }

    [Fact]
    public void UserEntity_WithName_ShouldSetNameCorrectly()
    {
        // Arrange
        const string expectedName = "Jane";
        
        // Act
        var user = UserFactory.CreateUser(name: expectedName);

        // Assert
        user.Name.Should().Be(expectedName);
    }

    [Fact]
    public void UserEntity_WithEmail_ShouldSetEmailCorrectly()
    {
        // Arrange
        const string expectedEmail = "jane@example.com";
        
        // Act
        var user = UserFactory.CreateUser(email: expectedEmail);

        // Assert
        user.Email.Should().Be(expectedEmail);
    }

    [Fact]
    public void UserEntity_WithAllCustomValues_ShouldSetAllPropertiesCorrectly()
    {
        // Arrange
        const int expectedId = 42;
        const string expectedName = "Alice";
        const string expectedSurname = "Smith";
        const string expectedEmail = "alice@example.com";
        
        // Act
        var user = UserFactory.CreateUser(
            id: expectedId,
            name: expectedName,
            surname: expectedSurname,
            email: expectedEmail);

        // Assert
        user.Id.Should().Be(expectedId);
        user.Name.Should().Be(expectedName);
        user.Surname.Should().Be(expectedSurname);
        user.Email.Should().Be(expectedEmail);
    }

    [Fact]
    public void UserEntity_WhenCreatedWithDefaults_ShouldHaveValidSecurityProperties()
    {
        // Arrange & Act
        var user = UserFactory.CreateUser();

        // Assert
        user.AssertDefaultUserValues();
        user.Username.Should().NotBeNullOrEmpty();
        user.Password.Should().NotBeNullOrEmpty();
        user.Salt.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void UserEntity_WhenCreatedWithDefaults_ShouldBeActive()
    {
        // Arrange & Act
        var user = UserFactory.CreateUser();

        // Assert
        user.IsActive.Should().BeTrue();
        user.Deleted.Should().BeFalse();
    }

    [Fact]
    public void UserEntity_WhenCreated_ShouldHaveDefaultUserStatus()
    {
        // Arrange & Act
        var user = UserFactory.CreateUser();

        // Assert
        user.UserStatusId.Should().Be(1);
        user.UserStatusName.Should().Be("Active");
        user.UserStatusSymbol.Should().Be("A");
        user.UserStatusColor.Should().Be("Green");
    }

    [Theory]
    [InlineData(1, "John")]
    [InlineData(2, "Jane")]
    [InlineData(3, "Bob")]
    public void UserEntity_WithDifferentIds_ShouldSetIdCorrectly(int id, string name)
    {
        // Arrange & Act
        var user = UserFactory.CreateUser(id: id, name: name);

        // Assert
        user.Id.Should().Be(id);
        user.Name.Should().Be(name);
    }

    [Fact]
    public void UserEntity_CanSetTimestamps()
    {
        // Arrange
        var user = UserFactory.CreateUser();
        var now = DateTime.Now;

        // Act
        user.CreatedAt = now;
        user.UpdatedAt = now.AddDays(1);
        user.DeletedAt = now.AddDays(2);

        // Assert
        user.CreatedAt.Should().Be(now);
        user.UpdatedAt.Should().Be(now.AddDays(1));
        user.DeletedAt.Should().Be(now.AddDays(2));
    }

    [Fact]
    public void UserEntity_CanSetAuditProperties()
    {
        // Arrange
        var user = UserFactory.CreateUser();

        // Act
        user.CreatedBy = "Admin";
        user.UpdatedBy = "Editor";
        user.DeletedBy = "Deleter";

        // Assert
        user.CreatedBy.Should().Be("Admin");
        user.UpdatedBy.Should().Be("Editor");
        user.DeletedBy.Should().Be("Deleter");
    }

    [Fact]
    public void UserEntity_CanSetAuthenticationProperties()
    {
        // Arrange
        var user = UserFactory.CreateUser();
        const string token = "jwt-token-123";
        var expiryDate = DateTime.Now.AddDays(1);

        // Act
        user.Token = token;
        user.ExpiryDateTime = expiryDate;
        user.LoginDate = DateTime.Now;

        // Assert
        user.Token.Should().Be(token);
        user.ExpiryDateTime.Should().Be(expiryDate);
        user.LoginDate.Should().NotBeNull();
    }

    [Fact]
    public void UserEntity_CanSetPasswordResetProperties()
    {
        // Arrange
        var user = UserFactory.CreateUser();
        const string resetGuid = "reset-guid-123";
        const string otp = "123456";

        // Act
        user.ForgotPasswordGuid = resetGuid;
        user.Otp = otp;
        user.ChangePassword = true;

        // Assert
        user.ForgotPasswordGuid.Should().Be(resetGuid);
        user.Otp.Should().Be(otp);
        user.ChangePassword.Should().BeTrue();
    }

    [Fact]
    public void UserEntity_CanSetContactProperties()
    {
        // Arrange
        var user = UserFactory.CreateUser();
        const string phoneNumber = "+1234567890";
        const string idNumber = "ID123456";

        // Act
        user.PhoneNumber = phoneNumber;
        user.IdNumber = idNumber;

        // Assert
        user.PhoneNumber.Should().Be(phoneNumber);
        user.IdNumber.Should().Be(idNumber);
    }

    [Fact]
    public void UserEntity_CanSetRole()
    {
        // Arrange
        var user = UserFactory.CreateUser();
        const string role = "Admin";

        // Act
        user.Role = role;

        // Assert
        user.Role.Should().Be(role);
    }
}
