using App.Application.Common.Mappings;
using App.Domain;

namespace App.Application.Dtos
{
    public class AssessmentResultDto : IMapFrom<AssessmentResult>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid TextPassageId { get; set; }
        public float AccuracyScore { get; set; }
        public float FluencyScore { get; set; }
        public float CompletenessScore { get; set; }
        public float PronunciationScore { get; set; }
        public float ProsodyScore { get; set; }
        public DateTime AssessedAt { get; set; }
    }
}