# Complete POC Project: Domain Events with EF Core and Blazor WASM

Here's a step-by-step guide to create a complete Proof of Concept (POC) project from scratch using VS Code. This will implement the domain events pattern with EF Core's `SaveChangesInterceptor` and base response classes.

## Step 1: Setup the Solution Structure

1. Open VS Code
2. Open the terminal (Ctrl+`)

```bash
# Create solution directory
mkdir DomainEventsPOC
cd DomainEventsPOC

# Create solution file
dotnet new sln -n DomainEventsPOC

# Create projects
dotnet new webapi -n DomainEventsPOC.Server
dotnet new blazorwasm -n DomainEventsPOC.Client
dotnet new classlib -n DomainEventsPOC.Core

# Add projects to solution
dotnet sln add DomainEventsPOC.Server
dotnet sln add DomainEventsPOC.Client
dotnet sln add DomainEventsPOC.Core

# Add references
dotnet add DomainEventsPOC.Server reference DomainEventsPOC.Core
dotnet add DomainEventsPOC.Client reference DomainEventsPOC.Core
```

## Step 2: Implement Core Domain Logic

1. Create these files in `DomainEventsPOC.Core`:

**Domain/Entity.cs**
```csharp
namespace DomainEventsPOC.Core.Domain;

public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();
   
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
   
    protected void AddDomainEvent(IDomainEvent eventItem) => _domainEvents.Add(eventItem);
   
    public void ClearDomainEvents() => _domainEvents.Clear();
}
```

**Domain/IDomainEvent.cs**
```csharp
namespace DomainEventsPOC.Core.Domain;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
    string EventType { get; }
}

public abstract class DomainEvent : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
    public abstract string EventType { get; }
}
```

**Domain/NameChangedEvent.cs**
```csharp
namespace DomainEventsPOC.Core.Domain;

public class NameChangedEvent : DomainEvent
{
    public Guid EntityId { get; }
    public string OldName { get; }
    public string NewName { get; }

    public NameChangedEvent(Guid entityId, string oldName, string newName)
    {
        EntityId = entityId;
        OldName = oldName;
        NewName = newName;
    }

    public override string EventType => "NameChanged";
}
```

**Models/ApiResponse.cs**
```csharp
namespace DomainEventsPOC.Core.Models;

public class ApiResponse<T>
{
    public bool Success { get; set; } = true;
    public string? Message { get; set; }
    public T? Data { get; set; }
    public List<IDomainEvent> Events { get; set; } = new();

    public static ApiResponse<T> Ok(T data, List<IDomainEvent> events)
    {
        return new ApiResponse<T>
        {
            Data = data,
            Events = events
        };
    }

    public static ApiResponse<T> Error(string message)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message
        };
    }
}
```

## Step 3: Implement Server-side Components

1. In `DomainEventsPOC.Server`, add these files:

**Services/EventCollector.cs**
```csharp
using DomainEventsPOC.Core.Domain;

namespace DomainEventsPOC.Server.Services;

public interface IEventCollector
{
    void AddEvent(IDomainEvent domainEvent);
    IReadOnlyList<IDomainEvent> GetEvents();
    void ClearEvents();
}

public class EventCollector : IEventCollector
{
    private readonly List<IDomainEvent> _events = new();
    private readonly object _lock = new();

    public void AddEvent(IDomainEvent domainEvent)
    {
        lock (_lock)
        {
            _events.Add(domainEvent);
        }
    }

    public IReadOnlyList<IDomainEvent> GetEvents()
    {
        lock (_lock)
        {
            return _events.ToList().AsReadOnly();
        }
    }

    public void ClearEvents()
    {
        lock (_lock)
        {
            _events.Clear();
        }
    }
}
```

**Interceptors/DomainEventInterceptor.cs**
```csharp
using DomainEventsPOC.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DomainEventsPOC.Server.Interceptors;

public class DomainEventInterceptor : SaveChangesInterceptor
{
    private readonly IEventCollector _eventCollector;

    public DomainEventInterceptor(IEventCollector eventCollector)
    {
        _eventCollector = eventCollector;
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        CollectDomainEvents(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        CollectDomainEvents(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void CollectDomainEvents(DbContext? context)
    {
        if (context == null) return;

        var entities = context.ChangeTracker
            .Entries<Entity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

        foreach (var entity in entities)
        {
            var events = entity.DomainEvents.ToList();
            foreach (var domainEvent in events)
            {
                _eventCollector.AddEvent(domainEvent);
            }
            entity.ClearDomainEvents();
        }
    }
}
```

**Models/Person.cs**
```csharp
using DomainEventsPOC.Core.Domain;

namespace DomainEventsPOC.Server.Models;

public class Person : Entity
{
    private string _name = string.Empty;
   
    public Guid Id { get; private set; } = Guid.NewGuid();
   
    public string Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                AddDomainEvent(new NameChangedEvent(Id, _name, value));
                _name = value;
            }
        }
    }
}
```

**Data/AppDbContext.cs**
```csharp
using DomainEventsPOC.Core.Domain;
using DomainEventsPOC.Server.Models;
using Microsoft.EntityFrameworkCore;
using DomainEventsPOC.Server.Interceptors;

