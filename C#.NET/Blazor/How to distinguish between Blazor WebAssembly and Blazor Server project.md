To distinguish between a **Blazor WebAssembly** project and a **Blazor Server** project, you can look at several key differences in the project structure, files, and configurations.

---

### **1. File Structure Differences**

#### **Blazor WebAssembly Project**
- The project has a `wwwroot/` folder containing static assets like HTML, CSS, and JavaScript.
- No `Startup.cs` or `Program.cs` configuration related to a server-side application.
- Presence of a `_framework/` folder in `wwwroot/` when published, indicating the Blazor WebAssembly runtime.
- The entry point is the `wwwroot/index.html` file (not `Pages/_Host.cshtml`).

Example file structure for a Blazor WebAssembly project:
```
BlazorWebAssemblyApp/
├── wwwroot/
│   ├── index.html
│   ├── css/
│   └── js/
├── Pages/
│   └── Index.razor
├── Program.cs
└── BlazorWebAssemblyApp.csproj
```

#### **Blazor Server Project**
- No `wwwroot/index.html` file. Instead, there is a `_Host.cshtml` file located in the `Pages/` folder (or `Views/` in older templates).
- Presence of `Startup.cs` (or its equivalent configurations in `Program.cs`) for configuring the server-side middleware.
- The project will include a `razor` file that acts as the entry point for Razor Components hosted on the server (`_Host.cshtml`).
- Requires server execution to render components.

Example file structure for a Blazor Server project:
```
BlazorServerApp/
├── Pages/
│   ├── _Host.cshtml
│   └── Index.razor
├── Startup.cs
├── Program.cs
├── App.razor
└── BlazorServerApp.csproj
```

---

### **2. Program.cs Differences**

#### **Blazor WebAssembly**
The `Program.cs` file in a WebAssembly project sets up the app to run in the browser with `builder.RootComponents.Add<App>("app");`.

```csharp
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

await builder.Build().RunAsync();
```

#### **Blazor Server**
The `Program.cs` file in a Server project sets up the server environment using `builder.Services.AddServerSideBlazor();`.

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
```

---

### **3. Hosting Model**
- **Blazor WebAssembly**: Runs entirely in the browser using WebAssembly. The application does not depend on a server to render UI.
- **Blazor Server**: Runs on the server, sending UI updates to the browser over a SignalR connection.

---

### **4. Dependencies in the `.csproj`**

#### **Blazor WebAssembly**:
The `.csproj` file contains references to WebAssembly-specific SDKs like `Microsoft.AspNetCore.Components.WebAssembly`.

```xml
<Project Sdk="Microsoft.NET.Sdk.BlazorWebAssembly">
  <PropertyGroup>
    <TargetFramework>net7.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly" Version="7.0.0" />
    <PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly.DevServer" Version="7.0.0" />
  </ItemGroup>
</Project>
```

#### **Blazor Server**:
The `.csproj` file references `Microsoft.NET.Sdk.Web` and includes the server-side Blazor dependency `Microsoft.AspNetCore.Components`.

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net7.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Components" Version="7.0.0" />
  </ItemGroup>
</Project>
```

---

### **5. Run and Debug Behavior**
- **Blazor WebAssembly**:
  - Can be hosted as a static site without a server (e.g., served via Azure Static Web Apps or GitHub Pages).
  - Debugging occurs in the browser (via browser developer tools or Visual Studio).
  
- **Blazor Server**:
  - Requires a running server to host the app.
  - Debugging occurs on both server-side logic and browser UI updates.

---

### **Summary Table**

| **Aspect**                 | **Blazor WebAssembly**                     | **Blazor Server**                          |
|----------------------------|--------------------------------------------|--------------------------------------------|
| **Entry Point**            | `wwwroot/index.html`                      | `Pages/_Host.cshtml`                       |
| **Runtime**                | Runs in the browser (WebAssembly)         | Runs on the server, UI updates via SignalR |
| **Dependencies**           | `Microsoft.AspNetCore.Components.WebAssembly` | `Microsoft.AspNetCore.Components`         |
| **CSP Configuration**      | Configure in `wwwroot/index.html`         | Configure in `Startup.cs` or `Program.cs` |
| **Offline Support**        | Yes (PWA possible)                        | No                                         |
| **Debugging**              | Browser or Visual Studio                  | Server and client                          |

---

By checking the **file structure**, **Program.cs**, or **.csproj**, you can quickly identify whether the project is a Blazor WebAssembly or Blazor Server app.
