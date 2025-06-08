using App.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.PhonemeScores.Commands.DeletePhonemeScore
{
    public class DeletePhonemeScoreCommandHandler : IRequestHandler<DeletePhonemeScoreCommand>
    {
        private readonly IAppDbContext _context;

        public DeletePhonemeScoreCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeletePhonemeScoreCommand request, CancellationToken cancellationToken)
        {
            var phonemeScore = await _context.PhonemeScores.FirstOrDefaultAsync(ps => ps.Id == request.Id, cancellationToken);

            if (phonemeScore != null)
            {
                _context.PhonemeScores.Remove(phonemeScore);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}