// Features/TextToSpeech/SpeakTextCommand.cs
using MediatR;

namespace App.Application.Features.TextToSpeech
{
    public class SpeakTextCommand : IRequest
    {
        public string Text { get; set; }
        public string Voice { get; set; }
    }
}