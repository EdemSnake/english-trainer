using App.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.AssessmentResults.Commands.DeleteAssessmentResult
{
    public class DeleteAssessmentResultCommandHandler : IRequestHandler<DeleteAssessmentResultCommand>
    {
        private readonly IAppDbContext _context;

        public DeleteAssessmentResultCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteAssessmentResultCommand request, CancellationToken cancellationToken)
        {
            var assessmentResult = await _context.AssessmentResults.FirstOrDefaultAsync(ar => ar.Id == request.Id, cancellationToken);

            if (assessmentResult != null)
            {
                _context.AssessmentResults.Remove(assessmentResult);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}