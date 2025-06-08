using App.Application.Interfaces;
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
                // Or throw a custom not found exception
                return Unit.Value;
            }

            assessmentResult.AccuracyScore = request.OverallAccuracy;
            assessmentResult.FluencyScore = request.WordsPerMinute;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}