using App.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.WordScores.Commands.UpdateWordScore
{
    public class UpdateWordScoreCommandHandler : IRequestHandler<UpdateWordScoreCommand>
    {
        private readonly IAppDbContext _context;

        public UpdateWordScoreCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateWordScoreCommand request, CancellationToken cancellationToken)
        {
            var wordScore = await _context.WordScores.FirstOrDefaultAsync(ws => ws.Id == request.Id, cancellationToken);

            if (wordScore == null)
            {
                // Or throw a custom not found exception
                return Unit.Value;
            }

            wordScore.AccuracyScore = request.Accuracy;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}