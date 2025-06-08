using App.Application.Dtos;
using App.Application.Features.Users.Commands.CreateUser;
using App.Application.Features.Users.Commands.DeleteUser;
using App.Application.Features.Users.Commands.UpdateUser;
using App.Application.Features.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Mvc;

namespace App.WebApi.Controllers
{
    public class UsersController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            return Ok(await Mediator.Send(new GetUsersQuery()));
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateUserCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, UpdateUserCommand command)
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
            await Mediator.Send(new DeleteUserCommand { Id = id });

            return NoContent();
        }
    }
}