using Clean.Architecture.Template.Core.Entity;

namespace Clean.Architecture.Template.TestCommon.Extensions;

/// <summary>
/// Assertion extension methods for UserEntity validation in tests.
/// Reduces boilerplate and provides meaningful assertion messages.
/// </summary>
public static class UserEntityAssertionExtensions
{
    /// <summary>
    /// Asserts that a UserEntity matches the expected values.
    /// </summary>
    public static void AssertValidUserEntity(
        this UserEntity user,
        int expectedId,
        string expectedName,
        string expectedSurname,
        string expectedEmail)
    {
        user.Should().NotBeNull();
        user.Id.Should().Be(expectedId);
        user.Name.Should().Be(expectedName);
        user.Surname.Should().Be(expectedSurname);
        user.Email.Should().Be(expectedEmail);
        user.IsActive.Should().BeTrue();
    }

    /// <summary>
    /// Asserts that a UserEntity has all default values from the factory.
    /// </summary>
    public static void AssertDefaultUserValues(this UserEntity user)
    {
        user.Username.Should().NotBeNullOrEmpty();
        user.Password.Should().NotBeNullOrEmpty();
        user.Salt.Should().NotBeNullOrEmpty();
        user.Deleted.Should().BeFalse();
        user.IsActive.Should().BeTrue();
    }
}
