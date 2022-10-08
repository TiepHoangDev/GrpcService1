using GrpcService1.Services;
using Microsoft.Extensions.Hosting.WindowsServices;

//1.binding path to start windown service!
var options = new WebApplicationOptions
{
    ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default,
    Args = args
};

var builder = WebApplication.CreateBuilder(options);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

builder.WebHost.UseUrls("http://localhost:12345");

// Add services to the container.
builder.Services.AddGrpc();

//2. start app as windows service
builder.Host.UseWindowsService();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

//3. Run appp
await app.RunAsync();
