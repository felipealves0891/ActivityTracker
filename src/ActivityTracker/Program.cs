using ActivityTracker;
using ActivityTracker.Services.ActivityRepository;
using ActivityTracker.Services.TrackerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddTransient<IActivityRepository, WebSocketRepository>();
        services.AddTransient<ITrackerService, Tracker>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