namespace DomainEventsPOC.Server.Data;

public class AppDbContext : DbContext
{
    private readonly DomainEventInterceptor _interceptor;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        DomainEventInterceptor interceptor) : base(options)
    {
        _interceptor = interceptor;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_interceptor);
    }

    public DbSet<Person> People => Set<Person>();
}
```

## Step 4: Configure the Server

1. In `DomainEventsPOC.Server/Program.cs`:

```csharp
using DomainEventsPOC.Server.Data;
using DomainEventsPOC.Server.Interceptors;
using DomainEventsPOC.Server.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IEventCollector, EventCollector>();
builder.Services.AddScoped<DomainEventInterceptor>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("DomainEventsPOC");
    options.AddInterceptors(
        builder.Services.BuildServiceProvider().GetRequiredService<DomainEventInterceptor>());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Seed initial data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.People.Add(new() { Name = "Initial Person" });
    db.SaveChanges();
}

app.Run();
```

## Step 5: Implement API Controllers

**Controllers/PeopleController.cs**
```csharp
using DomainEventsPOC.Core.Domain;
using DomainEventsPOC.Core.Models;
using DomainEventsPOC.Server.Data;
using DomainEventsPOC.Server.Models;
using DomainEventsPOC.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DomainEventsPOC.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IEventCollector _eventCollector;

    public PeopleController(AppDbContext context, IEventCollector eventCollector)
    {
        _context = context;
        _eventCollector = eventCollector;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<PersonDto>>>> GetPeople()
    {
        var people = await _context.People.ToListAsync();
        return ApiResponse<List<PersonDto>>.Ok(
            people.Select(p => new PersonDto(p)).ToList(),
            _eventCollector.GetEvents().ToList()
        );
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<PersonDto>>> GetPerson(Guid id)
    {
        var person = await _context.People.FindAsync(id);
        if (person == null) return NotFound(ApiResponse<PersonDto>.Error("Not found"));

        return ApiResponse<PersonDto>.Ok(
            new PersonDto(person),
            _eventCollector.GetEvents().ToList()
        );
    }

    [HttpPut("{id}/name")]
    public async Task<ActionResult<ApiResponse<PersonDto>>> UpdateName(
        Guid id,
        [FromBody] UpdateNameRequest request)
    {
        var person = await _context.People.FindAsync(id);
        if (person == null) return NotFound(ApiResponse<PersonDto>.Error("Not found"));

        person.Name = request.NewName;
        await _context.SaveChangesAsync();

        return ApiResponse<PersonDto>.Ok(
            new PersonDto(person),
            _eventCollector.GetEvents().ToList()
        );
    }
}

public record PersonDto(Person Person)
{
    public Guid Id => Person.Id;
    public string Name => Person.Name;
}

public record UpdateNameRequest(string NewName);
```

## Step 6: Implement Blazor WASM Client

1. In `DomainEventsPOC.Client`, add these files:

**Services/ApiClient.cs**
```csharp
using DomainEventsPOC.Core.Domain;
using DomainEventsPOC.Core.Models;
using System.Net.Http.Json;

namespace DomainEventsPOC.Client.Services;

public class ApiClient
{
    private readonly HttpClient _httpClient;
    private readonly NotificationService _notificationService;

    public ApiClient(HttpClient httpClient, NotificationService notificationService)
    {
        _httpClient = httpClient;
        _notificationService = notificationService;
    }

    public async Task<ApiResponse<T>> GetAsync<T>(string endpoint)
    {
        var response = await _httpClient.GetFromJsonAsync<ApiResponse<T>>(endpoint);
        ProcessEvents(response);
        return response!;
    }

    public async Task<ApiResponse<T>> PutAsync<T>(string endpoint, object data)
    {
        var response = await _httpClient.PutAsJsonAsync(endpoint, data);
        var result = await response.Content.ReadFromJsonAsync<ApiResponse<T>>();
        ProcessEvents(result);
        return result!;
    }

    private void ProcessEvents<T>(ApiResponse<T>? response)
    {
        if (response?.Events == null) return;
       
        foreach (var domainEvent in response.Events)
        {
            _notificationService.HandleDomainEvent(domainEvent);
        }
    }
}
```

**Services/NotificationService.cs**
```csharp
using DomainEventsPOC.Core.Domain;

namespace DomainEventsPOC.Client.Services;

public class NotificationService
{
    public event Action<string>? OnNotification;

    public void HandleDomainEvent(IDomainEvent domainEvent)
    {
        var message = domainEvent switch
        {
            NameChangedEvent nameChanged =>
                $"Name changed from '{nameChanged.OldName}' to '{nameChanged.NewName}'",
            _ => $"Event occurred: {domainEvent.EventType}"
        };

        OnNotification?.Invoke(message);
    }
}
```

## Step 7: Configure Blazor Client

In `DomainEventsPOC.Client/Program.cs`:

```csharp
using DomainEventsPOC.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddScoped<ApiClient>();
builder.Services.AddScoped<NotificationService>();

await builder.Build().RunAsync();
```

## Step 8: Create UI Components

**Pages/People.razor**
```razor
@page "/people"
@using DomainEventsPOC.Client.Services
@using DomainEventsPOC.Core.Models
@inject ApiClient ApiClient

<h3>People</h3>

@if (people == null)
{
    <p>Loading...</p>
}
else
{
    <table class="table">
        <thead>
            <tr>
                <th>ID</th>
                <th>Name</th>
                <th>Actions</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var person in people)
            {
                <tr>
                    <td>@person.Id</td>
                    <td>@person.Name</td>
                    <td>
                        <button class="btn btn-primary"
                                @onclick="() => EditPerson(person.Id)">
                            Edit Name
                        </button>
                    </td>
                </tr>
            }
        </tbody>
    </table>
}

