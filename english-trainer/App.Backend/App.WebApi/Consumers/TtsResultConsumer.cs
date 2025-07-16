// Consumers/TtsResultConsumer.cs
using MassTransit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using App.WebApi.Hubs;

namespace App.WebApi.Consumers
{
    public class TtsResult
    {
        public string AudioUrl { get; set; }
    }

    public class TtsResultConsumer : IConsumer<TtsResult>
    {
        private readonly IHubContext<TtsHub> _hubContext;

        public TtsResultConsumer(IHubContext<TtsHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Consume(ConsumeContext<TtsResult> context)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveTtsResult", context.Message.AudioUrl);
        }
    }
}