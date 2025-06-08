using MediatR;

namespace App.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string? Username { get; set; }
        public string? Password { get; set; } // Note: In a real app, this should be handled more securely.
    }
}