using MediatR;

namespace App.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; } // Note: In a real app, this should be handled more securely.
    }
}