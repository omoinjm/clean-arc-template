# Test Structure Analysis - Clean Architecture Reference

## Overview
This document compares the test structure of the reference Clean Architecture repository (amantinband/clean-architecture) with your current template structure and provides recommendations for implementation.

---

## Reference Repository Structure

### Folder Organization
```
tests/
├── TestCommon/                                    # Shared test utilities & factories
│   ├── TestConstants/                             # Constant values for tests
│   │   ├── Constants.Subscription.cs
│   │   ├── Constants.User.cs
│   │   └── Constants.Reminder.cs
│   ├── Subscriptions/                             # Domain-specific test helpers
│   │   ├── SubscriptionCommandFactory.cs          # Factory for creating commands
│   │   ├── SubscriptionQueryFactory.cs            # Factory for creating queries
│   │   ├── SubscriptionFactory.cs                 # Factory for creating entities
│   │   ├── SubscriptionValidationExtensions.cs    # Assertion helpers
│   │   └── MediatorExtensions.cs                  # MediatR test helpers
│   ├── Reminders/                                 # Similar to Subscriptions
│   ├── Users/
│   ├── Security/                                  # Security & auth test utilities
│   │   └── TestCurrentUserProvider.cs
│   └── TestUtilities/
│       └── NSubstitute/
│           └── Must.cs                            # NSubstitute assertion helpers
│
├── CleanArchitecture.Domain.UnitTests/            # Domain entity tests
│   ├── Users/
│   │   └── UserTests.cs
│   └── Reminders/
│       └── ReminderTests.cs
│
├── CleanArchitecture.Application.UnitTests/       # Application layer tests
│   └── Common/
│       └── Behaviors/                             # MediatR behavior tests
│           ├── ValidationBehaviorTests.cs
│           └── AuthorizationBehaviorTests.cs
│
├── CleanArchitecture.Application.SubcutaneousTests/  # Subcutaneous tests
│   ├── Common/                                       # Shared test setup
│   │   └── WebApplicationFactory/
│   └── Subscriptions/
│       ├── Commands/
│       │   └── CreateSubscription/
│       │       ├── CreateSubscriptionTests.cs        # Main functionality tests
│       │       ├── CreateSubscription.AuthorizationTests.cs  # Separate auth tests
│       │       └── CreateSubscription.ValidationTests.cs     # Separate validation tests
│       └── Queries/
│           └── GetSubscription/
│               ├── GetSubscriptionTests.cs
│               ├── GetSubscription.AuthorizationTests.cs
│               └── GetSubscription.ValidationTests.cs
│
└── CleanArchitecture.Api.IntegrationTests/       # API/Controller tests
    └── Controllers/
```

---

## Key Patterns from Reference Repository

### 1. Test Separation Pattern
**Instead of one large test file, split tests by concern:**

```
CreateSubscription/
├── CreateSubscriptionTests.cs                      # Happy path, core functionality
├── CreateSubscription.AuthorizationTests.cs        # Authorization/permission tests
└── CreateSubscription.ValidationTests.cs           # Validation/error cases
```

**Benefits:**
- Each test class focuses on a single concern
- Better test readability and maintenance
- Easier to run specific test categories
- Clearer failure messages

### 2. Test Fixture Pattern
Use xUnit's `[Collection]` attribute and `WebAppFactory`:

```csharp
[Collection(WebAppFactoryCollection.CollectionName)]
public class CreateSubscriptionTests(WebAppFactory webAppFactory)
{
    private readonly IMediator _mediator = webAppFactory.CreateMediator();
    
    [Fact]
    public async Task CreateSubscription_WhenNoSubscription_ShouldCreateSubscription()
    {
        // Arrange
        var command = SubscriptionCommandFactory.CreateCreateSubscriptionCommand();
        
        // Act
        var result = await _mediator.Send(command);
        
        // Assert
        result.IsError.Should().BeFalse();
    }
}
```

### 3. Factory Pattern for Test Data
Create centralized factories in `TestCommon`:

```csharp
// TestCommon/Subscriptions/SubscriptionCommandFactory.cs
public static class SubscriptionCommandFactory
{
    public static CreateSubscriptionCommand CreateCreateSubscriptionCommand(
        Guid? userId = null,
        string firstName = Constants.User.FirstName,
        string lastName = Constants.User.LastName,
        string email = Constants.User.Email,
        SubscriptionType? subscriptionType = null)
    {
        return new CreateSubscriptionCommand(
            userId ?? Constants.User.Id,
            firstName,
            lastName,
            email,
            subscriptionType ?? Constants.Subscription.Type);
    }
}
```

### 4. Test Constants
Centralize test data constants:

```csharp
// TestCommon/TestConstants/Constants.Subscription.cs
namespace TestCommon.TestConstants;

public static class Constants
{
    public static class User
    {
        public const string FirstName = "John";
        public const string LastName = "Doe";
        public const string Email = "john.doe@example.com";
        public static readonly Guid Id = Guid.Parse("00000000-0000-0000-0000-000000000001");
    }
    
    public static class Subscription
    {
        public static readonly Guid Id = Guid.Parse("00000000-0000-0000-0000-000000000002");
        public const SubscriptionType Type = SubscriptionType.Premium;
    }
}
```

