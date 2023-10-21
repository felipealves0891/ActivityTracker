using System.Buffers;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using ActivityTracker.Api.Services.PublisherService;
using ActivityTracker.Api.Services.RepositoryService;
using ActivityTracker.Core;
using Microsoft.AspNetCore.Mvc;

namespace ActivityTracker.Api.Controllers;

[Route("activities")]
public class ActivityController : ControllerBase
{
    private readonly IPublisher _publisher;
    private readonly IRepository _repository;
    private readonly ILogger<ActivityController> _logger;

    public ActivityController(
        IPublisher publisher,
        IRepository repository,
        ILogger<ActivityController> logger) 
    {
        _publisher = publisher;
        _repository = repository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<ActivityEntity>> GetAllAsync()
    {
        return await _repository.GetActivitiesAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActivityEntity> GetAsync(int id)
    {
        return await _repository.GetActivityByIdAsync(id);
    }

    [HttpGet("lastet")]
    public async Task<ActivityEntity> GetLastetAsync()
    {
        return await _repository.GetActivityLastetAsync();
    }

    [HttpGet("/ws/activity")]
    public async Task<IActionResult> ActivityAsync(CancellationToken cancellationToken)
    {
        if (!HttpContext.WebSockets.IsWebSocketRequest)
            return BadRequest("A requisição não é um websocket!");

        _logger.LogInformation("Recebido uma requisição que é Web Socket!");

        using var webSocket 
            = await HttpContext.WebSockets.AcceptWebSocketAsync();

        await ReceiveAsync(webSocket, cancellationToken);
        return NoContent();
        
    }

    private async Task ReceiveAsync(WebSocket socket, CancellationToken cancellationToken)
    {
        using IMemoryOwner<byte> buffer = MemoryPool<byte>.Shared.Rent(1024 * 1024);

        while (socket.State == WebSocketState.Open && !cancellationToken.IsCancellationRequested)
        {
            var result = await socket.ReceiveAsync(buffer.Memory, cancellationToken);
            _logger.LogInformation($"Recebido uma mensagem do tipo {result.MessageType}!");
            
            if (result.MessageType == WebSocketMessageType.Close)
            {
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, cancellationToken);
                return;
            }
            
            var memory = buffer.Memory.Slice(0, result.Count);
            await _publisher.PublishAsync(memory, cancellationToken);
        }
    }




}
