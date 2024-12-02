# API Usage Strategy

Developing and implementing an effective API usage strategy is critical for optimizing performance, ensuring security, and maintaining scalability of your applications.

---

### 1. **Authentication and Authorization**

- **Token-based Authentication**: Use tokens (e.g., JWT) to authenticate API requests.
  - **Example**: Implementing JWT authentication.
  - **.NET Core Code**:
    ```csharp
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;

    var builder = WebApplication.CreateBuilder(args);

    // Configure JWT Authentication
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "yourdomain.com",
                ValidAudience = "yourdomain.com",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKey"))
            };
        });

    var app = builder.Build();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapPost("/login", (UserModel user) =>
    {
        // Validate user (this is an example, replace with real validation)
        if (user.Username == "admin" && user.Password == "password")
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("YourSecretKey");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Username) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Results.Ok(new { Token = tokenHandler.WriteToken(token) });
        }

        return Results.Unauthorized();
    });

    app.MapGet("/protected", () => "This is a protected route").RequireAuthorization();

    app.Run();

    public record UserModel(string Username, string Password);
    ```

---

### 2. **Rate Limiting**

- **Purpose**: Prevent abuse and ensure fair use of API resources.
  - **Example**: Limiting each user to 1000 API calls per hour.
  - **.NET Core Code**:
    ```csharp
    using AspNetCoreRateLimit;

    var builder = WebApplication.CreateBuilder(args);

    // Add Rate Limiting
    builder.Services.AddMemoryCache();
    builder.Services.Configure<IpRateLimitOptions>(options =>
    {
        options.GeneralRules = new List<RateLimitRule>
        {
            new RateLimitRule
            {
                Endpoint = "*",
                Limit = 1000,
                Period = "1h"
            }
        };
    });
    builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
    builder.Services.AddInMemoryRateLimiting();

    var app = builder.Build();

    app.UseIpRateLimiting();

    app.MapGet("/api/data", () => "This is a rate-limited API endpoint");

    app.Run();
    ```

---

### 3. **Caching**

- **Purpose**: Improve performance by storing responses for commonly requested data.
  - **Example**: Using MemoryCache for API responses.
  - **.NET Core Code**:
    ```csharp
    using Microsoft.Extensions.Caching.Memory;

    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddMemoryCache();

    var app = builder.Build();

    app.MapGet("/data", (IMemoryCache cache) =>
    {
        const string cacheKey = "CachedData";
        if (!cache.TryGetValue(cacheKey, out string cachedData))
        {
            cachedData = "This is cached data"; // Replace with actual data fetching logic
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
            };
            cache.Set(cacheKey, cachedData, cacheOptions);
        }
        return cachedData;
    });

    app.Run();
    ```

---

### 4. **Logging and Monitoring**

- **Purpose**: Track usage, detect issues, and ensure system health.
  - **Example**: Using Serilog for structured logging.
  - **.NET Core Code**:
    ```csharp
    using Serilog;

    var builder = WebApplication.CreateBuilder(args);

    // Configure Serilog
    Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();

    builder.Host.UseSerilog();

    var app = builder.Build();

    app.MapGet("/", () => "This is a monitored API endpoint");

    app.Run();
    ```

---

### 5. **Versioning**

- **Purpose**: Manage changes and maintain backward compatibility.
  - **Example**: Using API versioning.
  - **.NET Core Code**:
    ```csharp
    using Microsoft.AspNetCore.Mvc;

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddApiVersioning(options =>
    {
        options.ReportApiVersions = true;
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = new ApiVersion(1, 0);
    });

    var app = builder.Build();

    app.MapGet("/api/v1/resource", () => "This is version 1 of the resource");
    app.MapGet("/api/v2/resource", () => "This is version 2 of the resource");

    app.Run();
    ```

---

### 6. **Documentation**

- **Purpose**: Provide clear and comprehensive API usage guidelines.
  - **Example**: Using Swashbuckle for API documentation.
  - **.NET Core Code**:
    ```csharp
    using Microsoft.OpenApi.Models;

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
    });

    var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));

    app.MapGet("/resource", () => "This is a documented API endpoint");

    app.Run();
    ```

---

### 7. **Error Handling**

- **Purpose**: Ensure meaningful error messages and codes are returned.
  - **Example**: Global exception handling middleware.
  - **.NET Core Code**:
    ```csharp
    var builder = WebApplication.CreateBuilder(args);

    var app = builder.Build();

    app.UseExceptionHandler("/error");

    app.MapGet("/error", () => Results.Problem("An unexpected error occurred."));
    app.MapGet("/", () => throw new Exception("Test exception"));

    app.Run();
    ```

---

### 8. **Security**

- **Purpose**: Protect APIs from threats like SQL injection, XSS, and DDoS attacks.
  - **Example**: Using HTTPS, input validation, and rate limiting.
  - **.NET Core Code**:
    ```csharp
    using Microsoft.AspNetCore.HttpOverrides;

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddHttpsRedirection(options => options.HttpsPort = 443);
    builder.Services.AddDataProtection();
    builder.Services.AddIpRateLimiting();

    var app = builder.Build();

    app.UseHttpsRedirection();
    app.UseIpRateLimiting();

    app.MapGet("/", () => "This is a secured API endpoint");

    app.Run();
    ```

---

### Summary

By following these strategies and using the provided .NET Core Web API code examples, you can ensure your API is robust, secure, and scalable. Additionally, you can efficiently manage API usage, implement proper authentication, and monitor system health to maintain reliability under various conditions.
