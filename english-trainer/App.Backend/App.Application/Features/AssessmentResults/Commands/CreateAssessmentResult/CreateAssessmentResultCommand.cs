using MediatR;

namespace App.Application.Features.AssessmentResults.Commands.CreateAssessmentResult
{
    public class CreateAssessmentResultCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid TextPassageId { get; set; }
        public float AccuracyScore { get; set; }
        public float FluencyScore { get; set; }
        public float CompletenessScore { get; set; }
        public float PronunciationScore { get; set; }
        public float ProsodyScore { get; set; }
    }
}