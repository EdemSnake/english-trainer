using App.Application.Dtos;
using App.Application.Features.TextPassages.Commands.CreateTextPassage;
using App.Application.Features.TextPassages.Commands.DeleteTextPassage;
using App.Application.Features.TextPassages.Commands.UpdateTextPassage;
using App.Application.Features.TextPassages.Queries.GetTextPassages;
using Microsoft.AspNetCore.Mvc;

namespace App.WebApi.Controllers
{
    public class TextPassagesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<TextPassagesVm>> GetTextPassages()
        {
            return Ok(await Mediator.Send(new GetTextPassagesQuery()));
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateTextPassageCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, UpdateTextPassageCommand command)
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
            await Mediator.Send(new DeleteTextPassageCommand { Id = id });

            return NoContent();
        }
    }
}