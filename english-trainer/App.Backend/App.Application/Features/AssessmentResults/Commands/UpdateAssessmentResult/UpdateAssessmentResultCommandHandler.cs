using App.Application.Interfaces;
using App.Application.Common.Exceptions;
using App.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.AssessmentResults.Commands.UpdateAssessmentResult
{
    public class UpdateAssessmentResultCommandHandler : IRequestHandler<UpdateAssessmentResultCommand>
    {
        private readonly IAppDbContext _context;

        public UpdateAssessmentResultCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateAssessmentResultCommand request, CancellationToken cancellationToken)
        {
            var assessmentResult = await _context.AssessmentResults.FirstOrDefaultAsync(ar => ar.Id == request.Id, cancellationToken);

            if (assessmentResult == null)
            {
                throw new EntityNotFoundException(nameof(AssessmentResult), request.Id);
            }

            assessmentResult.AccuracyScore = request.AccuracyScore;
            assessmentResult.FluencyScore = request.FluencyScore;
            assessmentResult.CompletenessScore = request.CompletenessScore;
            assessmentResult.PronunciationScore = request.PronunciationScore;
            assessmentResult.ProsodyScore = request.ProsodyScore;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}