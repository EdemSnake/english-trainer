using App.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.PhonemeScores.Commands.UpdatePhonemeScore
{
    public class UpdatePhonemeScoreCommandHandler : IRequestHandler<UpdatePhonemeScoreCommand>
    {
        private readonly IAppDbContext _context;

        public UpdatePhonemeScoreCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdatePhonemeScoreCommand request, CancellationToken cancellationToken)
        {
            var phonemeScore = await _context.PhonemeScores.FirstOrDefaultAsync(ps => ps.Id == request.Id, cancellationToken);

            if (phonemeScore == null)
            {
                // Or throw a custom not found exception
                return Unit.Value;
            }

            phonemeScore.AccuracyScore = request.Accuracy;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}