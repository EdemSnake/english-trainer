using App.Application.Dtos;
using App.Application.Features.AssessmentResults.Commands.CreateAssessmentResult;
using App.Application.Features.AssessmentResults.Commands.DeleteAssessmentResult;
using App.Application.Features.AssessmentResults.Commands.UpdateAssessmentResult;
using App.Application.Features.AssessmentResults.Queries.GetAssessmentResults;
using Microsoft.AspNetCore.Mvc;

namespace App.WebApi.Controllers
{
    public class AssessmentResultsController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<AssessmentResultsVm>> GetAssessmentResults()
        {
            return Ok(await Mediator.Send(new GetAssessmentResultsQuery()));
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateAssessmentResultCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, UpdateAssessmentResultCommand command)
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
            await Mediator.Send(new DeleteAssessmentResultCommand { Id = id });

            return NoContent();
        }
    }
}