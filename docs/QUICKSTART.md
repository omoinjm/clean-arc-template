# Quick Start Guide

Get up and running with the Clean Architecture Template in 5 minutes.

## Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later
- Git
- Your favorite IDE (Visual Studio 2022, VS Code, or Rider)

## Installation

### Option 1: Clone This Repository

```bash
git clone https://github.com/yourusername/clean-arc-template.git
cd clean-arc-template
```

# Quick Start Guide

Get up and running with the Clean Architecture Template in 5 minutes.

## Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later
- Git
- Your favorite IDE (Visual Studio 2022, VS Code, or Rider)

## Installation

### Option 1: Clone This Repository

```bash
git clone https://github.com/yourusername/clean-arc-template.git
cd clean-arc-template
```

### Option 2: Use as a .NET Template from NuGet.org

```bash
# Install the template
dotnet new install Clean.Architecture.Solution.Template::9.0.8

# Create a new project
dotnet new ca-sln -o MyNewProject
cd MyNewProject
```

### Option 3: Use as a .NET Template from GitHub Packages

If the template is published to GitHub Packages, follow these steps:

1.  **Add GitHub Packages as a NuGet Source:**
    Configure NuGet to recognize GitHub Packages as a package source. Replace `<YOUR_GITHUB_USERNAME_OR_ORG>` with your GitHub username or organization, and `<YOUR_GITHUB_PAT>` with a GitHub Personal Access Token (PAT) that has `read:packages` scope.

    ```bash
    dotnet nuget add source https://nuget.pkg.github.com/<YOUR_GITHUB_USERNAME_OR_ORG>/index.json \
      --name GitHubPackages \
      --username <YOUR_GITHUB_USERNAME_OR_ORG> \
      --password <YOUR_GITHUB_PAT>
    ```

2.  **Install the Template:**
    Once the NuGet source is configured, install the template using its NuGet package ID. Replace `<PACKAGE_ID_OF_YOUR_TEMPLATE>` with the actual NuGet package ID.

    ```bash
    dotnet new --install <PACKAGE_ID_OF_YOUR_TEMPLATE>
    ```

After these steps, you can list installed templates to verify:

```bash
dotnet new --list
```

And then create a new project using your template:

```bash
dotnet new <TEMPLATE_SHORT_NAME> -n MyNewProject
```
*(You'll need to know the `short name` of your template, which is defined in its `.template.config/template.json` file.)*

## Project Structure

```
src/
├── Clean.Architecture.Template.Core/              # Business logic & entities
├── Clean.Architecture.Template.Application/       # Use cases & handlers
├── Clean.Architecture.Template.Infrastructure/    # Database & external services
└── Clean.Architecture.Template.API/               # REST API controllers

tests/
└── Clean.Architecture.Template.Application.Tests/ # Unit tests
```

## Building and Running

### 1. Restore Dependencies

```bash
dotnet restore
```

### 2. Build the Solution

```bash
dotnet build
```

### 3. Run Tests

```bash
dotnet test
```

### 4. Run the API

```bash
dotnet run --project src/Clean.Architecture.Template.API
```

The API will start at `https://localhost:5001`

## First API Call

### Check Health

```bash
curl https://localhost:5001/api/health/status
```

Expected response:
```json
{
  "status": "healthy",
  "timestamp": "2024-01-22T10:30:00Z"
}
```

### View Swagger Documentation

Open in your browser:
```
https://localhost:5001/swagger
```

## Creating Your First Feature

Follow this checklist to add a new API endpoint:

### 1. Create Entity (Core Layer)
- [ ] Add entity class to `src/Clean.Architecture.Template.Core/Entity/`
- [ ] Inherit from `BaseEntity`

### 2. Create Repository Interface (Core Layer)
- [ ] Add interface to `src/Clean.Architecture.Template.Core/Repository/`
- [ ] Define CRUD methods

### 3. Create CQRS Objects (Application Layer)
- [ ] Add command(s) to `src/Clean.Architecture.Template.Application/Commands/`
- [ ] Add query/queries to `src/Clean.Architecture.Template.Application/Queries/`
- [ ] Add response DTO to `src/Clean.Architecture.Template.Application/Response/`

### 4. Create Handlers (Application Layer)
- [ ] Add handler(s) to `src/Clean.Architecture.Template.Application/Handlers/`
- [ ] Implement `IRequestHandler<TRequest, TResponse>`

### 5. Implement Repository (Infrastructure Layer)
- [ ] Add repository implementation to `src/Clean.Architecture.Template.Infrastructure/Repository/`
- [ ] Implement the interface from step 2

### 6. Create Controller (API Layer)
- [ ] Add controller to `src/Clean.Architecture.Template.API/Controllers/`
- [ ] Inject `IMediator` and use it to send commands/queries

### 7. Register Dependencies (API Layer)
- [ ] Add DI registration to `src/Clean.Architecture.Template.API/Program.cs`

### 8. Add AutoMapper Profile (Application Layer)
- [ ] Add mapping profile to `src/Clean.Architecture.Template.Application/Mapper/`

### 9. Write Tests
- [ ] Add unit tests to `tests/Clean.Architecture.Template.Application.Tests/`

## File Templates

### Entity Template

```csharp
using Clean.Architecture.Template.Core.Entity.Common;

public class MyEntity : BaseEntity
{
    public string Name { get; set; }
    // Add your properties here
}
```

### Command Template

```csharp
using MediatR;
using Clean.Architecture.Template.Application.Response.General;

public class CreateMyCommand : IRequest<CreateResponse>
{
    public string Name { get; set; }
    // Add command properties here
}
```

### Query Template

```csharp
using MediatR;

public class GetMyQuery : IRequest<MyResponse>
{
    public int Id { get; set; }
}
```

### Handler Template

```csharp
using MediatR;
using Clean.Architecture.Template.Core.Repository;

public class CreateMyHandler : IRequestHandler<CreateMyCommand, CreateResponse>
{
    private readonly IMyRepository _repository;

    public CreateMyHandler(IMyRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateResponse> Handle(CreateMyCommand request, CancellationToken cancellationToken)
    {
        // Implement your logic here
        return new CreateResponse { IsSuccessful = true };
    }
}
```

### Controller Template

```csharp
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MyController : ControllerBase
{
    private readonly IMediator _mediator;

    public MyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<CreateResponse>> Create([FromBody] CreateMyCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MyResponse>> Get(int id)
    {
        var result = await _mediator.Send(new GetMyQuery { Id = id });
        return Ok(result);
    }
}
```

## Key Concepts

### Clean Architecture Layers

1. **Core** - Business rules, never changes
2. **Application** - Use cases and orchestration
3. **Infrastructure** - Implementation details
4. **API** - External interfaces

### CQRS Pattern

- **Commands** - Modify state (Create, Update, Delete)
- **Queries** - Retrieve data (Get, List)

### Dependency Injection

All dependencies are registered in `Program.cs` and injected via constructors.

## Troubleshooting

### Build Fails

```bash
dotnet clean
dotnet restore
dotnet build
```

### Tests Won't Run

```bash
dotnet test --logger "console;verbosity=detailed"
```

### Port Already in Use

Change the port in `launchSettings.json` or environment variables.

## Next Steps

1. Read [API_DOCUMENTATION.md](./API_DOCUMENTATION.md) for detailed feature creation
2. Check [../CONTRIBUTING.md](../CONTRIBUTING.md) to contribute
3. Review the example handlers in `src/Clean.Architecture.Template.Application/Handlers/`

## Tips & Tricks

- Use AutoMapper for entity-to-DTO mapping
- Always use repositories for data access
- Keep business logic out of controllers
- Write unit tests for handlers
- Use Swagger UI to test endpoints
- Check `launchSettings.json` for configuration

## Support

- 📖 Read the [../README.md](../README.md) for complete documentation
- 🐛 Open an issue for bugs
- 💡 Suggest features via discussions

---

Happy coding! 🚀

## Project Structure

```
src/
├── Clean.Architecture.Template.Core/              # Business logic & entities
├── Clean.Architecture.Template.Application/       # Use cases & handlers
├── Clean.Architecture.Template.Infrastructure/    # Database & external services
└── Clean.Architecture.Template.API/               # REST API controllers

tests/
└── Clean.Architecture.Template.Application.Tests/ # Unit tests
```

## Building and Running

### 1. Restore Dependencies

```bash
dotnet restore
```

### 2. Build the Solution

```bash
dotnet build
```

### 3. Run Tests

```bash
dotnet test
```

### 4. Run the API

```bash
dotnet run --project src/Clean.Architecture.Template.API
```

The API will start at `https://localhost:5001`

## First API Call

### Check Health

```bash
curl https://localhost:5001/api/health/status
```

Expected response:
```json
{
  "status": "healthy",
  "timestamp": "2024-01-22T10:30:00Z"
}
```

### View Swagger Documentation

Open in your browser:
```
https://localhost:5001/swagger
```

## Creating Your First Feature

Follow this checklist to add a new API endpoint:

### 1. Create Entity (Core Layer)
- [ ] Add entity class to `src/Clean.Architecture.Template.Core/Entity/`
- [ ] Inherit from `BaseEntity`

### 2. Create Repository Interface (Core Layer)
- [ ] Add interface to `src/Clean.Architecture.Template.Core/Repository/`
- [ ] Define CRUD methods

### 3. Create CQRS Objects (Application Layer)
- [ ] Add command(s) to `src/Clean.Architecture.Template.Application/Commands/`
- [ ] Add query/queries to `src/Clean.Architecture.Template.Application/Queries/`
- [ ] Add response DTO to `src/Clean.Architecture.Template.Application/Response/`

### 4. Create Handlers (Application Layer)
- [ ] Add handler(s) to `src/Clean.Architecture.Template.Application/Handlers/`
- [ ] Implement `IRequestHandler<TRequest, TResponse>`

### 5. Implement Repository (Infrastructure Layer)
- [ ] Add repository implementation to `src/Clean.Architecture.Template.Infrastructure/Repository/`
- [ ] Implement the interface from step 2

### 6. Create Controller (API Layer)
- [ ] Add controller to `src/Clean.Architecture.Template.API/Controllers/`
- [ ] Inject `IMediator` and use it to send commands/queries

### 7. Register Dependencies (API Layer)
- [ ] Add DI registration to `src/Clean.Architecture.Template.API/Program.cs`

### 8. Add AutoMapper Profile (Application Layer)
- [ ] Add mapping profile to `src/Clean.Architecture.Template.Application/Mapper/`

### 9. Write Tests
- [ ] Add unit tests to `tests/Clean.Architecture.Template.Application.Tests/`

## File Templates

### Entity Template

```csharp
using Clean.Architecture.Template.Core.Entity.Common;

public class MyEntity : BaseEntity
{
    public string Name { get; set; }
    // Add your properties here
}
```

### Command Template

```csharp
using MediatR;
using Clean.Architecture.Template.Application.Response.General;

public class CreateMyCommand : IRequest<CreateResponse>
{
    public string Name { get; set; }
    // Add command properties here
}
```

### Query Template

```csharp
using MediatR;

public class GetMyQuery : IRequest<MyResponse>
{
    public int Id { get; set; }
}
```

### Handler Template

```csharp
using MediatR;
using Clean.Architecture.Template.Core.Repository;

public class CreateMyHandler : IRequestHandler<CreateMyCommand, CreateResponse>
{
    private readonly IMyRepository _repository;

    public CreateMyHandler(IMyRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateResponse> Handle(CreateMyCommand request, CancellationToken cancellationToken)
    {
        // Implement your logic here
        return new CreateResponse { IsSuccessful = true };
    }
}
```

### Controller Template

```csharp
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MyController : ControllerBase
{
    private readonly IMediator _mediator;

    public MyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<CreateResponse>> Create([FromBody] CreateMyCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MyResponse>> Get(int id)
    {
        var result = await _mediator.Send(new GetMyQuery { Id = id });
        return Ok(result);
    }
}
```

## Key Concepts

### Clean Architecture Layers

1. **Core** - Business rules, never changes
2. **Application** - Use cases and orchestration
3. **Infrastructure** - Implementation details
4. **API** - External interfaces

### CQRS Pattern

- **Commands** - Modify state (Create, Update, Delete)
- **Queries** - Retrieve data (Get, List)

### Dependency Injection

All dependencies are registered in `Program.cs` and injected via constructors.

## Troubleshooting

### Build Fails

```bash
dotnet clean
dotnet restore
dotnet build
```

### Tests Won't Run

```bash
dotnet test --logger "console;verbosity=detailed"
```

### Port Already in Use

Change the port in `launchSettings.json` or environment variables.

## Next Steps

1. Read [API_DOCUMENTATION.md](./API_DOCUMENTATION.md) for detailed feature creation
2. Check [../CONTRIBUTING.md](../CONTRIBUTING.md) to contribute
3. Review the example handlers in `src/Clean.Architecture.Template.Application/Handlers/`

## Tips & Tricks

- Use AutoMapper for entity-to-DTO mapping
- Always use repositories for data access
- Keep business logic out of controllers
- Write unit tests for handlers
- Use Swagger UI to test endpoints
- Check `launchSettings.json` for configuration

## Support

- 📖 Read the [../README.md](../README.md) for complete documentation
- 🐛 Open an issue for bugs
- 💡 Suggest features via discussions

---

Happy coding! 🚀