### 5. Assertion Extensions
Create domain-specific assertion helpers:

```csharp
// TestCommon/Subscriptions/SubscriptionValidationExtensions.cs
public static class SubscriptionAssertionExtensions
{
    public static void AssertCreatedFrom(this SubscriptionResponse response, 
        CreateSubscriptionCommand command)
    {
        response.FirstName.Should().Be(command.FirstName);
        response.LastName.Should().Be(command.LastName);
        response.Email.Should().Be(command.Email);
    }
}
```

### 6. MediatR Test Extensions
Centralize MediatR helper methods:

```csharp
// TestCommon/Subscriptions/MediatorExtensions.cs
public static class MediatorExtensions
{
    public static async Task<Result<SubscriptionResponse>> GetSubscriptionAsync(
        this IMediator mediator,
        Guid? userId = null)
    {
        return await mediator.Send(
            new GetSubscriptionQuery(userId ?? Constants.User.Id));
    }
}
```

### 7. Test Naming Convention
Use clear, descriptive test names following the pattern:
```
MethodName_WhenCondition_ShouldExpectedBehavior
```

Examples:
- `CreateSubscription_WhenNoSubscription_ShouldCreateSubscription`
- `CreateSubscription_WhenSubscriptionAlreadyExists_ShouldReturnConflict`
- `CreateSubscription_WhenInvalidFirstName_ShouldReturnValidationError`

### 8. Test Categories
**Subcutaneous Tests** - Test through the application layer with database:
- Setup real database
- Test end-to-end within app boundaries
- Use WebAppFactory for setup

**Unit Tests** - Test individual components in isolation:
- Mock dependencies
- Test specific behavior
- Fast execution

**Integration Tests** - Test API controllers:
- Use WebApplicationFactory
- Make real HTTP requests
- Test API contracts

---

## Current Template Structure Issues

Your current structure has:
- Single large test files instead of separated concerns
- Using MSTest instead of xUnit (reference uses xUnit)
- Limited test data factory pattern
- No centralized test constants

---

## Recommended Migration Steps

### Step 1: Update TestCommon Project
Add the following to `tests/TestCommon/Clean.Architecture.Template.TestCommon.csproj`:

```xml
<ItemGroup>
  <PackageReference Include="FluentAssertions" Version="6.12.0" />
  <PackageReference Include="NSubstitute" Version="5.1.0" />
  <PackageReference Include="xunit" Version="2.8.1" />
</ItemGroup>
```

### Step 2: Create Test Constants Structure
```
tests/TestCommon/
├── TestConstants/
│   └── Constants.cs
├── Factories/
│   ├── UserFactory.cs
│   └── (other domain factories)
└── Extensions/
    └── (assertion extensions)
```

### Step 3: Switch from MSTest to xUnit
Change project files to use xUnit:
- Update `*.csproj` files
- Replace `[TestClass]` with class declarations
- Replace `[TestMethod]` with `[Fact]` or `[Theory]`
- Replace `Assert.` with `FluentAssertions` or xUnit assertions

### Step 4: Separate Test Files by Concern
For each command/query handler:
```
CommandName/
├── CommandNameTests.cs                    # Core functionality
├── CommandName.AuthorizationTests.cs      # Authorization tests
└── CommandName.ValidationTests.cs         # Validation/error cases
```

### Step 5: Create Subcutaneous Test Layer
```
tests/subcutaneous/
├── Common/
│   └── WebAppFactory.cs
└── YourDomain/
    ├── Commands/...
    └── Queries/...
```

---

## Benefits of This Structure

1. **Clarity**: Each test file has a single responsibility
2. **Maintainability**: Easier to locate and modify tests
3. **Reusability**: Shared factories and constants reduce duplication
4. **Scalability**: Pattern scales well as application grows
5. **Best Practices**: Follows industry standards for Clean Architecture testing

---

## Key Takeaways

✅ **Do:**
- Separate tests by concern (happy path, auth, validation)
- Use factories for test data
- Centralize constants and helpers in TestCommon
- Use xUnit with FluentAssertions
- Create descriptive test names

❌ **Don't:**
- Mix multiple test concerns in one file
- Duplicate test data creation across files
- Use magic strings/values in tests
- Skip authorization/validation test coverage

---

## Reference Files Worth Studying

From the reference repository, examine:
1. `/tests/TestCommon/Subscriptions/SubscriptionCommandFactory.cs` - Factory pattern
2. `/tests/CleanArchitecture.Application.SubcutaneousTests/Subscriptions/Commands/CreateSubscription/CreateSubscriptionTests.cs` - Core test structure
3. `/tests/CleanArchitecture.Application.SubcutaneousTests/Subscriptions/Commands/CreateSubscription/CreateSubscription.AuthorizationTests.cs` - Authorization tests
4. `/tests/CleanArchitecture.Application.SubcutaneousTests/Subscriptions/Commands/CreateSubscription/CreateSubscription.ValidationTests.cs` - Validation tests
5. `/tests/TestCommon/TestConstants/` - Constants structure

