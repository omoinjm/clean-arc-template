# Creating Custom .NET Web API Project Templates

## Overview

This documentation explains **how to create custom project templates in .NET**, specifically for **ASP.NET Web APIs**, based on the topic discussed in the provided transcript.

The motivation behind this approach is the change introduced in **.NET 6**, where Microsoft replaced the traditional `Startup.cs`-based Web API template with a **minimal hosting model**. While the minimal approach is simpler, many teams prefer the **explicit structure** used in .NET 5 and earlier versions.

By the end of this guide, you will understand:

- Why custom templates are useful
- How the .NET templating system works
- How to convert an existing project into a reusable template
- How to install, test, and share the template

---

## Background: .NET 5 vs .NET 6 Web API Templates

### .NET 5 Web API Structure

In .NET 5, Web API projects typically included:

- `Program.cs` – Entry point that builds and runs the host
- `Startup.cs` –
  - `ConfigureServices()` for dependency injection
  - `Configure()` for middleware pipeline

- Controllers folder with API controllers

This separation of concerns was widely adopted and familiar to many teams.

### .NET 6 Minimal Hosting Model

.NET 6 introduced:

- A **single `Program.cs` file**
- No `Startup.cs`
- Services and middleware configured inline
- Implicit `using` statements
- File‑scoped namespaces

Example:

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

