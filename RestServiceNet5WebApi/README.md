# RestServiceNet5WebApi
.net 5 web api for rest service example for BootCamp 04 Fullstack (Backend) https://github.com/abelopezpaniagua/RestServiceNet5WebApi

# Team members 
- Abel Lopez 
- Alan Alvarez 
- Samuel Loza

## Key Features

With .Net 5, developing RESTful API it just got a whole lot easier and better by allowing you to focus more on your bussiness logics and creating an Endpoint with few lines of code

- OpenAPI Specification on by default.
- Azure API Management Import
- Better launch experience for web API projects
- Performance improvements like HTTP/2 and Docker Containers
- Authentication and authorization:
  - Azure Active Directory authentication with Microsoft.Identity.Web
  - Allow anonymous access to an endpoint
  - Custom handling of authorization failures
  - Authorization when using endpoint routing
- API improvements
  - JSON extension methods for HttpRequest and HttpResponse
  - System.Diagnostics.Activity
  - FromBodyAttribute
- Miscellaneous improvements
  - Control Startup class activation
  - Auto refresh with dotnet watch 
  - Console Logger Formatter
  - JSON Console Logger

Source: https://docs.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-5.0?view=aspnetcore-5.0

## Dependency

We need execute the next command for add dependency, for this example we add the dependency of Entity Framework Core
For our Project:
```
dotnet add package Microsoft.EntityFrameworkCore
```
By default, but optional:
```
dotnet add package Swashbuckle.AspNetCore
```

## Advantages/Disadvantages

## Advantages:

* Open source and free
* Better performance than previous ASP NET versions
* Compatibility different OS in development (Linux, Mac, Windows)
* To being consistent and unified within ASP.NET Core, APIs built in .NET Core are much easier to test than those built on previous versions
* The built-in support for hosting ASP.NET Core apps, in a test host that can create an HttpClient that makes in-memory requests to the app, is a huge benefit when it comes to automated testing.
* A Upgrade Assistant can be used to migrate older projects .Net 5.0
* Entity Framework Core is better than the Entity framework used in .NET Framework with better mappings (such as AutoMapper),migrations and query performance.
* The features such as reflections are supported by .NET Core.
* The .NET is far lighter, faster and more modular than the .NET Framework.
* Is not necessary install Visual Studio to work with ASP NET Core 5 Web API

## Disadvantages:

* .NET Core is not fully matured and is still evolving.
* Is not possible to use Xamarin with .NET Core at this point.
* The Upgrade Assistant not always work
* Even though .NET Core supports VB and F# but that is not supported for all the projects types
* Missing 3rd party library support.
* Can't use Windows-specific APIs in ASP.NET Core and .NET Core like System.Drawing namespace or work with Windows Registry
