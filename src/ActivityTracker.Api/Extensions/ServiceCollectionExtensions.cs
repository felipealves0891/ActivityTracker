namespace ActivityTracker.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMyHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {                
        var mongoConnectionString 
            = configuration.GetConnectionString("MongoDB") ??
                throw new Exception("Mongo DB Connection String is null");

        var postgresqlConnectionString 
            = configuration.GetConnectionString("Postgresql") ??
                throw new Exception("Postgresql Connection String is null");

        var rabbitMQConnectionString
            = configuration.GetConnectionString("RabbitMQ") ??
                throw new Exception("RabbitMQ Connection String is null");

        var redisConnectionString
            = configuration.GetConnectionString("Redis") ??
                throw new Exception("Redis Connection String is null");

        services.AddHealthChecks()
            .AddMongoDb(mongoConnectionString)
            .AddNpgSql(postgresqlConnectionString)
            .AddRabbitMQ(rabbitConnectionString: rabbitMQConnectionString)
            .AddRedis(redisConnectionString);

        return services;
    }
}
