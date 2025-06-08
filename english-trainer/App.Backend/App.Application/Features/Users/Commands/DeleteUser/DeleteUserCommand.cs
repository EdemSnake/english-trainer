using MediatR;

namespace App.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}