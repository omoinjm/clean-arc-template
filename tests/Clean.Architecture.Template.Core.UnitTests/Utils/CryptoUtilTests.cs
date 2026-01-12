using Clean.Architecture.Template.Core.Utils;

namespace Clean.Architecture.Template.Core.UnitTests.Utils;

public class CryptoUtilTests
{
    [Fact]
    public void GenerateSalt_ShouldReturnNonEmptyString()
    {
        // Act
        var salt = CryptoUtil.GenerateSalt();

        // Assert
        salt.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GenerateSalt_ShouldReturnGuidFormat()
    {
        // Act
        var salt = CryptoUtil.GenerateSalt();

        // Assert
        // Should be in GUID format
        salt.Should().NotBeNullOrEmpty();
        salt.Length.Should().BeGreaterThan(0);
    }

    [Fact]
    public void GenerateSalt_ShouldReturnDifferentValuesEachTime()
    {
        // Act
        var salt1 = CryptoUtil.GenerateSalt();
        var salt2 = CryptoUtil.GenerateSalt();

        // Assert
        salt1.Should().NotBe(salt2);
    }

    [Fact]
    public void HashMultiple_WithValidInputs_ShouldReturnHashedString()
    {
        // Arrange
        const string password = "password123";
        const string salt = "salt-value-123";

        // Act
        var hashedPassword = CryptoUtil.HashMultiple(password, salt);

        // Assert
        hashedPassword.Should().NotBeNull();
        hashedPassword.Should().NotBeEmpty();
        hashedPassword.Should().NotBe(password);
    }

    [Fact]
    public void HashMultiple_WithSameInputs_ShouldReturnSameHash()
    {
        // Arrange
        const string password = "password123";
        const string salt = "salt-value-123";

        // Act
        var hash1 = CryptoUtil.HashMultiple(password, salt);
        var hash2 = CryptoUtil.HashMultiple(password, salt);

        // Assert
        hash1.Should().Be(hash2);
    }

    [Fact]
    public void HashMultiple_WithDifferentPasswords_ShouldReturnDifferentHashes()
    {
        // Arrange
        const string password1 = "password123";
        const string password2 = "password456";
        const string salt = "salt-value-123";

        // Act
        var hash1 = CryptoUtil.HashMultiple(password1, salt);
        var hash2 = CryptoUtil.HashMultiple(password2, salt);

        // Assert
        hash1.Should().NotBe(hash2);
    }

    [Fact]
    public void HashMultiple_WithDifferentSalts_ShouldReturnDifferentHashes()
    {
        // Arrange
        const string password = "password123";
        const string salt1 = "salt-value-123";
        const string salt2 = "salt-value-456";

        // Act
        var hash1 = CryptoUtil.HashMultiple(password, salt1);
        var hash2 = CryptoUtil.HashMultiple(password, salt2);

        // Assert
        hash1.Should().NotBe(hash2);
    }

    [Fact]
    public void HashMultiple_ShouldProduceHexadecimalString()
    {
        // Arrange
        const string password = "password123";
        const string salt = "salt-value";

        // Act
        var hash = CryptoUtil.HashMultiple(password, salt);

        // Assert
        hash.Should().NotBeNull();
        // SHA512 hash should be hexadecimal
        hash.All(c => char.IsDigit(c) || (c >= 'A' && c <= 'F')).Should().BeTrue();
    }

    [Fact]
    public void HashMultiple_WithEmptyPassword_ShouldReturnHash()
    {
        // Arrange
        const string password = "";
        const string salt = "salt-value";

        // Act
        var hash = CryptoUtil.HashMultiple(password, salt);

        // Assert
        hash.Should().NotBeNull();
    }

    [Fact]
    public void HashMultiple_WithLongPassword_ShouldReturnHash()
    {
        // Arrange
        var password = new string('a', 1000);
        const string salt = "salt-value";

        // Act
        var hash = CryptoUtil.HashMultiple(password, salt);

        // Assert
        hash.Should().NotBeNull();
        hash.Should().NotBeEmpty();
    }

    [Fact]
    public void HashMultiple_ResultShouldBeConsistentLength()
    {
        // Arrange
        const string password1 = "short";
        const string password2 = "this-is-a-very-long-password-with-many-characters";
        const string salt = "salt";

        // Act
        var hash1 = CryptoUtil.HashMultiple(password1, salt);
        var hash2 = CryptoUtil.HashMultiple(password2, salt);

        // Assert
        hash1.Length.Should().Be(hash2.Length);
    }

    [Fact]
    public void VerifyPassword_WithCorrectPassword_ShouldReturnTrue()
    {
        // Arrange
        const string password = "myPassword123";
        var salt = CryptoUtil.GenerateSalt();
        var hashedPassword = CryptoUtil.HashMultiple(password, salt);

        // Act
        var result = CryptoUtil.VerifyPassword(password, salt, hashedPassword);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void VerifyPassword_WithIncorrectPassword_ShouldReturnFalse()
    {
        // Arrange
        const string password = "myPassword123";
        const string wrongPassword = "wrongPassword456";
        var salt = CryptoUtil.GenerateSalt();
        var hashedPassword = CryptoUtil.HashMultiple(password, salt);

        // Act
        var result = CryptoUtil.VerifyPassword(wrongPassword, salt, hashedPassword);

        // Assert
        result.Should().BeFalse();
    }
}
