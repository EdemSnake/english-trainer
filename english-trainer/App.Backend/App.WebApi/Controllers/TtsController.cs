// Controllers/TtsController.cs
using App.Application.Dtos;
using App.Application.Features.TextToSpeech;
using App.WebApi.Hubs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace App.WebApi.Controllers
{
    public class TtsController : BaseApiController
    {
        [HttpPost("speak")]
        public async Task<IActionResult> Speak([FromBody] SpeakTextCommand command)
        {
            if (command == null || string.IsNullOrEmpty(command.Text) || string.IsNullOrEmpty(command.Voice))
            {
                return BadRequest("Invalid request payload.");
            }

            await Mediator.Send(command);

            return Accepted();
        }
        [HttpPost("send")]
        public async Task<IActionResult> SendAudioUrl([FromBody] AudioUrlDto dto, [FromServices] IHubContext<TtsHub> hubContext)
        {
            if (string.IsNullOrEmpty(dto.AudioUrl))
            {
                return BadRequest("AudioUrl is required.");
            }
            Console.WriteLine($"[SignalR] Received audio URL: {dto.AudioUrl}");


            await hubContext.Clients.All.SendAsync("ReceiveAudioUrl", dto.AudioUrl);

            return Ok();
        }
    }
}