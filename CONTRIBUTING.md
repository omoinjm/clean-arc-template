# Contributing to Clean Architecture Template

Thank you for your interest in contributing! This document provides guidelines for contributing to the project.

## Code of Conduct

Be respectful and professional in all interactions.

## Getting Started

1. Fork the repository
2. Clone your fork: `git clone https://github.com/yourusername/clean-arc-template.git`
3. Create a feature branch: `git checkout -b feature/your-feature-name`
4. Make your changes
5. Commit: `git commit -am 'Add feature description'`
6. Push to the branch: `git push origin feature/your-feature-name`
7. Submit a Pull Request

## Development Setup

1. Ensure you have [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) installed
2. Restore dependencies: `dotnet restore`
3. Build the solution: `dotnet build`
4. Run tests: `dotnet test`

## Before Submitting

- Ensure all tests pass: `dotnet test`
- Build the solution: `dotnet build`
- Follow the existing code style and conventions
- Add tests for new features
- Update documentation as needed

## Coding Standards

- Use meaningful variable and method names
- Keep methods small and focused
- Add XML documentation comments to public APIs
- Follow the Single Responsibility Principle
- Use dependency injection for loose coupling

## Clean Architecture Principles

When making changes, ensure they align with Clean Architecture:

1. **Layers are independent** - No layer should have hard dependencies on implementation details of layers below
2. **Dependencies flow inward** - Dependencies should always point toward the Core
3. **Entities are framework-agnostic** - Domain entities should not depend on any framework
4. **Separation of concerns** - Keep business logic separate from infrastructure concerns

## Reporting Issues

When reporting issues, please include:

- A clear description of the issue
- Steps to reproduce
- Expected behavior
- Actual behavior
- Your environment (OS, .NET version)

## Questions?

Feel free to open an issue or reach out via the repository discussions.

Thank you for contributing!
