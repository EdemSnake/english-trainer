using App.Application.Dtos;
using App.Application.Features.WordScores.Commands.CreateWordScore;
using App.Application.Features.WordScores.Commands.DeleteWordScore;
using App.Application.Features.WordScores.Commands.UpdateWordScore;
using App.Application.Features.WordScores.Queries.GetWordScores;
using Microsoft.AspNetCore.Mvc;

namespace App.WebApi.Controllers
{
    public class WordScoresController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WordScoreDto>>> GetWordScores()
        {
            return Ok(await Mediator.Send(new GetWordScoresQuery()));
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateWordScoreCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, UpdateWordScoreCommand command)
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
            await Mediator.Send(new DeleteWordScoreCommand { Id = id });

            return NoContent();
        }
    }
}