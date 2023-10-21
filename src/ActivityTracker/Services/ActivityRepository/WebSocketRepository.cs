using ActivityTracker.Core.Models;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace ActivityTracker.Services.ActivityRepository
{
    public class WebSocketRepository : IActivityRepository
    {
        private readonly ILogger<WebSocketRepository> _logger;
        private readonly IConfiguration _configuration;
        private readonly ClientWebSocket _webSocket;

        public WebSocketRepository(ILogger<WebSocketRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _webSocket = new ClientWebSocket();
        }

        public async Task SaveAsync(IEnumerable<ProcessEntity> activities, CancellationToken cancellationToken)
        {
            if(_webSocket.State != WebSocketState.Open)
                await ConnectAsync(cancellationToken);

            var json = JsonSerializer.Serialize(activities);

            byte[] data = Encoding.UTF8.GetBytes(json);
            await _webSocket.SendAsync(data, WebSocketMessageType.Text, true, cancellationToken);
        }

        private async Task ConnectAsync(CancellationToken cancellationToken)
        {
            var url = _configuration.GetValue<string>("WebSocket");
            var uri = new Uri(url);

            await _webSocket.ConnectAsync(uri, cancellationToken);
        }
    }
}
