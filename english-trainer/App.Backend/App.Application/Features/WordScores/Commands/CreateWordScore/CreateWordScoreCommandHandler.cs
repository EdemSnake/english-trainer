using App.Application.Interfaces;
using App.Domain;
using MediatR;

namespace App.Application.Features.WordScores.Commands.CreateWordScore
{
    public class CreateWordScoreCommandHandler : IRequestHandler<CreateWordScoreCommand, Guid>
    {
        private readonly IAppDbContext _context;

        public CreateWordScoreCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateWordScoreCommand request, CancellationToken cancellationToken)
        {
            var wordScore = new WordScore
            {
                Id = Guid.NewGuid(),
                AssessmentResultId = request.AssessmentResultId,
                Word = request.Word ?? string.Empty,
                AccuracyScore = request.Accuracy
            };

            await _context.WordScores.AddAsync(wordScore, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return wordScore.Id;
        }
    }
}