using ActivityTracker.Api;
using ActivityTracker.Api.Extensions;
using ActivityTracker.Api.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMyHealthChecks(configuration);
builder.Services.AddSingleton<IPublisher, Publisher>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseWebSockets(new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromSeconds(5)
});

app.UseHttpsRedirection();
app.MapControllers();

app.MapHealthChecks("/healthcheck", new HealthCheckOptions
{
    ResponseWriter = HealthCheckExtensions.WriteResponse
});

await app.RunAsync();
