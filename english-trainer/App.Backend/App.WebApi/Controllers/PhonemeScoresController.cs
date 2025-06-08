using App.Application.Dtos;
using App.Application.Features.PhonemeScores.Commands.CreatePhonemeScore;
using App.Application.Features.PhonemeScores.Commands.DeletePhonemeScore;
using App.Application.Features.PhonemeScores.Commands.UpdatePhonemeScore;
using App.Application.Features.PhonemeScores.Queries.GetPhonemeScores;
using Microsoft.AspNetCore.Mvc;

namespace App.WebApi.Controllers
{
    public class PhonemeScoresController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhonemeScoreDto>>> GetPhonemeScores()
        {
            return Ok(await Mediator.Send(new GetPhonemeScoresQuery()));
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreatePhonemeScoreCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, UpdatePhonemeScoreCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeletePhonemeScoreCommand { Id = id });

            return NoContent();
        }
    }
}