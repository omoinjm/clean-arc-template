# Clean Architecture Template - API Documentation

This document provides comprehensive guidance on using the Clean Architecture Template for building REST APIs.

## Architecture Overview

The template is organized into 4 main layers:

```
┌─────────────────────────────────────────────────────────────┐
│                    API Layer (Controllers)                  │
├─────────────────────────────────────────────────────────────┤
│            Application Layer (Commands, Queries)            │
├─────────────────────────────────────────────────────────────┤
│         Infrastructure Layer (Repositories, Services)       │
├─────────────────────────────────────────────────────────────┤
│              Core Layer (Entities, Interfaces)              │
└─────────────────────────────────────────────────────────────┘
```

## Layer Responsibilities

### Core Layer
- Domain entities and business rules
- Repository interfaces
- Service contracts
- Value objects
- Specifications and enums

### Application Layer
- CQRS Commands and Queries
- Request handlers (MediatR)
- DTOs (Data Transfer Objects)
- Business logic orchestration
- Mapper profiles

### Infrastructure Layer
- Repository implementations
- External service integrations
- Database access (Dapper)
- Caching
- Configuration management

### API Layer
- HTTP Controllers
- Request routing
- Response formatting
- Middleware configuration

## Creating a New Feature: Step-by-Step

### Step 1: Define the Entity (Core Layer)

Create your domain entity in `src/Clean.Architecture.Template.Core/Entity/`:

```csharp
using Clean.Architecture.Template.Core.Entity.Common;

public class ProductEntity : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}
```

### Step 2: Create Repository Interface (Core Layer)

Add interface to `src/Clean.Architecture.Template.Core/Repository/`:

```csharp
using Clean.Architecture.Template.Core.Entity;
using Clean.Architecture.Template.Core.Specs;

public interface IProductRepository
{
    Task<ProductEntity> GetProductByIdAsync(int id);
    Task<DataList<ProductEntity>> GetProductsAsync(ListParams listParams);
    Task<int> CreateProductAsync(ProductEntity product);
    Task<bool> UpdateProductAsync(ProductEntity product);
    Task<bool> DeleteProductAsync(int id);
}
```

### Step 3: Create Commands/Queries (Application Layer)

Commands in `src/Clean.Architecture.Template.Application/Commands/`:

```csharp
using MediatR;
using Clean.Architecture.Template.Application.Response.General;

public class CreateProductCommand : IRequest<CreateResponse>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}

public class UpdateProductCommand : IRequest<UpdateResponse>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}

public class DeleteProductCommand : IRequest<DeleteResponse>
{
    public int Id { get; set; }
}
```

Queries in `src/Clean.Architecture.Template.Application/Queries/`:

```csharp
using MediatR;
using Clean.Architecture.Template.Application.Response;

public class GetProductQuery : IRequest<ProductResponse>
{
    public int Id { get; set; }
}

public class ListProductsQuery : ListQuery<ProductResponse>
{
}
```

### Step 4: Create Response DTOs (Application Layer)

Add to `src/Clean.Architecture.Template.Application/Response/`:

```csharp
public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public DateTime CreatedDate { get; set; }
}
```

### Step 5: Create Handlers (Application Layer)

Add to `src/Clean.Architecture.Template.Application/Handlers/Products/`:

```csharp
using MediatR;
using Clean.Architecture.Template.Application.Commands;
using Clean.Architecture.Template.Application.Response.General;
using Clean.Architecture.Template.Core.Repository;
using Clean.Architecture.Template.Core.Entity;
using Clean.Architecture.Template.Application.Mapper;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateResponse>
{
    private readonly IProductRepository _repository;

    public CreateProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new ProductEntity
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            CreatedDate = DateTime.UtcNow
        };

        var id = await _repository.CreateProductAsync(product);

        return new CreateResponse 
        { 
            Id = id, 
            IsSuccessful = true,
            Message = "Product created successfully"
        };
    }
}

public class GetProductHandler : IRequestHandler<GetProductQuery, ProductResponse>
{
    private readonly IProductRepository _repository;

    public GetProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetProductByIdAsync(request.Id);
        return LazyMapper.Mapper.Map<ProductResponse>(product);
    }
}

public class ListProductsHandler : IRequestHandler<ListProductsQuery, DataList<ProductResponse>>
{
    private readonly IProductRepository _repository;

    public ListProductsHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<DataList<ProductResponse>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _repository.GetProductsAsync(request.ListParams);
        return LazyMapper.Mapper.Map<DataList<ProductResponse>>(products);
    }
}
```

### Step 6: Implement Repository (Infrastructure Layer)

Add to `src/Clean.Architecture.Template.Infrastructure/Repository/`:

