using Moq;
using Clean.Architecture.Template.Core.Repository;
using Clean.Architecture.Template.Core.Specs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Clean.Architecture.Template.Application.Tests;

[TestClass]
public class RepositoryTests
{
    [TestMethod]
    public void MockRepository_ShouldBeCreatable()
    {
        // Arrange
        var mockRepository = new Mock<IUserRepository>();

        // Act
        var repository = mockRepository.Object;

        // Assert
        Assert.IsNotNull(repository);
    }

    [TestMethod]
    public void ApplicationSetup_ShouldHaveAllDependencies()
    {
        // This is a placeholder test to verify the test project is properly configured
        Assert.IsTrue(true);
    }
}
