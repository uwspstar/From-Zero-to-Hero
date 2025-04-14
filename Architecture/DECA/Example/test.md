# Capturing Domain Events from Entity Framework (NameChanged Example)

Here's how to implement domain event capture from Entity Framework, specifically for events like `NameChanged`, and propagate them to your Blazor WASM client:

## 1. Domain Event Implementation

First, create your domain events and entity base class:

```csharp
// DomainEvent.cs
public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}

public abstract class DomainEvent : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}

// NameChangedEvent.cs
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
}
```

## 2. Entity Base Class with Domain Events

```csharp
public abstract class Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();
   
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
   
    protected void AddDomainEvent(IDomainEvent eventItem) => _domainEvents.Add(eventItem);
   
    public void ClearDomainEvents() => _domainEvents.Clear();
}

// Example entity
public class Person : Entity
{
    private string _name;
   
    public Guid Id { get; private set; }
   
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

## 3. EF Core Interceptor to Dispatch Events

```csharp
public class DomainEventDispatcherInterceptor : SaveChangesInterceptor
{
    private readonly IDomainEventDispatcher _dispatcher;

    public DomainEventDispatcherInterceptor(IDomainEventDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context == null) return result;
       
        await DispatchDomainEvents(eventData.Context);
        return result;
    }

    private async Task DispatchDomainEvents(DbContext context)
    {
        var entities = context.ChangeTracker
            .Entries<Entity>()
            .Where(e => e.Entity.DomainEvents.Any())
            .Select(e => e.Entity)
            .ToList();

        foreach (var entity in entities)
        {
            var events = entity.DomainEvents.ToArray();
            entity.ClearDomainEvents();
           
            foreach (var domainEvent in events)
            {
                await _dispatcher.DispatchAsync(domainEvent);
            }
        }
    }
}
```

## 4. Register the Interceptor in DbContext

```csharp
public class AppDbContext : DbContext
{
    private readonly IDomainEventDispatcher _dispatcher;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        IDomainEventDispatcher dispatcher) : base(options)
    {
        _dispatcher = dispatcher;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new DomainEventDispatcherInterceptor(_dispatcher));
    }
   
    // Register your DbSets here
    public DbSet<Person> People { get; set; }
}
```

## 5. Domain Event Dispatcher Implementation

```csharp
public interface IDomainEventDispatcher
{
    Task DispatchAsync(IDomainEvent domainEvent);
}

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IEventPublisher _publisher;

    public DomainEventDispatcher(IServiceProvider serviceProvider, IEventPublisher publisher)
    {
        _serviceProvider = serviceProvider;
        _publisher = publisher;
    }

    public async Task DispatchAsync(IDomainEvent domainEvent)
    {
        // First handle domain logic (optional)
        var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
        var handlers = _serviceProvider.GetServices(handlerType);
       
        foreach (dynamic handler in handlers)
        {
            await handler.HandleAsync((dynamic)domainEvent);
        }

        // Then publish to client
        await _publisher.PublishAsync(domainEvent);
    }
}
```

## 6. Event Publisher (Choose one from previous options)

For example, using SSE:

```csharp
public class SseEventPublisher : IEventPublisher
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly List<SseClient> _clients = new();
    private readonly object _lock = new();

    public SseEventPublisher(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task PublishAsync(IDomainEvent domainEvent)
    {
        List<SseClient> clients;
        lock (_lock)
        {
            clients = new List<SseClient>(_clients);
        }

        var json = JsonSerializer.Serialize(domainEvent);
        var tasks = clients.Select(client =>
            client.Response.WriteAsync($"data: {json}\n\n"));
       
        await Task.WhenAll(tasks);
    }

    public void AddClient(HttpResponse response)
    {
        lock (_lock)
        {
            _clients.Add(new SseClient(response));
        }
    }

    public void RemoveClient(HttpResponse response)
    {
        lock (_lock)
        {
            _clients.RemoveAll(c => c.Response == response);
        }
    }

    private class SseClient
    {
        public HttpResponse Response { get; }

        public SseClient(HttpResponse response)
        {
            Response = response;
        }
    }
}
```

## 7. API Controller for SSE

```csharp
[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly SseEventPublisher _publisher;

    public EventsController(SseEventPublisher publisher)
    {
        _publisher = publisher;
    }

    [HttpGet("stream")]
    public async Task StreamEvents()
    {
        Response.ContentType = "text/event-stream";
        Response.Headers.CacheControl = "no-cache";
        Response.Headers.Connection = "keep-alive";

        _publisher.AddClient(Response);
       
        try
        {
            while (!HttpContext.RequestAborted.IsCancellationRequested)
            {
                await Task.Delay(1000); // Keep connection alive
            }
        }
        finally
        {
            _publisher.RemoveClient(Response);
        }
    }
}
```

## 8. Blazor WASM Client Implementation

```csharp
public class EventNotificationService : IAsyncDisposable
{
    private readonly HttpClient _httpClient;
    private readonly NotificationDispatcher _dispatcher;
    private EventSource? _eventSource;

    public EventNotificationService(HttpClient httpClient, NotificationDispatcher dispatcher)
    {
        _httpClient = httpClient;
        _dispatcher = dispatcher;
    }

    public async Task StartAsync()
    {
        _eventSource = new EventSource("api/events/stream", _httpClient);
       
        _eventSource.OnMessage += (sender, e) =>
        {
            try
            {
                var domainEvent = JsonSerializer.Deserialize<IDomainEvent>(e.Data,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
               
                if (domainEvent is NameChangedEvent nameChanged)
                {
                    _dispatcher.Dispatch(new ClientNotification
                    {
                        Title = "Name Changed",
                        Message = $"Name changed from {nameChanged.OldName} to {nameChanged.NewName}",
                        EventType = "NameChanged",
                        EntityId = nameChanged.EntityId
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing event: {ex.Message}");
            }
        };
       
        await _eventSource.StartAsync();
    }

    public async ValueTask DisposeAsync()
    {
        if (_eventSource != null)
        {
            await _eventSource.CloseAsync();
        }
    }
}
```

## 9. UI Notification Component

```razor
@implements IAsyncDisposable
@inject EventNotificationService NotificationService

<div class="notification-container">
    @foreach (var notification in _notifications)
    {
        <div class="notification @notification.Type">
            <h5>@notification.Title</h5>
            <p>@notification.Message</p>
            <button @onclick="() => Dismiss(notification.Id)">×</button>
        </div>
    }
</div>

@code {
    private List<ClientNotification> _notifications = new();
   
    protected override async Task OnInitializedAsync()
    {
        await NotificationService.StartAsync();
    }
   
    private void Dismiss(Guid id)
    {
        _notifications.RemoveAll(n => n.Id == id);
        StateHasChanged();
    }
   
    public async ValueTask DisposeAsync()
    {
        await NotificationService.DisposeAsync();
    }
}
```

## Key Points for NameChanged Event Flow

1. **Entity Property Setter** triggers the event when name changes
2. **EF Core Interceptor** captures all pending events during SaveChanges
3. **DomainEventDispatcher** processes domain logic and publishes to clients
4. **SSE Publisher** maintains client connections and broadcasts events
5. **Blazor Client** receives events and displays notifications
