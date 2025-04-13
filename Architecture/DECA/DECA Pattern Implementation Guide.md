# DECA Pattern Implementation Guide

Here's a step-by-step guide to implement the DECA pattern in .NET Core using VS Code, with clear code structure and implementation details:

## Project Setup in VS Code

NOTE :
-  may need run `dotnet new install Microsoft.DotNet.Web.ProjectTemplates.8.0 --force`

### 1. Create Solution & Projects

```bash
# Create solution
dotnet new sln -n ClaimSystem

# Create projects
dotnet new webapi -n ClaimSystem.API
dotnet new blazor -n ClaimSystem.Web --auth Individual
dotnet new classlib -n ClaimSystem.Core

# Add projects to solution
dotnet sln add ClaimSystem.API/ClaimSystem.API.csproj
dotnet sln add ClaimSystem.Web/ClaimSystem.Web.csproj
dotnet sln add ClaimSystem.Core/ClaimSystem.Core.csproj

# Open in VS Code
code .
```

## 2. Install Required Extensions

1. C# Dev Kit
2. SQL Server (mssql)
3. Blazor Snippets
4. Azure Tools (optional)

## Solution Structure

```txt
ClaimSystem/
├── ClaimSystem.API/
│   ├── Controllers/
│   ├── Services/
│   ├── Program.cs
│   └── appsettings.json
├── ClaimSystem.Web/
│   ├── Pages/
│   ├── Shared/
│   ├── wwwroot/
│   └── Program.cs
├── ClaimSystem.Core/
│   ├── Models/
│   ├── Interfaces/
│   ├── Services/
│   └── Extensions/
└── ClaimSystem.sln
```

## Core Implementation Steps

### 1. Define Domain Models

```csharp
// ClaimSystem.Core/Models/Claim.cs
public class Claim
{
    public int Id { get; set; }
    public string ClaimNumber { get; set; }
    public string Status { get; set; }
    public decimal Amount { get; set; }
    public string AssigneeId { get; set; }
    
    [NotMapped]
    public Dictionary<string, object> OriginalValues { get; set; } = new();
    
    // Navigation properties
    public virtual ApplicationUser Assignee { get; set; }
    public virtual ICollection<ClaimAudit> Audits { get; set; }
}

// ClaimSystem.Core/Models/ClaimRule.cs
public class ClaimRule
{
    public string Name { get; set; }
    public List<string> TriggerFields { get; set; }
    public List<RuleCondition> Conditions { get; set; }
}

public class RuleCondition
{
    public string Field { get; set; }
    public string Operator { get; set; }
    public string Value { get; set; }
    public List<RuleAction> Actions { get; set; }
}
```

### 2. Configure Database Context

```csharp
// ClaimSystem.Core/Data/AppDbContext.cs
public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Claim> Claims { get; set; }
    public DbSet<ClaimAudit> ClaimAudits { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Claim>(entity => 
        {
            entity.HasIndex(c => c.ClaimNumber).IsUnique();
            entity.Property(c => c.Amount).HasColumnType("decimal(18,2)");
            
            entity.HasMany(c => c.Audits)
                  .WithOne(a => a.Claim)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
```

### 3. Set Up Rule Configuration

```json
// ClaimSystem.API/appsettings.json
"DecaRules": {
  "Claim": [
    {
      "Name": "StatusChangeRule",
      "TriggerFields": ["Status"],
      "Conditions": [
        {
          "Field": "Status",
          "Operator": "changed",
          "Actions": [
            {
              "Type": "Notify",
              "Target": "All",
              "Template": "Claim {ClaimNumber} status changed to {Status}"
            }
          ]
        }
      ]
    }
  ]
}
```

### 4. Implement Rule Engine

```csharp
// ClaimSystem.Core/Services/DecaEngine.cs
public class DecaEngine : IDecaEngine
{
    private readonly IConfiguration _config;
    private readonly IHubContext<NotificationHub> _hub;
    private readonly AppDbContext _context;

    public async Task ProcessChanges(Claim claim, ClaimsPrincipal user)
    {
        var rules = _config.GetSection("DecaRules:Claim").Get<List<ClaimRule>>();
        var changes = GetFieldChanges(claim);

        foreach (var rule in rules)
        {
            if (!rule.TriggerFields.Any(f => changes.ContainsKey(f))) 
                continue;

            foreach (var condition in rule.Conditions)
            {
                if (EvaluateCondition(condition, claim, changes))
                {
                    await ExecuteActions(condition.Actions, claim, user);
                }
            }
        }
    }

    private Dictionary<string, FieldChange> GetFieldChanges(Claim claim)
    {
        return typeof(Claim).GetProperties()
            .Where(p => claim.OriginalValues.ContainsKey(p.Name))
            .ToDictionary(
                p => p.Name,
                p => new FieldChange
                {
                    OldValue = claim.OriginalValues[p.Name],
                    NewValue = p.GetValue(claim)
                });
    }
}
```

### 5. Set Up SignalR Hub

