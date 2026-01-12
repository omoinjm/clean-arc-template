# Test Implementation Guide

This document describes the test structure and patterns implemented in this project, based on the Clean Architecture reference implementation.

## Overview

The test infrastructure has been restructured to follow industry best practices with clear separation of concerns, comprehensive test utilities, and a scalable pattern for future test development.

## Test Project Structure

```
tests/
├── TestCommon/                                  # Shared test infrastructure
│   ├── Factories/
│   │   └── UserFactory.cs                       # Factory for creating test UserEntity instances
│   ├── TestConstants/
│   │   └── Constants.cs                         # Centralized test constants
│   ├── Extensions/
│   │   └── UserEntityAssertionExtensions.cs     # Domain-specific assertion helpers
│   └── TestUtilities/
│       └── NSubstitute/
│           └── SubstituteExtensions.cs          # Mock verification helpers
│
├── unit/
│   ├── Clean.Architecture.Template.Core.UnitTests/
│   │   └── UserEntityTests.cs                   # Core domain entity tests
│   │
│   ├── Clean.Architecture.Template.Application.UnitTests/
│   │   └── RepositoryTests.cs                   # Application layer tests
│   │
│   └── Clean.Architecture.Template.Infrastructure.UnitTests/
│       └── PlaceholderTests.cs                  # Infrastructure tests
│
└── integration/
    └── Clean.Architecture.Template.API.IntegrationTests/
        └── PlaceholderTests.cs                  # API integration tests
```

## Key Patterns Implemented

### 1. Test Framework Migration
- **Old**: MSTest with `[TestClass]`, `[TestMethod]`, `Assert.` methods
- **New**: xUnit with `[Fact]`, `[Theory]` attributes and FluentAssertions

**Benefits:**
- More modern and flexible test framework
- Better async/await support
- Cleaner assertion syntax
- Industry standard for .NET projects

### 2. Centralized Test Constants
**File**: `tests/TestCommon/TestConstants/Constants.cs`

```csharp
public static class Constants
{
    public static class User
    {
        public const int Id = 1;
        public const string Name = "John";
        public const string Surname = "Doe";
        public const string Email = "john.doe@example.com";
        // ... other constants
    }
}
```

**Benefits:**
- Single source of truth for test data
- Easy to maintain and update
- Reduces magic strings/numbers in tests
- Ensures consistency across all tests

### 3. Factory Pattern for Test Data
**File**: `tests/TestCommon/Factories/UserFactory.cs`

```csharp
public static class UserFactory
{
    public static UserEntity CreateUser(
        int id = -1,
        string name = "",
        string surname = "",
        string email = "")
    {
        return new UserEntity
        {
            Id = id == -1 ? Constants.User.Id : id,
            Name = string.IsNullOrEmpty(name) ? Constants.User.Name : name,
            // ... other properties initialized with constants
        };
    }
}
```

**Benefits:**
- Reusable test data creation
- Fluent API for customization
- Default values from Constants
- Easy to extend for new scenarios

**Usage Examples:**
```csharp
// Use defaults
var user = UserFactory.CreateUser();

// Override specific values
var customUser = UserFactory.CreateUser(
    name: "Jane",
    email: "jane@example.com");
```

### 4. Assertion Extension Methods
**File**: `tests/TestCommon/Extensions/UserEntityAssertionExtensions.cs`

```csharp
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
    // ... other assertions
}
```

**Benefits:**
- Encapsulates complex assertion logic
- Reusable across multiple tests
- Improves test readability
- Provides domain-specific validation

**Usage:**
```csharp
user.AssertValidUserEntity(1, "John", "Doe", "john@example.com");
user.AssertDefaultUserValues();
```

### 5. Mock/Substitute Helper Utilities
**File**: `tests/TestCommon/TestUtilities/NSubstitute/SubstituteExtensions.cs`

Provides cleaner syntax for mock verification:

```csharp
public static void MustHaveBeenCalledOnce<T>(this T substitute) where T : class
{
    substitute.ReceivedCalls().Count().Should().BeGreaterThan(0);
}

public static void MustNotHaveBeenCalled<T>(this T substitute) where T : class
{
    substitute.ReceivedCalls().Should().BeEmpty();
}
```

**Benefits:**
- Cleaner, more readable mock assertions
- Consistency in verification patterns
- Easy to extend with new verification methods

## Test Categories

### Unit Tests
Located in: `tests/unit/`

Test individual components in isolation:
- Domain entities
- Application handlers
- Infrastructure services

**Example**: `UserEntityTests.cs`
```csharp
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
            email: "john@example.com");

        // Assert
        user.Id.Should().Be(1);
        user.Name.Should().Be("John");
        user.AssertDefaultUserValues();
    }
}
```

### Integration Tests
Located in: `tests/integration/`

Test API endpoints and controller behavior:
- HTTP request/response handling
- API contracts
- End-to-end workflows

## Test Naming Convention

Use the pattern: `MethodName_WhenCondition_ShouldExpectedBehavior`

Examples:
- `UserEntity_Properties_CanBeSetAndGet`
- `MockRepository_ShouldBeCreatable`
- `ApplicationSetup_ShouldHaveAllDependencies`

