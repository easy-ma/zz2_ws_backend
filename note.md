# LINK DIAPO CODE FIRST

- https://docs.microsoft.com/fr-fr/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-5.0
- https://docs.microsoft.com/fr-fr/dotnet/core/testing/unit-testing-with-dotnet-testIActionResult
- https://docs.microsoft.com/fr-fr/aspnet/core/mvc/controllers/testing?view=aspnetcore-5.0
- https://xunit.net/#documentation
- https://openclassrooms.com/fr/courses/5641591-testez-votre-application-c/5696246-utilisez-le-framework-de-simulacre-moq

# Struture

```
├── data-access
│   ├── IRepository.cs
│   ├── Migrations/
│   ├── Models/
│   └── Repositories/
│  
└── turradgiver-api
    ├── Controllers/
    ├── Services/
    ├── Program.cs/
    └── Startup.cs
```

## Repository

https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application

Example with user:

1. Create an IUserRepository
1. Create a concrete UserRepository

```
  .---------------------.      .-----------------------.
  |         DAL         |      |    Turradgiver-API    |
  |---------------------|      |-----------------------|
  |  .----------------. |      |   .----------------.  |
  |  | UserRepository |<---------->| AuthRepository |  |
  |  '----------------' |      |   '----------------'  |
  |                     |      |            ^          |
  '---------------------'      |            |          |
        |  ^                   |            v          |
        |  | Postgre cloud     |   .----------------.  |
        |  |                   |   | AuthController |  |
        v  |                   |   '----------------'  |
            .-,( ),-.          '------------^----------'
        .-(          )-.     POST /login    |
       (    internet    )-------------------'
        '-(          ).-'
            '-.( ).-'
```
