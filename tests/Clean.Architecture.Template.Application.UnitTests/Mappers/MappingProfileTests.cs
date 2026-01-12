using Clean.Architecture.Template.Application.Mapper;
using Clean.Architecture.Template.Core.Entity;
using Clean.Architecture.Template.Core.Results;

namespace Clean.Architecture.Template.Application.UnitTests.Mappers;

public class ConfigureMappingProfileTests
{
    [Fact]
    public void ConfigureMappingProfile_ShouldMapUserEntityCorrectly()
    {
        // Arrange
        var userEntity = UserFactory.CreateUser(name: "TestUser");
        var mapper = LazyMapper.Mapper;

        // Act
        var mapped = mapper.Map<UserEntity>(userEntity);

        // Assert
        mapped.Should().NotBeNull();
        mapped.Name.Should().Be(userEntity.Name);
    }

    [Fact]
    public void ConfigureMappingProfile_ShouldMapCreateRecordResult()
    {
        // Arrange
        var result = new CreateRecordResult { RecordId = 1, Message = "Success" };
        var mapper = LazyMapper.Mapper;

        // Act
        var mapped = mapper.Map<CreateRecordResult>(result);

        // Assert
        mapped.Should().NotBeNull();
        mapped.RecordId.Should().Be(1);
    }

    [Fact]
    public void LazyMapper_ShouldReturnSingletonInstance()
    {
        // Arrange & Act
        var mapper1 = LazyMapper.Mapper;
        var mapper2 = LazyMapper.Mapper;

        // Assert
        mapper1.Should().NotBeNull();
        mapper2.Should().NotBeNull();
        mapper1.Should().BeSameAs(mapper2);
    }

    [Fact]
    public void ConfigureMappingProfile_ShouldBeConfiguredWithoutErrors()
    {
        // Arrange & Act & Assert
        // If mapping profiles have errors, this should throw
        var mapper = LazyMapper.Mapper;
        mapper.Should().NotBeNull();
    }
}
