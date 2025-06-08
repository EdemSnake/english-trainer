using MediatR;

namespace App.Application.Features.AssessmentResults.Commands.CreateAssessmentResult
{
    public class CreateAssessmentResultCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid TextPassageId { get; set; }
        public float OverallAccuracy { get; set; }
        public float WordsPerMinute { get; set; }
    }
}