```csharp
// ClaimSystem.API/Hubs/NotificationHub.cs
public class NotificationHub : Hub
{
    public async Task SubscribeToClaim(string claimId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"claim-{claimId}");
    }
}

// Program.cs
builder.Services.AddSignalR();
app.MapHub<NotificationHub>("/notificationHub");
```

### 6. Database Change Detection

```csharp
// ClaimSystem.API/Services/ClaimChangeService.cs
public class ClaimChangeService : BackgroundService
{
    private readonly IServiceProvider _services;
    private SqlTableDependency<Claim> _dependency;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        
        _dependency = new SqlTableDependency<Claim>(
            db.Database.GetConnectionString(),
            "Claims",
            mapper: new CustomClaimMapper());

        _dependency.OnChanged += async (sender, e) => 
        {
            using var processScope = _services.CreateScope();
            var engine = processScope.ServiceProvider.GetRequiredService<IDecaEngine>();
            var user = processScope.ServiceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext.User;
            
            await engine.ProcessChanges(e.Entity, user);
        };

        _dependency.Start();
        return Task.CompletedTask;
    }
}
```

### 7. Blazor Client Integration

```razor
// ClaimSystem.Web/Pages/ClaimDetail.razor
@page "/claims/{claimId}"
@inject NavigationManager Navigation
@implements IAsyncDisposable

<NotificationToast @ref="_toast" />

<div class="claim-detail">
    <!-- Claim form fields -->
</div>

@code {
    private HubConnection _hubConnection;
    private Claim _claim;
    private NotificationToast _toast;

    protected override async Task OnInitializedAsync()
    {
        _claim = await ClaimService.GetClaimAsync(claimId);
        
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(Navigation.ToAbsoluteUri("/notificationHub"))
            .Build();

        _hubConnection.On<NotificationMessage>("ClaimUpdated", message => 
        {
            _toast.ShowNotification(message);
            StateHasChanged();
        });

        await _hubConnection.StartAsync();
        await _hubConnection.SendAsync("SubscribeToClaim", claimId);
    }
}
```

## Key Implementation Files

### Backend (API)

```txt
ClaimSystem.API/
├── Controllers/
│   └── ClaimsController.cs
├── Hubs/
│   └── NotificationHub.cs
├── Services/
│   ├── ClaimChangeService.cs
│   └── NotificationService.cs
├── Program.cs
└── appsettings.json
```

### Frontend (Blazor)

```txt
ClaimSystem.Web/
├── Pages/
│   ├── Claims/
│   │   ├── Index.razor
│   │   └── Detail.razor
│   └── Shared/
│       ├── NotificationToast.razor
│       └── NavMenu.razor
├── Services/
│   ├── ClaimClientService.cs
│   └── NotificationClientService.cs
└── Program.cs
```

### Core Library

```txt
ClaimSystem.Core/
├── Models/
│   ├── Claim.cs
│   ├── ClaimRule.cs
│   └── Notification.cs
├── Interfaces/
│   ├── IDecaEngine.cs
│   └── INotificationService.cs
└── Services/
    ├── DecaEngine.cs
    └── NotificationService.cs
```

## Running the Application

### 1. **Apply Database Migrations**

```bash
cd ClaimSystem.API
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 2. **Enable Service Broker**

```sql
ALTER DATABASE ClaimDb SET ENABLE_BROKER WITH ROLLBACK IMMEDIATE;
```

### 3. **Run Projects**

```bash
# Terminal 1 - API
dotnet run --project ClaimSystem.API

# Terminal 2 - Blazor
dotnet run --project ClaimSystem.Web
```

## Debugging Tips

### 1. **SQL Table Dependency Debugging**

```csharp
// Configure in Program.cs
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
SqlTableDependency.EnableDebugMode();
```

### 2. **SignalR Logging**

```csharp
// Client-side connection
_hubConnection = new HubConnectionBuilder()
    .ConfigureLogging(logging => {
        logging.AddConsole();
        logging.SetMinimumLevel(LogLevel.Debug);
    })
    .WithUrl("/notificationHub")
    .Build();
```

### 3. **EF Core Diagnostics**

```csharp
// In DbContext configuration
options.UseSqlServer(connectionString)
       .EnableSensitiveDataLogging()
       .LogTo(Console.WriteLine, LogLevel.Information);
```

## Production Considerations

1. **Scale SignalR**:

```csharp
// In Program.cs
builder.Services.AddSignalR().AddAzureSignalR();
```

### 2. **Database Optimization**

```sql
CREATE INDEX IX_Claims_Status ON Claims(Status) 
WHERE Status IN ('Pending', 'Approved', 'Rejected');
```

### 3. **Rule Caching**

```csharp
// In DecaEngine
private readonly IMemoryCache _cache;
var rules = await _cache.GetOrCreateAsync("DecaRules", async entry => {
    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
    return await LoadRulesFromDatabase();
});
```

This implementation provides a complete DECA pattern solution with:

- Real-time database change detection
- Dynamic rule evaluation
- Multi-target notifications
- Secure access control
- Comprehensive auditing

The architecture supports easy extension for new rule types and action handlers while maintaining clean separation of concerns.
