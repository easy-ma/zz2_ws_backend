# AutoMapper

- https://docs.automapper.org/en/latest/Configuration.html#profile-instances

- nugget pacakge:
  `dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 8.1.1`

## Startup.cs

```cs
public void ConfigureServices(IServiceCollection services)
{
    //...
    services.AddAutoMapper(typeof(Startup));
    //...
}
```

## MapperProfiles

```cs
using AutoMapper;
using MonDto;
using MonModel;

namespace mynamespace
{
    public class MonMapperProfile : Profile
    {
        public MonMapperProfile()
        {
            CreateMap<MonDto, MonModel>().ReverseMap();
            // ReverseMap: https://docs.automapper.org/en/latest/Reverse-Mapping-and-Unflattening.html
        }
    }
}
```

**ReverseMap** : Bidirectional Mapping

- Dto --> Model
- Model --> Dto => reverseMap

## Use

```cs
public class MyClass
{

    private readonly IMapper _mapper;
    
    public MyClass(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<> MyMethod(MyDto myDto)
        {

            // Dto --> Model
            MyModel model = _mapper.Map<MyModel>(myDto);

            // reverse Model -> Dto
            MyDto dto = _mapper.Map<MyDto>(model);
        }
```
