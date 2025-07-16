// Controllers/TtsController.cs
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using App.Application.Features.TextToSpeech;

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
    }
}