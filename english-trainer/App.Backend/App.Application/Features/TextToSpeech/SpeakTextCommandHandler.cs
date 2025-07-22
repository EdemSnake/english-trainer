// Features/TextToSpeech/SpeakTextCommandHandler.cs
using MediatR;
using MassTransit;
using System.Threading;
using System.Threading.Tasks;

namespace App.Application.Features.TextToSpeech
{
    public class SpeakTextCommandHandler : IRequestHandler<SpeakTextCommand>
    {
        private readonly IBus _bus;

        public SpeakTextCommandHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task<Unit> Handle(SpeakTextCommand request, CancellationToken cancellationToken)
        {
            var endpoint = await _bus.GetSendEndpoint(new Uri("queue:tts_requests"));
            var message = new SpeakTextMessage
            {
                Text = request.Text,
                Voice = request.Voice
            };

            await endpoint.Send(message, cancellationToken);

            return Unit.Value;
        }
    }
}