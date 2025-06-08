using App.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.TextPassages.Commands.UpdateTextPassage
{
    public class UpdateTextPassageCommandHandler : IRequestHandler<UpdateTextPassageCommand>
    {
        private readonly IAppDbContext _context;

        public UpdateTextPassageCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTextPassageCommand request, CancellationToken cancellationToken)
        {
            var textPassage = await _context.TextPassages.FirstOrDefaultAsync(tp => tp.Id == request.Id, cancellationToken);

            if (textPassage == null)
            {
                // Or throw a custom not found exception
                return Unit.Value;
            }

            textPassage.Content = request.Content ?? textPassage.Content;
            textPassage.DifficultyLevel = request.Difficulty ?? textPassage.DifficultyLevel;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}