```csharp
using Dapper;
using Clean.Architecture.Template.Core.Entity;
using Clean.Architecture.Template.Core.Repository;
using Clean.Architecture.Template.Core.Specs;

public class ProductRepository : DBRepository, IProductRepository
{
    public ProductRepository(IDbConnection connection) : base(connection) { }

    public async Task<ProductEntity> GetProductByIdAsync(int id)
    {
        var query = "SELECT * FROM products WHERE id = @id AND is_active = true";
        return await _connection.QueryFirstOrDefaultAsync<ProductEntity>(query, new { id });
    }

    public async Task<DataList<ProductEntity>> GetProductsAsync(ListParams listParams)
    {
        var query = "SELECT * FROM products WHERE is_active = true ORDER BY created_date DESC";
        var items = await _connection.QueryAsync<ProductEntity>(query);
        
        return new DataList<ProductEntity>
        {
            Data = items.ToList(),
            TotalRecords = items.Count()
        };
    }

    public async Task<int> CreateProductAsync(ProductEntity product)
    {
        var query = @"
            INSERT INTO products (name, description, price, stock_quantity, created_date, created_by)
            VALUES (@name, @description, @price, @stockQuantity, @createdDate, @createdBy)
            RETURNING id";
        
        return await _connection.ExecuteScalarAsync<int>(query, product);
    }

    public async Task<bool> UpdateProductAsync(ProductEntity product)
    {
        var query = @"
            UPDATE products 
            SET name = @name, description = @description, price = @price, 
                stock_quantity = @stockQuantity, updated_date = @updatedDate
            WHERE id = @id";
        
        var result = await _connection.ExecuteAsync(query, product);
        return result > 0;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var query = "UPDATE products SET is_active = false WHERE id = @id";
        var result = await _connection.ExecuteAsync(query, new { id });
        return result > 0;
    }
}
```

### Step 7: Create Controller (API Layer)

Add to `src/Clean.Architecture.Template.API/Controllers/`:

```csharp
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Clean.Architecture.Template.Application.Commands;
using Clean.Architecture.Template.Application.Queries;
using Clean.Architecture.Template.Application.Response;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponse>> GetProduct(int id)
    {
        var result = await _mediator.Send(new GetProductQuery { Id = id });
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<DataList<ProductResponse>>> ListProducts([FromQuery] int pageNumber = 1)
    {
        var result = await _mediator.Send(new ListProductsQuery 
        { 
            ListParams = new ListParams { PageNumber = pageNumber } 
        });
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CreateResponse>> CreateProduct([FromBody] CreateProductCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.IsSuccessful)
            return BadRequest(result);
        return CreatedAtAction(nameof(GetProduct), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        if (!result.IsSuccessful)
            return BadRequest(result);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result = await _mediator.Send(new DeleteProductCommand { Id = id });
        if (!result.IsSuccessful)
            return BadRequest(result);
        return NoContent();
    }
}
```

### Step 8: Register in Program.cs

Add dependency injection in the API's `Program.cs`:

```csharp
// Add repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
```

### Step 9: Create AutoMapper Profile (Application Layer)

Add to `src/Clean.Architecture.Template.Application/Mapper/`:

```csharp
using AutoMapper;
using Clean.Architecture.Template.Core.Entity;
using Clean.Architecture.Template.Application.Response;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<ProductEntity, ProductResponse>();
        CreateMap<CreateProductCommand, ProductEntity>();
        CreateMap<UpdateProductCommand, ProductEntity>();
    }
}
```

## CQRS Pattern Explanation

### Commands
Commands represent **actions** that modify state:
- Always return a response
- Implement `IRequest<TResponse>`
- Examples: Create, Update, Delete

### Queries
Queries represent **questions** that retrieve data:
- Read-only operations
- Implement `IRequest<TResponse>`
- Examples: Get, List, Search

## Error Handling

Use the Result pattern for consistent error handling:

```csharp
public class Result
{
    public bool IsSuccessful { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; }
}
```

In handlers:

```csharp
if (!isValid)
{
    return new CreateResponse
    {
        IsSuccessful = false,
        Message = "Validation failed",
        Errors = validationErrors
    };
}
```

In controllers:

```csharp
var result = await _mediator.Send(command);
if (!result.IsSuccessful)
    return BadRequest(result);
```

## Testing

Write unit tests for handlers:

```csharp
[Fact]
public async Task CreateProductHandler_WithValidInput_ReturnsSuccessfulResult()
{
    // Arrange
    var mockRepository = new Mock<IProductRepository>();
    var handler = new CreateProductHandler(mockRepository.Object);
    var command = new CreateProductCommand { Name = "Test Product", Price = 99.99m };

    // Act
    var result = await handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.True(result.IsSuccessful);
    mockRepository.Verify(x => x.CreateProductAsync(It.IsAny<ProductEntity>()), Times.Once);
}
```

## Best Practices Summary

1. ✅ Keep entities simple and focused on domain logic
2. ✅ Use repositories for all data access
3. ✅ Separate commands and queries
4. ✅ Use MediatR for CQRS implementation
5. ✅ Map entities to DTOs in handlers
6. ✅ Validate input in handlers
7. ✅ Return appropriate HTTP status codes
8. ✅ Document API endpoints with Swagger
9. ✅ Write unit tests for critical business logic
10. ✅ Keep layers independent and loosely coupled

For more information, see the main [README.md](../README.md) file.