@if (showEditDialog)
{
    <div class="modal" style="display:block">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Edit Name</h5>
                </div>
                <div class="modal-body">
                    <input @bind="newName" class="form-control" />
                </div>
                <div class="modal-footer">
                    <button class="btn btn-primary" @onclick="SaveName">Save</button>
                    <button class="btn btn-secondary" @onclick="CancelEdit">Cancel</button>
                </div>
            </div>
        </div>
    </div>
}

<NotificationDisplay />

@code {
    private List<PersonDto> people = new();
    private bool showEditDialog;
    private Guid editingPersonId;
    private string newName = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        var response = await ApiClient.GetAsync<List<PersonDto>>("api/people");
        people = response.Data ?? new();
    }

    private void EditPerson(Guid id)
    {
        editingPersonId = id;
        var person = people.FirstOrDefault(p => p.Id == id);
        newName = person?.Name ?? string.Empty;
        showEditDialog = true;
    }

    private async Task SaveName()
    {
        var response = await ApiClient.PutAsync<PersonDto>(
            $"api/people/{editingPersonId}/name",
            new { NewName = newName });
       
        if (response.Success)
        {
            var updatedPerson = response.Data!;
            var index = people.FindIndex(p => p.Id == updatedPerson.Id);
            people[index] = updatedPerson;
        }
       
        showEditDialog = false;
    }

    private void CancelEdit()
    {
        showEditDialog = false;
    }

    public record PersonDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}
```

**Shared/NotificationDisplay.razor**
```razor
@using DomainEventsPOC.Client.Services
@inject NotificationService NotificationService
@implements IDisposable

<div class="notification-container" style="position: fixed; top: 20px; right: 20px; z-index: 1000;">
    @foreach (var notification in notifications)
    {
        <div class="alert alert-info alert-dismissible fade show" role="alert">
            @notification
            <button type="button" class="btn-close" @onclick="() => DismissNotification(notification)"></button>
        </div>
    }
</div>

@code {
    private List<string> notifications = new();

    protected override void OnInitialized()
    {
        NotificationService.OnNotification += HandleNotification;
    }

    private void HandleNotification(string message)
    {
        notifications.Add(message);
        StateHasChanged();
       
        // Auto-dismiss after 5 seconds
        var notification = message;
        Task.Delay(5000).ContinueWith(_ =>
        {
            InvokeAsync(() => DismissNotification(notification));
        });
    }

    private void DismissNotification(string notification)
    {
        notifications.Remove(notification);
        StateHasChanged();
    }

    public void Dispose()
    {
        NotificationService.OnNotification -= HandleNotification;
    }
}
```

## Step 9: Run the Application

1. Open two terminals in VS Code
2. In first terminal:
```bash
cd DomainEventsPOC.Server
dotnet run
```
3. In second terminal:
```bash
cd DomainEventsPOC.Client
dotnet run
```

## Step 10: Test the Application

1. Open the Blazor app in your browser (usually https://localhost:5001)
2. You should see the initial person listed
3. Click "Edit Name" and change the name
4. You should see a notification appear in the top-right corner showing the name change event
5. The table should update with the new name

This complete POC demonstrates:
- Domain event capture using EF Core's interceptor
- Event propagation through API responses
- Client-side notification handling
- Full-stack Blazor WASM implementation
- In-memory database for easy testing
