using App.Application.Common.Mappings;
using App.Domain;
using AutoMapper;

namespace App.Application.Dtos
{
    public class PhonemeScoreDto : IMapFrom<PhonemeScore>
    {
        public Guid Id { get; set; }
        public Guid WordScoreId { get; set; }
        public string? Phoneme { get; set; }
        public float Accuracy { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PhonemeScore, PhonemeScoreDto>()
                .ForMember(d => d.Accuracy, opt => opt.MapFrom(src => src.AccuracyScore));
        }
    }
}