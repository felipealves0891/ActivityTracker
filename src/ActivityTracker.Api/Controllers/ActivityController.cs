using System.Buffers;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using ActivityTracker.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ActivityTracker.Api.Controllers;

public class ActivityController : ControllerBase
{
    private readonly IPublisher _publisher;
    private readonly ILogger<ActivityController> _logger;

    public ActivityController(
        IPublisher publisher,
        ILogger<ActivityController> logger) 
    {
        _publisher = publisher;
        _logger = logger;
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
            _logger.LogInformation($"Recebido uma mensagem do Web Socket {result.MessageType}!");
            
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
