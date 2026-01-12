using Clean.Architecture.Template.Core.Utils;

namespace Clean.Architecture.Template.Core.UnitTests.Utils;

public class EmailValidatorTests
{
    [Theory]
    [InlineData("john@example.com")]
    [InlineData("jane.doe@example.com")]
    [InlineData("user+tag@example.co.uk")]
    [InlineData("test123@domain.org")]
    public void IsValidEmail_WithValidEmails_ShouldReturnTrue(string email)
    {
        // Act
        var result = EmailValidator.IsValidEmail(email);

        // Assert
        result.Should().BeTrue($"{email} should be valid");
    }

    [Theory]
    [InlineData("invalid")]
    [InlineData("@example.com")]
    [InlineData("user@")]
    [InlineData("user @example.com")]
    [InlineData("user@.com")]
    [InlineData("user@example")]
    [InlineData("")]
    public void IsValidEmail_WithInvalidEmails_ShouldReturnFalse(string email)
    {
        // Act
        var result = EmailValidator.IsValidEmail(email);

        // Assert
        result.Should().BeFalse($"{email} should be invalid");
    }

    [Fact]
    public void IsValidEmail_WithMultipleDots_ShouldReturnTrue()
    {
        // Arrange
        const string email = "user@mail.co.uk";

        // Act
        var result = EmailValidator.IsValidEmail(email);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsValidEmail_WithNumbers_ShouldReturnTrue()
    {
        // Arrange
        const string email = "user123@example123.com";

        // Act
        var result = EmailValidator.IsValidEmail(email);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsValidEmail_WithSpecialCharacters_ShouldReturnTrue()
    {
        // Arrange
        const string email = "user+label@example.com";

        // Act
        var result = EmailValidator.IsValidEmail(email);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsValidEmail_WithDash_ShouldReturnTrue()
    {
        // Arrange
        const string email = "user-name@example.com";

        // Act
        var result = EmailValidator.IsValidEmail(email);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsValidEmail_WithUnderscore_ShouldReturnTrue()
    {
        // Arrange
        const string email = "user_name@example.com";

        // Act
        var result = EmailValidator.IsValidEmail(email);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsValidEmail_WithDotInLocalPart_ShouldReturnTrue()
    {
        // Arrange
        const string email = "first.last@example.com";

        // Act
        var result = EmailValidator.IsValidEmail(email);

        // Assert
        result.Should().BeTrue();
    }
}
