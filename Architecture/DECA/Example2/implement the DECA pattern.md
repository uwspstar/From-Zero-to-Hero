# Implement the DECA pattern

- Here's a step-by-step guide to implement the DECA pattern with .NET Core WebAPI, EF Core, and Blazor using **VS Code**:

---

## steps

### **Step 1: Set Up Projects**

#### 1. Open VS Code and create a workspace folder

#### 2. Open Terminal (`Ctrl+``) and run

```bash
# Create solution
dotnet new sln -n DecaDemo

# Create WebAPI project
dotnet new webapi -n DecaApi
dotnet sln add DecaApi

# Create Blazor WASM project
dotnet new blazorwasm -n DecaBlazor
dotnet sln add DecaBlazor

# Navigate to API project and add packages
cd DecaApi
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.AspNetCore.SignalR.Client
```

---

### **Step 2: Define Entity & DbContext**

#### 1. In `DecaApi/Models/Person.cs`

```csharp
public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public DateTime LastUpdated { get; set; }
}
```

#### 2. In `DecaApi/Data/AppDbContext.cs`

```csharp
public class AppDbContext : DbContext
{
    public DbSet<Person> People { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
```

---

### **Step 3: Implement DECA Interceptor**

#### 1. Create `DecaApi/Interceptors/PersonChangeInterceptor.cs`

```csharp
public class PersonChangeInterceptor : SaveChangesInterceptor
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public PersonChangeInterceptor(IHubContext<NotificationHub> hubContext)
        => _hubContext = hubContext;

    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        var modifiedPeople = eventData.Context.ChangeTracker.Entries<Person>()
            .Where(e => e.State == EntityState.Modified)
            .ToList();

        foreach (var entry in modifiedPeople)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveWarning", 
                $"Person {entry.Entity.Id} updated!");
        }

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}
```

---

### **Step 4: Configure SignalR Hub**

#### 1. Create `DecaApi/Hubs/NotificationHub.cs`

```csharp
public class NotificationHub : Hub { }
```

#### 2. In `DecaApi/Program.cs`

```csharp
// Add SignalR
builder.Services.AddSignalR();

// Add DbContext with Interceptor
builder.Services.AddDbContext<AppDbContext>(options => 
{
    options.UseInMemoryDatabase("PeopleDB");
    options.AddInterceptors(
        new PersonChangeInterceptor(
            builder.Services.BuildServiceProvider()
                .GetRequiredService<IHubContext<NotificationHub>>()));
});

// Add CORS for Blazor
builder.Services.AddCors(options => 
    options.AddPolicy("BlazorCors", policy => 
        policy.WithOrigins("https://localhost:7121")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()));

// Map Hub
app.MapHub<NotificationHub>("/notificationHub");
```

---

### **Step 5: Create API Controller**

#### In `DecaApi/Controllers/PeopleController.cs`

```csharp
[ApiController]
[Route("[controller]")]
public class PeopleController : ControllerBase
{
    private readonly AppDbContext _context;

    public PeopleController(AppDbContext context) => _context = context;

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Person person)
    {
        var existing = await _context.People.FindAsync(id);
        if (existing == null) return NotFound();

        existing.Name = person.Name;
        existing.Age = person.Age;
        await _context.SaveChangesAsync(); // Triggers interceptor
        return Ok(existing);
    }
}
```

---

### **Step 6: Set Up Blazor Frontend**

#### 1. In `DecaBlazor/Pages/Index.razor`

```razor
@page "/"
@inject NotificationService NotificationService

<h1>DECA Demo</h1>

@if (showAlert)
{
    <div class="alert alert-warning">
        @alertMessage
    </div>
}

@code {
    private bool showAlert;
    private string alertMessage = "";

    protected override async Task OnInitializedAsync()
    {
        await NotificationService.StartAsync();
        NotificationService.OnWarningReceived += (msg) => 
        {
            alertMessage = msg;
            showAlert = true;
            StateHasChanged();
        };
    }
}
```

#### 2. Create `DecaBlazor/Services/NotificationService.cs`

```csharp
public class NotificationService
{
    private HubConnection? _hubConnection;
    public event Action<string>? OnWarningReceived;

    public async Task StartAsync()
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7293/notificationHub")
            .Build();

        _hubConnection.On<string>("ReceiveWarning", msg => 
            OnWarningReceived?.Invoke(msg));

        await _hubConnection.StartAsync();
    }
}
```

#### 3. Register service in `DecaBlazor/Program.cs`

```csharp
builder.Services.AddScoped<NotificationService>();
```

---

### **Step 7: Run the Application**

1. **Start API**

   ```bash
   cd DecaApi
   dotnet run
   ```

2. **Start Blazor**:

   ```bash
   cd ../DecaBlazor
   dotnet run
   ```

3. **Test**:
   - Open Blazor app at `https://localhost:7121`
   - Use Postman to send a PUT request to `https://localhost:7293/people/1`

     ```json
     {
       "name": "Updated Name",
       "age": 30
     }
     ```

   - Observe the warning alert in Blazor!

---

### **Key Files Structure**

```text
DecaDemo/
├── DecaApi/
│   ├── Controllers/
│   │   └── PeopleController.cs
│   ├── Hubs/
│   │   └── NotificationHub.cs
│   ├── Interceptors/
│   │   └── PersonChangeInterceptor.cs
│   ├── Models/
│   │   └── Person.cs
│   └── Program.cs
└── DecaBlazor/
    ├── Pages/
    │   └── Index.razor
    ├── Services/
    │   └── NotificationService.cs
    └── Program.cs
```

This implementation shows how to:

1. Capture entity changes automatically via EF Core
2. Trigger real-time frontend updates without manual property setters
3. Maintain clean separation of concerns
