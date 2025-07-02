using App.Application.Features.PronunciationAssessment;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace App.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PronunciationAssessmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PronunciationAssessmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<string>> AssessPronunciation([FromForm] AssessPronunciationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}