app.Run();
```

While concise, this model can become cluttered in larger projects and reduces structural consistency across teams.

---

## Why Create Custom Project Templates?

Custom templates allow teams to:

- Enforce architectural standards
- Maintain consistency across projects
- Reduce setup time for new services
- Restore preferred structures (e.g., `Startup.cs`)
- Share internal best practices

Most professional teams use **custom templates layered on top of Microsoft defaults**.

---

## Understanding the .NET Templating System

.NET uses the `dotnet new` command to scaffold projects from templates.

### Listing Installed Templates

```bash
dotnet new --list
```

This displays all available templates on your machine (console apps, web apps, class libraries, etc.).

### Creating a Project from a Template

```bash
dotnet new webapi -n MyApi
```

Your goal is to create **your own template** that appears in this list.

---

## Step 1: Create the Base Project

1. Create a **Web API project** targeting **.NET 6**
2. Manually restore the classic structure:
   - Add `Startup.cs`
   - Move service registration to `ConfigureServices`
   - Move middleware setup to `Configure`

3. Keep modern features if desired:
   - Implicit `using` statements
   - File‑scoped namespaces

This project becomes the **source** for your template.

---

## Step 2: Add the Template Configuration Folder

At the **root of your project**, create the following folder:

```
.template.config/
```

Inside it, create a file:

```
template.json
```

This file defines how the template behaves.

---

## Step 3: Define `template.json`

A basic example:

```json
{
  "$schema": "http://json.schemastore.org/template",
  "author": "Your Name or Company",
  "classifications": ["Web", "API", "ASP.NET"],
  "identity": "Company.WebApi.Template",
  "name": "Company Web API Template",
  "shortName": "company-webapi",
  "tags": {
    "language": "C#",
    "type": "project"
  },
  "sourceName": "TemplateProjectName"
}
```

### Key Fields Explained

- **identity** – Unique identifier (must be globally unique)
- **name** – Friendly name shown in `dotnet new --list`
- **shortName** – CLI command name
- **classifications** – Categories for discovery
- **sourceName** – Placeholder project name to be replaced. For example, if your base project's namespace is `TemplateProjectName`, when a user creates a new project with `-n MyAwesomeApi`, all occurrences of `TemplateProjectName` (like in namespaces and folder names) will be replaced by `MyAwesomeApi`.

---

## Step 4: Prepare the Project for Templating

### Replace Hardcoded Names

If your project is named:

```
TemplateProjectName
```

.NET will replace it automatically when users run:

```bash
dotnet new company-webapi -n MyAwesomeApi
```

Ensure:

- Namespace matches the project name
- Folder names do not hardcode values unless intended

---

## Step 5: Install the Template Locally

From the **project root directory**:

```bash
dotnet new install .
```

You should now see your template listed:

```bash
dotnet new --list
```

---

## Step 6: Test the Template

Create a new project using your template:

```bash
dotnet new company-webapi -n TestApi
```

Verify:

- Project builds successfully (`dotnet build TestApi`).
- `Startup.cs` is present.
- Services and middleware are structured as expected.
- Run any included unit/integration tests (`dotnet test TestApi`).
- For Web API templates, run the application (`dotnet run --project TestApi.API`) and verify that API endpoints are accessible and return expected responses (e.g., using a tool like Postman or `curl`).

---

## Step 7: Uninstall or Update the Template

### Uninstall

```bash
dotnet new uninstall .
```

### Reinstall After Changes

```bash
dotnet new uninstall .
dotnet new install .
```

---

## Sharing the Template

You can distribute templates by:

- Publishing to a private Git repository
- Packaging as a NuGet package
- Sharing the folder directly within a company

### Install from a Repository

```bash
dotnet new install <path-or-repo-url>
```

### Packaging as a NuGet Package

For more formal distribution, especially within an organization, packaging your template as a NuGet package (`.nupkg`) is recommended.

1.  **Create a `.csproj` file for your template package:** In the root directory of your template, create a new `.csproj` file (e.g., `MyCompany.Templates.csproj`) with content similar to this:

    ```xml
    <Project Sdk="Microsoft.NET.Sdk">
      <PropertyGroup>
        <PackageType>Template</PackageType>
        <PackageVersion>1.0.0</PackageVersion>
        <PackageId>MyCompany.WebApi.Template</PackageId>
        <Title>My Company Web API Template</Title>
        <Authors>Your Company</Authors>
        <Description>Template for creating ASP.NET Core Web API projects with company standards.</Description>
        <PackageTags>dotnet-new;template;webapi;company</PackageTags>
        <TargetFramework>netstandard2.0</TargetFramework>
        <IncludeContentInPack>true</IncludeContentInPack>
        <IncludeBuildOutput>false</IncludeBuildOutput>
        <ContentTargetFolders>content</ContentTargetFolders>
        <NoWarn>$(NoWarn);NU5128</NoWarn>
      </PropertyGroup>

      <ItemGroup>
        <Content Include="**\*" Exclude=".template.config\**;**\bin\**;**\obj\**;**\.vs\**;**\.vscode\**" />
        <Compile Remove="**\*" />
      </ItemGroup>
    </Project>
    ```

    *   **Important:** Adjust `PackageId`, `Title`, `Authors`, and `Description` to match your template.
    *   The `<Content Include="**\*"... />` line ensures all necessary files from your template project are included in the NuGet package, while excluding temporary build artifacts and template configuration itself.

2.  **Pack the template:** From the directory containing your template's `.csproj` file, run:

    ```bash
    dotnet pack
    ```
    This will generate a `.nupkg` file (e.g., `MyCompany.WebApi.Template.1.0.0.nupkg`) in the `bin/Debug` (or `bin/Release`) folder.

3.  **Distribute the NuGet package:**
    *   **Publish to a NuGet feed:** Push the `.nupkg` file to a private NuGet feed (e.g., Azure Artifacts, MyGet, a local network share) using `dotnet nuget push`.
        ```bash
        dotnet nuget push MyCompany.WebApi.Template.1.0.0.nupkg --source "Your NuGet Feed URL"
        ```
    *   **Share directly:** You can also share the `.nupkg` file directly, and users can install it by specifying the local path to the file.
        ```bash
        dotnet new install <path-to-your-template>.nupkg
        ```

---

## Best Practices

- Keep templates **minimal but opinionated**
- Avoid over‑engineering
- Document how to use the template
- Version templates carefully
- Align with team conventions

---

## Conclusion

Custom .NET templates give you the best of both worlds:

- Modern .NET features
- Familiar, maintainable project structure

They are an essential tool for teams that value **consistency, clarity, and productivity** in real‑world applications.

---

## Next Steps

- Add optional parameters (auth, logging, health checks)
- Create multiple templates (API, Worker, Library)
- Automate publishing via CI/CD

If you want, I can:

- Add **advanced template parameters**
- Convert this into a **company internal standard doc**
- Generate a **sample GitHub repo structure**

Just tell me how you'd like to proceed.
