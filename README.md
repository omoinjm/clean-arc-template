# Clean Architecture Solution Template

The goal of this template is to provide a straightforward and efficient approach to enterprise application development, leveraging the power of Clean Architecture and ASP.NET Core. Using this template, you can effortlessly create REST API with ASP.NET Core, while adhering to the principles of Clean Architecture. Getting started is easy - simply install the **.NET template** (see below for full details).

If you find this project useful, please give it a star. Thanks! ⭐

## Getting Started

The following prerequisites are required to build and run the solution:

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) (latest version)

The easiest way to get started is to install the [.NET template](https://www.nuget.org/packages/Clean.Architecture.Solution.Template):

```bash
dotnet new install Clean.Architecture.Solution.Template::9.0.8
```

Once installed, create a new solution using the template. You can choose to use Angular, React, or create a Web API-only solution. Specify the client framework using the `-cf` or `--client-framework` option, and provide the output directory where your project will be created. Here are some examples:

To create a REST API with ASP.NET Core:

To create a ASP.NET Core Web API-only solution:

```bash
dotnet new ca-sln -cf None -o YourProjectNames
```
