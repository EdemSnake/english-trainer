using App.Application.Common.Mappings;
using App.Domain;

namespace App.Application.Dtos
{
    public class AssessmentResultDto : IMapFrom<AssessmentResult>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid TextPassageId { get; set; }
        public float OverallAccuracy { get; set; }
        public float WordsPerMinute { get; set; }
        public DateTime AssessmentDate { get; set; }
    }
}