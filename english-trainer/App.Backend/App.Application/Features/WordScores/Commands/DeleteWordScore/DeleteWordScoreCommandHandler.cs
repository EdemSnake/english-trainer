using App.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.WordScores.Commands.DeleteWordScore
{
    public class DeleteWordScoreCommandHandler : IRequestHandler<DeleteWordScoreCommand>
    {
        private readonly IAppDbContext _context;

        public DeleteWordScoreCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteWordScoreCommand request, CancellationToken cancellationToken)
        {
            var wordScore = await _context.WordScores.FirstOrDefaultAsync(ws => ws.Id == request.Id, cancellationToken);

            if (wordScore != null)
            {
                _context.WordScores.Remove(wordScore);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}