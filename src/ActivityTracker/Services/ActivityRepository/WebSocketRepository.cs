using ActivityTracker.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

        public async Task SaveAsync(Activity activity, CancellationToken cancellationToken)
        {
            if(_webSocket.State != WebSocketState.Open)
                await ConnectAsync(cancellationToken);

            var json = activity.ToString();
            byte[] data = Encoding.UTF8.GetBytes(json);
            await _webSocket.SendAsync(data, WebSocketMessageType.Text, true, cancellationToken);

            _logger.LogInformation(json);
        }

        private async Task ConnectAsync(CancellationToken cancellationToken)
        {
            var url = _configuration.GetValue<string>("WebSocket");
            var uri = new Uri(url);

            await _webSocket.ConnectAsync(uri, cancellationToken);
        }
    }
}
