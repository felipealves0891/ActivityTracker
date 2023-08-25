using ActivityTracker.Core.Models;

namespace ActivityTracker.Api.Services;

public interface IPublisher : IDisposable
{
    Task PublishAsync(ReadOnlyMemory<byte> message, CancellationToken cancellationToken);
}