# Quick Reference: Test Patterns

## Test Constants

Define constants in `tests/TestCommon/TestConstants/Constants.cs`:

```csharp
public static class Constants
{
    public static class User
    {
        public const int Id = 1;
        public const string Name = "John";
        public const string Email = "john@example.com";
    }
}
```

Use in tests:
```csharp
Constants.User.Id
Constants.User.Name
Constants.User.Email
```

## Factories

Create objects quickly in tests:

```csharp
// Use all defaults
var user = UserFactory.CreateUser();

// Override specific fields
var customUser = UserFactory.CreateUser(
    name: "Jane",
    email: "jane@example.com");
```

Create new factories:
```csharp
public static class ProductFactory
{
    public static ProductEntity CreateProduct(
        int id = -1,
        string name = "")
    {
        return new ProductEntity
        {
            Id = id == -1 ? Constants.Product.Id : id,
            Name = string.IsNullOrEmpty(name) ? Constants.Product.Name : name
        };
    }
}
```

## Assertion Extensions

Add custom assertions for cleaner tests:

```csharp
// Define
public static void AssertValidProduct(
    this ProductEntity product,
    int expectedId,
    string expectedName)
{
    product.Should().NotBeNull();
    product.Id.Should().Be(expectedId);
    product.Name.Should().Be(expectedName);
}

// Use
product.AssertValidProduct(1, "Widget");
```

## Write Tests

Standard test structure:

```csharp
public class UserEntityTests
{
    [Fact]
    public void UserEntity_WhenCreated_ShouldHaveValidProperties()
    {
        // Arrange
        var user = UserFactory.CreateUser(name: "John");
        
        // Act
        var result = user.Name;
        
        // Assert
        result.Should().Be("John");
        user.AssertValidUserEntity(1, "John", "Doe", "john@example.com");
    }

    [Fact]
    public void UserEntity_WhenInvalid_ShouldFail()
    {
        // Arrange
        var user = UserFactory.CreateUser(name: "");
        
        // Act & Assert
        user.Name.Should().Be("");
    }

    [Theory]
    [InlineData("John")]
    [InlineData("Jane")]
    public void UserEntity_WithDifferentNames_ShouldStore(string name)
    {
        // Arrange
        var user = UserFactory.CreateUser(name: name);
        
        // Assert
        user.Name.Should().Be(name);
    }
}
```

## Use NSubstitute for Mocks

```csharp
// Create mock
var mockRepository = Substitute.For<IUserRepository>();

// Setup
mockRepository.GetUser(Arg.Any<int>()).Returns(new UserEntity { Id = 1 });

// Verify
mockRepository.Received().GetUser(1);
mockRepository.DidNotReceive().GetUser(2);
```

Or use helper extensions:

```csharp
mockRepository.MustHaveBeenCalledOnce();
mockRepository.MustNotHaveBeenCalled();
```

## Naming Conventions

```
ClassName_WhenCondition_ShouldExpectedResult

Examples:
- UserEntity_WhenCreated_ShouldHaveDefaults
- UserEntity_WithInvalidEmail_ShouldFail
- Repository_WhenEmpty_ShouldReturnNotFound
- Service_WithNullInput_ShouldThrowException
```

## FluentAssertions Examples

```csharp
// Basic
user.Should().NotBeNull();
user.Id.Should().Be(1);
user.Name.Should().Be("John");

// Strings
email.Should().Contain("@");
name.Should().StartWith("J");
description.Should().BeNullOrEmpty();

// Collections
list.Should().HaveCount(5);
list.Should().Contain(item);
list.Should().BeEmpty();

// Numbers
count.Should().BeGreaterThan(0);
price.Should().BeApproximately(99.99m, 0.01m);
percentage.Should().BeLessThanOrEqualTo(100);

// Exceptions
Action act = () => throw new ArgumentNullException();
act.Should().Throw<ArgumentNullException>();

// Objects
user.Should().BeEquivalentTo(expectedUser);
```

## File Structure Template

When adding a new domain entity:

1. Add constants in `TestConstants/Constants.cs`
2. Create factory in `Factories/EntityFactory.cs`
3. Create assertions in `Extensions/EntityAssertionExtensions.cs`
4. Write tests in `tests/unit/` or `tests/integration/`

## Run Tests

```bash
# All tests
dotnet test Clean.Architecture.Template.sln

# Specific project
dotnet test tests/unit/Clean.Architecture.Template.Core.UnitTests/

# Specific test
dotnet test --filter "MethodName=UserEntity_WhenCreated_ShouldHaveValidProperties"

# With output
dotnet test -v normal

# With coverage
dotnet test /p:CollectCoverage=true
```

## Documentation

- **TEST_IMPLEMENTATION_GUIDE.md** - Complete implementation details
- **TEST_STRUCTURE_REFERENCE.md** - Reference architecture patterns
- This file - Quick reference for common tasks

---

**Remember**: Write tests following these patterns, and your test suite will be:
- ✅ Maintainable
- ✅ Readable
- ✅ Reusable
- ✅ Scalable
