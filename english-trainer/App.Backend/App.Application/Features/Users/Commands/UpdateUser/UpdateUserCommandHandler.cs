using App.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IAppDbContext _context;

        public UpdateUserCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (user == null)
            {
                // Or throw a custom not found exception
                return Unit.Value;
            }

            user.Username = request.Username ?? user.Username;
            user.PasswordHash = request.Password ?? user.PasswordHash; // In a real app, hash the password

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}