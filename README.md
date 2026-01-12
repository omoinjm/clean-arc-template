[![Build](https://github.com/omoinjm/clean-arc-template/actions/workflows/docker-ci.yml/badge.svg)](https://github.com/omoinjm/clean-arc-template/actions/workflows/docker-ci.yml) [![Publish template to NuGet](https://github.com/omoinjm/clean-arc-template/actions/workflows/build-and-pack.yml/badge.svg)](https://github.com/omoinjm/clean-arc-template/actions/workflows/docker-ci.yml)

[![GitHub contributors](https://img.shields.io/github/contributors/omoinjm/clean-arc-template)](https://github.com/omoinjm/clean-arc-template/graphs/contributors/) [![GitHub Stars](https://img.shields.io/github/stars/omoinjm/clean-arc-template.svg)](https://github.com/omoinjm/clean-arc-template/stargazers) [![GitHub license](https://img.shields.io/github/license/omoinjm/clean-arc-template)](https://github.com/omoinjm/clean-arc-template/blob/main/LICENSE)
[![codecov](https://codecov.io/gh/omoinjm/clean-arc-template/branch/main/graph/badge.svg?token=DR2EBIWK7B)](https://codecov.io/gh/omoinjm/clean-arc-template)

---

![Clean Architecture Template Title](assets/Clean%20Architecture%20Template%20Title.png)

---

The goal of this template is to provide a straightforward and efficient approach to enterprise application development, leveraging the power of Clean Architecture and ASP.NET Core. Using this template, you can effortlessly create REST APIs with ASP.NET Core, while adhering to the principles of Clean Architecture. Getting started is easy - simply install the **.NET template** (see below for full details).

If you find this project useful, please give it a star. Thanks! ⭐

## 📋 Table of Contents

- [Architecture Overview](#architecture-overview)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Technology Stack](#technology-stack)
- [Building and Running](#building-and-running)
- [Best Practices](#best-practices)
- [Documentation](#documentation)

## Architecture Overview

This template implements **Clean Architecture** with clear separation of concerns:

### **Layers:**

1. **Core Layer** (`Clean.Architecture.Template.Core`)
   - Domain entities and business logic
   - Repository interfaces
   - Specifications for business rules
   - Service interfaces
   - Enums and attributes

2. **Application Layer** (`Clean.Architecture.Template.Application`)
   - Commands and Queries (CQRS pattern)
   - Request handlers (MediatR)
   - DTOs and Response objects
   - AutoMapper profiles
   - Business logic orchestration

3. **Infrastructure Layer** (`Clean.Architecture.Template.Infrastructure`)
   - Database repositories implementation
   - External service integrations
   - Caching services
   - Database queries
   - Configuration services

4. **API Layer** (`Clean.Architecture.Template.API`)
   - ASP.NET Core Web API
   - Controllers
   - Dependency injection configuration
   - Middleware setup
   - Swagger documentation

## Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) (latest version)
- Visual Studio 2022 or your preferred IDE

### Installation

The easiest way to get started is to install the [.NET template](https://www.nuget.org/packages/Clean.Architecture.Solution.Template):

```bash
dotnet new install Clean.Architecture.Solution.Template::9.0.8
```

### Local Development Installation

If you want to install the template from your local machine to test changes, you can install it directly from the project's root directory.

**Install:**
Use this command to install the template from the local source code:

```bash
dotnet new install .
```

**Create a new project:**
Once installed, you can create a new solution with this command:

```bash
dotnet new clean-arc-sln -n YourProjectName
```

**Uninstall:**
To uninstall the local template, run this command from the project's root directory:

```bash
dotnet new uninstall .
```

### Creating a New Project

Once installed, create a new solution using the template:

```bash
# Create a Web API-only solution
dotnet new ca-sln -cf None -o YourProjectName

# Or using the full shorthand
dotnet new ca-sln -o YourProjectName
```

## Project Structure

```
src/
├── Clean.Architecture.Template.Core/              # Domain layer
│   ├── Attributes/                                # Custom attributes
│   ├── Auth/                                      # Authentication models
│   ├── Entity/                                    # Domain entities
│   ├── Enum/                                      # Enumerations
│   ├── Helpers/                                   # Helper utilities
│   ├── Repository/                                # Repository interfaces
│   ├── Results/                                   # Result types
│   ├── Services/                                  # Service interfaces
│   ├── Specs/                                     # Business specifications
│   └── Utils/                                     # Utility functions
│
├── Clean.Architecture.Template.Application/       # Application layer
│   ├── Commands/                                  # CQRS commands
│   ├── Handlers/                                  # MediatR request handlers
│   ├── Queries/                                   # CQRS queries
│   ├── Response/                                  # DTOs and response models
│   ├── Configuration/                             # Configuration services
│   └── Mapper/                                    # AutoMapper profiles
│
├── Clean.Architecture.Template.Infrastructure/    # Infrastructure layer
│   ├── Repository/                                # Repository implementations
│   ├── Services/                                  # Service implementations
│   └── DbQueries/                                 # SQL query builders
│
└── Clean.Architecture.Template.API/               # Presentation layer
    ├── Controllers/                               # API endpoints
    ├── Program.cs                                 # Application startup
    └── appsettings.json                           # Configuration
```

## Technology Stack

### Core Technologies

- **Framework:** ASP.NET Core 9.0
- **Language:** C# with nullable reference types
- **Pattern:** Clean Architecture + CQRS

### Key Libraries

- **MediatR** (12.4.1) - CQRS implementation
- **AutoMapper** (13.0.1) - Object mapping
- **Dapper** (2.1.35) - Data access
- **Npgsql** (9.0.2) - PostgreSQL provider
- **Swagger/Swashbuckle** (6.5.0) - API documentation

### Database

- PostgreSQL (configurable)

### Additional Features

- In-memory caching
- Azure Blob Storage support
- JWT token handling
- Dependency injection via Microsoft.Extensions.DependencyInjection

## Building and Running

### Build the Solution

```bash
dotnet build
```

### Run the API

```bash
# Development
dotnet run --project src/Clean.Architecture.Template.API

# Production
dotnet publish -c Release
```

The API will be available at `https://localhost:5001` by default.

### Swagger Documentation

Once the API is running, navigate to `https://localhost:5001/swagger` to view interactive API documentation.

## Best Practices

### 1. **Command and Query Separation**

- Use `Commands` for operations that modify state
- Use `Queries` for operations that retrieve data
- Implement corresponding handlers in the handlers folder

### 2. **Dependency Injection**

- Register services in `Program.cs`
- Use constructor injection throughout
- Avoid service locator pattern

### 3. **Entity and DTO Separation**

- Keep domain entities clean (no framework dependencies)
- Use response DTOs to shape API responses
- Map entities to DTOs using AutoMapper

### 4. **Repository Pattern**

- Define repository interfaces in the Core layer
- Implement repositories in the Infrastructure layer
- Use dependency injection to access repositories

### 5. **Error Handling**

- Use the `Result` type for consistent error handling
- Return appropriate HTTP status codes
- Log errors appropriately

### 6. **Validation**

- Validate input in handlers
- Use custom specifications for business logic validation
- Return meaningful error messages

### Example: Creating a New Feature

1. **Define the Entity** (Core/Entity)

```csharp
public class UserEntity : BaseEntity
{
    public string Email { get; set; }
    public string Username { get; set; }
}
```

2. **Create the Command/Query** (Application/Commands or Queries)

```csharp
public class CreateUserCommand : IRequest<CreateResponse>
{
    public string Email { get; set; }
    public string Username { get; set; }
}
```

3. **Implement the Handler** (Application/Handlers)

```csharp
public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateResponse>
{
    private readonly IUserRepository _repository;

    public CreateUserHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new UserEntity { Email = request.Email, Username = request.Username };
        await _repository.CreateAsync(user);
        return new CreateResponse { Id = user.Id, Success = true };
    }
}
```

4. **Create the Controller Endpoint** (API/Controllers)

```csharp
[HttpPost]
public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
{
    var result = await _mediator.Send(command);
    return Created($"/api/users/{result.Id}", result);
}
```

## 📚 Documentation

Comprehensive guides are available in the `docs/` folder:

- **[docs/QUICKSTART.md](./docs/QUICKSTART.md)** - Get started in 5 minutes
- **[docs/API_DOCUMENTATION.md](./docs/API_DOCUMENTATION.md)** - Step-by-step feature creation tutorial
- **[docs/TESTING_GUIDE.md](./docs/TESTING_GUIDE.md)** - Complete testing guide with multiple methods
- **[docs/COMPLETION_SUMMARY.md](./docs/COMPLETION_SUMMARY.md)** - What's included in this template
- **[CONTRIBUTING.md](./CONTRIBUTING.md)** - Contribution guidelines

## Contributing

Feel free to fork this repository and submit pull requests to improve the template.

## License

This project is licensed under the MIT License - see the LICENSE file for details.
