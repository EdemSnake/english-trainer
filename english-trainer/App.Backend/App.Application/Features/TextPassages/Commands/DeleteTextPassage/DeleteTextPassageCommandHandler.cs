using App.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.TextPassages.Commands.DeleteTextPassage
{
    public class DeleteTextPassageCommandHandler : IRequestHandler<DeleteTextPassageCommand>
    {
        private readonly IAppDbContext _context;

        public DeleteTextPassageCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTextPassageCommand request, CancellationToken cancellationToken)
        {
            var textPassage = await _context.TextPassages.FirstOrDefaultAsync(tp => tp.Id == request.Id, cancellationToken);

            if (textPassage != null)
            {
                _context.TextPassages.Remove(textPassage);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}