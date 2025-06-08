using App.Application.Common.Mappings;
using App.Domain;

namespace App.Application.Dtos
{
    public class WordScoreDto : IMapFrom<WordScore>
    {
        public Guid Id { get; set; }
        public Guid AssessmentResultId { get; set; }
        public string? Word { get; set; }
        public float Accuracy { get; set; }
    }
}