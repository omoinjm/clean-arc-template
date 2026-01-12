using Xunit;
using Moq;
using Clean.Architecture.Template.Core.Repository;
using Clean.Architecture.Template.Core.Specs;

namespace Clean.Architecture.Template.Application.Tests;

public class RepositoryTests
{
    [Fact]
    public void MockRepository_ShouldBeCreatable()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();

        // Act
        var repository = mockRepository.Object;

        // Assert
        Assert.NotNull(repository);
    }

    [Fact]
    public void ApplicationSetup_ShouldHaveAllDependencies()
    {
        // This is a placeholder test to verify the test project is properly configured
        Assert.True(true);
    }
}
