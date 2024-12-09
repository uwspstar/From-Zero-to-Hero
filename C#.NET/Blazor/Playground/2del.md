using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Yarp.ReverseProxy.Forwarder;

var builder = WebApplication.CreateBuilder(args);

// Add HttpForwarder service
builder.Services.AddHttpForwarder();

var app = builder.Build();

// Middleware pipeline
app.UseRouting();

app.Map("/api/fetch", async context =>
{
    // Retrieve the IHttpForwarder service
    var forwarder = context.RequestServices.GetRequiredService<IHttpForwarder>();

    // Construct the target URI
    var targetUri = new Uri("https://localhost:5058" + context.Request.Path + context.Request.QueryString);

    // Configure ForwarderRequestConfig
    var requestOptions = new ForwarderRequestConfig
    {
        ActivityTimeout = TimeSpan.FromSeconds(30), // Optional timeout
    };

    // HttpMessageInvoker is required by SendAsync
    var httpClient = new HttpMessageInvoker(new SocketsHttpHandler
    {
        UseCookies = false, // Optional: Disable cookies if unnecessary
    });

    // Forward the request
    var error = await forwarder.SendAsync(context, targetUri.ToString(), httpClient, requestOptions);

    // Handle errors if forwarding fails
    if (error != ForwarderError.None)
    {
        var errorFeature = context.Features.Get<IForwarderErrorFeature>();
        Console.WriteLine($"Forwarding error: {error}, Exception: {errorFeature?.Exception}");
        context.Response.StatusCode = 502; // Bad Gateway
    }
});

app.MapFallback(() => Results.Text("Reverse proxy is running!"));

app.Run();
