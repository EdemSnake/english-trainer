using MediatR;

namespace App.Application.Features.AssessmentResults.Commands.UpdateAssessmentResult
{
    public class UpdateAssessmentResultCommand : IRequest
    {
        public Guid Id { get; set; }
        public float OverallAccuracy { get; set; }
        public float WordsPerMinute { get; set; }
    }
}