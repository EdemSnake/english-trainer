using MediatR;

namespace App.Application.Features.AssessmentResults.Commands.DeleteAssessmentResult
{
    public class DeleteAssessmentResultCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}