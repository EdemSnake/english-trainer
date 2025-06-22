using App.Application.Interfaces;
using App.Domain;
using MediatR;

namespace App.Application.Features.AssessmentResults.Commands.CreateAssessmentResult
{
    public class CreateAssessmentResultCommandHandler : IRequestHandler<CreateAssessmentResultCommand, Guid>
    {
        private readonly IAppDbContext _context;

        public CreateAssessmentResultCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateAssessmentResultCommand request, CancellationToken cancellationToken)
        {
            var assessmentResult = new AssessmentResult
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                TextPassageId = request.TextPassageId,
                AccuracyScore = request.AccuracyScore,
                FluencyScore = request.FluencyScore,
                CompletenessScore = request.CompletenessScore,
                PronunciationScore = request.PronunciationScore,
                ProsodyScore = request.ProsodyScore,
                AssessedAt = DateTime.UtcNow
            };

            await _context.AssessmentResults.AddAsync(assessmentResult, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return assessmentResult.Id;
        }
    }
}