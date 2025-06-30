using App.Application.Interfaces;
using App.Application.Common.Exceptions;
using App.Domain;
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
                throw new EntityNotFoundException(nameof(PhonemeScore), request.Id);
            }

            phonemeScore.AccuracyScore = request.Accuracy;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}