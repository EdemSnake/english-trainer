using MediatR;

namespace App.Application.Features.AssessmentResults.Commands.UpdateAssessmentResult
{
    public class UpdateAssessmentResultCommand : IRequest
    {
        public Guid Id { get; set; }
        public float AccuracyScore { get; set; }
        public float FluencyScore { get; set; }
        public float CompletenessScore { get; set; }
        public float PronunciationScore { get; set; }
        public float ProsodyScore { get; set; }
    }
}