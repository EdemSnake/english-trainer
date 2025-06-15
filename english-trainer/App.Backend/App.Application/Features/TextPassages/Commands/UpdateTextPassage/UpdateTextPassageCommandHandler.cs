using App.Application.Common.Exceptions;
using App.Application.Interfaces;
using App.Domain;
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
                throw new NotFoundException(nameof(TextPassage), request.Id);
            }

            textPassage.Content = request.Content ?? textPassage.Content;
            textPassage.DifficultyLevel = request.Difficulty ?? textPassage.DifficultyLevel;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}