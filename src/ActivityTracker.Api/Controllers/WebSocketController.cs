using ActivityTracker.Api.Data;
using ActivityTracker.Api.Models.Dtos;
using ActivityTracker.Api.Services.ActivityUpdaterService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Buffers;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace ActivityTracker.Api.Controllers
{
    [Route("api/websocket")]
    [ApiController]
    public class WebSocketController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IActivityUpdater _updater;
        private readonly ILogger<WebSocketController> _logger;

        public WebSocketController(
            ApplicationDbContext context,
            IActivityUpdater updater, 
            ILogger<WebSocketController> logger)
        {
            _context = context;
            _updater = updater;
            _logger = logger;
        }

        [HttpGet("worker")]
        public async Task WorkerAsync(CancellationToken cancellationToken)
        {
            if (!HttpContext.WebSockets.IsWebSocketRequest)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                _logger.LogInformation("A requisição não é um websocket!");
            }
            else
            {
                using var webSocket 
                    = await HttpContext.WebSockets.AcceptWebSocketAsync();

                await ReceiveAsync(webSocket, cancellationToken);
            }
        }

        private async Task ReceiveAsync(WebSocket socket, CancellationToken cancellationToken)
        {
            using IMemoryOwner<byte> memory = MemoryPool<byte>.Shared.Rent(1024 * 1024);

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(
                    memory.Memory, cancellationToken);

                if (result.MessageType == WebSocketMessageType.Close)
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, cancellationToken);
                else
                    await UpdateActivityAsync(memory.Memory.Slice(0, result.Count));
            }
        }

        private async Task UpdateActivityAsync(Memory<byte> memory)
        {
            var json = Encoding.UTF8.GetString(memory.Span);
            _logger.LogInformation(json);

            try
            {
                var dto = JsonSerializer.Deserialize<UpdateActivityDto>(json);
                if (dto is null)
                    return;

                await _updater.UpdateAsync(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
            }
        }

        [HttpGet("client")]
        public async Task ClientAsync(CancellationToken cancellationToken)
        {
            if (!HttpContext.WebSockets.IsWebSocketRequest)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                _logger.LogInformation("A requisição não é um websocket!");
            }
            else
            {
                using var webSocket
                    = await HttpContext.WebSockets.AcceptWebSocketAsync();

                await SendAsync(webSocket, cancellationToken);
            }
        }

        private async Task SendAsync(WebSocket socket, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var activities = _context.Activities.ToArray();
                var json = JsonSerializer.Serialize(activities);
                var data = Encoding.UTF8.GetBytes(json); 

                await socket.SendAsync(
                    data, WebSocketMessageType.Text, true, cancellationToken);

                await Task.Delay(5000);
            }
        }
    }
}
