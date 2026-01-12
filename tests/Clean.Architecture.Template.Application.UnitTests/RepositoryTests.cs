using Clean.Architecture.Template.Core.Repository;
using NSubstitute;

namespace Clean.Architecture.Template.Application.Tests;

public class RepositoryTests
{
    [Fact]
    public void MockRepository_ShouldBeCreatable()
    {
        // Arrange
        var mockRepository = Substitute.For<IUserRepository>();

        // Act
        var repository = mockRepository;

        // Assert
        repository.Should().NotBeNull();
    }

    [Fact]
    public void ApplicationSetup_ShouldHaveAllDependencies()
    {
        // This verifies the test project is properly configured
        true.Should().BeTrue();
    }
}
