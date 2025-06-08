using App.Application.Interfaces;
using App.Domain;
using MediatR;

namespace App.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IAppDbContext _context;

        public CreateUserCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = request.Username ?? string.Empty,
                PasswordHash = request.Password ?? string.Empty // In a real app, hash the password
            };

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}