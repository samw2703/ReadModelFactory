# ReadModelFactory

Provides a simple reflection based service setup for queries within a CQRS architecture.

## How to use

Simply define your read model and a corresponding `ReadModelProvider`...

```csharp
public class UsersReadModel
{
    public List<object> Users { get; set; }
}

internal class UsersReadModelProvider : ReadModelProvider<UsersReadModel>
{
    public override Task<UsersReadModel> Get()
    {
        //Implement logic
    }
}
```

Optionally you can also define a `ReadModelProvider` that accepts arguments...

```csharp
public class UserReadModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class UserReadModelArgs
{
    public int Id { get; }

    public UserReadModelArgs(int id)
    {
        Id = id;
    }
}

internal class UserReadModelProvider : ReadModelProvider<UserReadModel, UserReadModelArgs>
{
    public override Task<UserReadModel> Get(UserReadModelArgs args)
    {
        //Implement logic
    }
}
```

Then use the provided `IServiceCollection` extension method, passing in the assemblies containing your read models...

```csharp
var sc = new ServiceCollection();
sc.AddReadModelFactory(typeof(UserReadModel).Assembly, typeof(UsersReadModel).Assembly);
```

You can then use the service provider to get your `IReadModelFactory` that can be used to get your read models...

```csharp
var sp = sc.BuildServiceProvider();
var readModelFactory = sp.GetRequiredService<IReadModelFactory>();

var usersReadModel = readModelFactory.Get<UsersReadModel>();
var userReadModel = readModelFactory.Get<UserReadModel, UserReadModelArgs>( new UserReadModelArgs(1) );
```