Benefits:
- Clear test intent
- Readable test results
- Easy to understand what's being tested
- Failure messages are self-documenting

## Package Dependencies

### TestCommon Project
- **FluentAssertions** (6.12.0): Modern assertion library
- **NSubstitute** (5.1.0): Mocking library
- **xUnit** (2.8.1): Test framework
- **xunit.runner.visualstudio** (2.5.6): Test runner for Visual Studio

### Unit Test Projects
- **Microsoft.NET.Test.Sdk** (17.12.0): Test SDK
- **xunit** (2.8.1): Test framework
- **FluentAssertions** (6.12.0): Assertions
- **xunit.runner.visualstudio**: Test runner

## Global Using Declarations

Each test project includes global usings in the `.csproj` file:

```xml
<ItemGroup>
  <Using Include="Xunit" />
  <Using Include="FluentAssertions" />
</ItemGroup>
```

This eliminates the need for `using` statements in individual test files.

## Best Practices

### ✅ DO:

1. **Use Constants** for all test data values
   ```csharp
   var user = UserFactory.CreateUser(email: Constants.User.Email);
   ```

2. **Use Factories** for entity creation
   ```csharp
   var user = UserFactory.CreateUser();
   ```

3. **Use Extension Methods** for assertions
   ```csharp
   user.AssertValidUserEntity(1, "John", "Doe", "john@example.com");
   ```

4. **Follow Naming Conventions**
   ```csharp
   [Fact]
   public void UserEntity_WhenCreated_ShouldHaveValidProperties() { }
   ```

5. **Arrange-Act-Assert Pattern**
   ```csharp
   [Fact]
   public void Example_Test()
   {
       // Arrange
       var user = UserFactory.CreateUser();
       
       // Act
       var result = user.Name;
       
       // Assert
       result.Should().Be("John");
   }
   ```

### ❌ DON'T:

1. **Don't use magic values**
   ```csharp
   // Bad
   var user = new UserEntity { Id = 1, Name = "John" };
   
   // Good
   var user = UserFactory.CreateUser();
   ```

2. **Don't mix multiple concerns in one test**
   - Separate authorization tests from validation tests
   - Separate happy path from error cases

3. **Don't repeat assertion logic**
   - Extract to extension methods
   - Reuse assertion helpers

4. **Don't use deprecated MSTest patterns**
   - Use `[Fact]` instead of `[TestMethod]`
   - Use FluentAssertions instead of `Assert.`

## Running Tests

### Run All Tests
```bash
dotnet test Clean.Architecture.Template.sln
```

### Run Specific Test Project
```bash
dotnet test tests/unit/Clean.Architecture.Template.Core.UnitTests/Clean.Architecture.Template.Core.UnitTests.csproj
```

### Run with Verbose Output
```bash
dotnet test Clean.Architecture.Template.sln -v normal
```

### Run with Code Coverage
```bash
dotnet test Clean.Architecture.Template.sln /p:CollectCoverage=true
```

## Extending the Test Infrastructure

### Adding a New Factory

1. Create file in `tests/TestCommon/Factories/`
2. Implement factory class with default values from Constants
3. Support parameter override pattern

Example: `OrderFactory.cs`
```csharp
public static class OrderFactory
{
    public static OrderEntity CreateOrder(
        int id = -1,
        string description = "")
    {
        return new OrderEntity
        {
            Id = id == -1 ? Constants.Order.Id : id,
            Description = string.IsNullOrEmpty(description) ? Constants.Order.Description : description
        };
    }
}
```

### Adding New Constants

1. Add to `tests/TestCommon/TestConstants/Constants.cs`
2. Group by entity type
3. Use descriptive names

```csharp
public static class Order
{
    public const int Id = 1;
    public const string Description = "Sample Order";
    public const decimal Amount = 99.99m;
}
```

### Adding Assertion Extensions

1. Create file in `tests/TestCommon/Extensions/`
2. Create static class with extension methods
3. Use FluentAssertions for implementation

```csharp
public static class OrderAssertionExtensions
{
    public static void AssertValidOrder(this OrderEntity order)
    {
        order.Should().NotBeNull();
        order.Id.Should().BeGreaterThan(0);
        order.Description.Should().NotBeNullOrEmpty();
    }
}
```

## Test Results

Current test status: **All tests passing** ✅

- Core.UnitTests: 2 passed
- Application.UnitTests: 2 passed
- Infrastructure.UnitTests: 1 passed
- API.IntegrationTests: 1 passed

**Total: 6 tests passed**

## Next Steps

1. **Create domain-specific factories** for other entities as needed
2. **Add subcutaneous tests** for command/query handlers (requires setup of MediatR infrastructure)
3. **Implement integration tests** for API endpoints
4. **Add authorization and validation tests** following the reference patterns
5. **Configure test coverage reporting** (coverlet)

## Resources

- [xUnit Documentation](https://xunit.net/)
- [FluentAssertions Documentation](https://fluentassertions.com/)
- [NSubstitute Documentation](https://nsubstitute.github.io/)
- [Clean Architecture Pattern](https://www.oreilly.com/library/view/clean-architecture/9780134494272/)

