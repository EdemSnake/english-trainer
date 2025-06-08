using App.Application.Common.Mappings;
using App.Domain;

namespace App.Application.Dtos
{
    public class PhonemeScoreDto : IMapFrom<PhonemeScore>
    {
        public Guid Id { get; set; }
        public Guid WordScoreId { get; set; }
        public string? Phoneme { get; set; }
        public float Accuracy { get; set; }
    }
}