using System.Text;
using System.Text.Json;
using ActivityTracker.Api.Services;
using ActivityTracker.Core.Models;
using RabbitMQ.Client;

namespace ActivityTracker.Api;

public sealed class Publisher : IPublisher
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queue;

    public Publisher(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RabbitMQ")
            ?? throw new Exception("RabbitMQ Connection String");

        _queue = configuration["RabbitMQ:Activity:Queue"]
            ?? throw new Exception("RabbitMQ Activity Queue");

        var factory = new ConnectionFactory()
        {
            Uri = new Uri(connectionString),
        };
        
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(
            queue: _queue,
            durable: true,
            exclusive: false,
            autoDelete: false);
    }

    public async Task PublishAsync(ReadOnlyMemory<byte> message, CancellationToken cancellationToken)
    {
        await Task.Run(() => Publish(message), cancellationToken);
    }

    private void Publish(ReadOnlyMemory<byte> body)
    {
        _channel.BasicPublish(exchange: string.Empty,
                              routingKey: _queue,
                              basicProperties: null,
                              body: body);
    }

    public void Dispose()
    {
        if(_channel != null)
            _channel.Dispose();

        if(_connection != null)
            _connection.Dispose();
    }
}
