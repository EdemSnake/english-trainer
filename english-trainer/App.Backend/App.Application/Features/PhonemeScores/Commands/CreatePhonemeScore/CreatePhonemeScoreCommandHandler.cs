using App.Application.Interfaces;
using App.Domain;
using MediatR;

namespace App.Application.Features.PhonemeScores.Commands.CreatePhonemeScore
{
    public class CreatePhonemeScoreCommandHandler : IRequestHandler<CreatePhonemeScoreCommand, Guid>
    {
        private readonly IAppDbContext _context;

        public CreatePhonemeScoreCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreatePhonemeScoreCommand request, CancellationToken cancellationToken)
        {
            var phonemeScore = new PhonemeScore
            {
                Id = Guid.NewGuid(),
                WordScoreId = request.WordScoreId,
                Phoneme = request.Phoneme ?? string.Empty,
                AccuracyScore = request.Accuracy
            };

            await _context.PhonemeScores.AddAsync(phonemeScore, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return phonemeScore.Id;
        }
    }
}