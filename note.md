---
author: Bilal Guillaumie
date: 20-02-21
subject: Csharp Notes
---

## Code first links

- https://docs.microsoft.com/fr-fr/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-5.0
- https://docs.microsoft.com/fr-fr/dotnet/core/testing/unit-testing-with-dotnet-testIActionResult
- https://docs.microsoft.com/fr-fr/aspnet/core/mvc/controllers/testing?view=aspnetcore-5.0
- https://xunit.net/#documentation
- https://openclassrooms.com/fr/courses/5641591-testez-votre-application-c/5696246-utilisez-le-framework-de-simulacre-moq

## Docker container

### Build the image from the dockerfile

```docker
dokcer build -t turradgiver -f turradgiver-api/Dockerfile .
```

### Build the container

Add `-p 5001:5001` if in Startup.cs you've used HttpRedirection

```docker
docker run -d -p 5000:5000 -v ./turradgiver-api/:/src/turradgiver-api ./data-access/:/src/data-access turradgiver
```

### Create the image and launch the container from the dockerfile

Add option if you doesn't want logs like `-d` for detach... see docker-compose documentation

```docker
docker-compose up
```

## Project structure

```bash
.
├── data-access
│   ├── Migrations/
│   ├── Models/
│   ├── Repositories/
│   └── data-access.csproj
├── docker-compose.yml
└── turradgiver-api
    ├── Controllers/
    ├── Dtos/
    ├── Services/
    ├── Utils/
    ├── turradgiver-api.csproj
    ├── appsettings.json
    ├── Program.cs
    ├── Startup.cs
    ├── Dockerfile.Net3.1
    └── Dockerfile.Net5
```

This is a schema of how I've structured my code  
See that from the controller I hit my logics inside my service and from my service I do some crud request over the Repository which as you will see in the next section is Generic

```
Client <---> Controller <---> Services <---> Repository <---> Database

  .------------------------.    .-----------------------.
  |         DAL            |    |    Turradgiver-API    |
  |------------------------|    |-----------------------|
  |  .------------------.  |    |   .----------------.  |
  |  | Repository<User> |<--------->|   AuthService  |  |
  |  '------------------'  |    |   '----------------'  |
  |                        |    |            ^          |
  '------------------------'    |            |          |
        |  ^                    |            v          |
        |  | Postgre cloud      |   .----------------.  |
        |  |                    |   | AuthController |  |
        v  |                    |   '----------------'  |
            .-,( ),-.           '------------^----------'
        .-(          )-.     POST /login ->  |
       (    internet    )<-------------------'
        '-(          ).-'  <-- AuthCredential/Unauthorized
            '-.( ).-'
```

### Repositories

Instead of having severals `IRepository` and `Repository` for each model as User, Places, Ratings with RatingRepository and IRatingRepository, UserRepository.
I've decided to add a **Generic** `IRepository`:

```cs
public interface IRepository<T> {
    IQueryable<T> GetAll();
    IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);
    T GetById(int id);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
  }
```

With basic CRUD methods defined wihtin the interface  
And I've implemented the `Repository` which is a concrete implementation of the `IRepository` interface but still generic and over classes that inherit from `baseModel`.  
`BaseModel` contains the basic model attributes which are common to any model over the application and look as follow:

```cs
public class BaseModel
{
    public BaseModel()
    {
        CreatedDate=DateTime.UtcNow;
    }

    [Column("id")]
    public int Id { get; set; }

    [Column("createdDate")]
    public DateTime CreatedDate {get; set;}
}
```

And within the Repository we can use the baseModel in order to do some request over the database over the `Id` props or basic CRUD operation as getAll object from the database...

Part of the Repository class:

```cs
public class Repository<T> : IRepository<T> where T : BaseModel
{
    protected readonly TurradgiverContext _context;
    protected DbSet<T> entities;
    public Repository(TurradgiverContext context) {
      _context = context;
      entities = context.Set<T>();
    }
    public void Create(T entity)
    {
      if (entity == null) {
        throw new ArgumentNullException("[Add]: null entity");
      }
      entities.Add(entity);
      _context.SaveChanges();
    }

    public T GetById(int id)
    {
      return entities.FirstOrDefault(entity => entity.Id == id);
    }

    public IQueryable<T> GetAll()
    {
      return entities.ToList().AsQueryable();
    }

    public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression){
        return entities.Where(expression).AsNoTracking();
    }

}
```

We could add more queries if needed and generic one will give us the possibility to use them withou changing the content of the repository for example `GetByCondition` could be used to retrieve any object from the database with a specific condition.  
For example if we want to retrieve a user with a specific email from the db with the user model we can do as follow:

```cs
User user =_userRepository.GetByCondition((u=> u.Email.CompareTo(email)== 0)).FirstOrDefault();
```

#### USe

In order to use the generic repository you have to:

1. Create a **Model** which inherit from **BaseModel**
1. In the Service you want to use the repository you just have to add the following statements :

```cs
 public class YourService : IYourService
{
    private readonly IRepository<YourModel> _yourModelRepository;
    public AuthService(IRepository<YourModel> yourModelRepository)
    {
        _yourModelRepository = yourModelRepository;
    }
// Thanks to dependancy injection the Repository will be used.
```

No need to update the `startup.cs` in order to add a new Scope it will be handle automatically thanks to the scope already added for the generic Repository.
